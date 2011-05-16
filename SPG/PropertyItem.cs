/*
 * Copyright © 2011, Denys Vuika
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * */

using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace System.Windows.Controls.PropertyGrid
{
  using ComponentModel;
  using Dynamic;

  /// <summary>
  /// PropertyItem hold a reference to an individual property in the propertygrid
  /// </summary>
  public sealed class PropertyItem : INotifyPropertyChanged, IPropertyFilterTarget
  {
    #region Events
    /// <summary>
    /// Event raised when an error is encountered attempting to set the Value
    /// </summary>
    public event EventHandler<ExceptionEventArgs> ValueError;
    /// <summary>
    /// Raises the ValueError event
    /// </summary>
    /// <param name="ex">The exception</param>
    private void OnValueError(Exception ex)
    {
      var handler = ValueError;
      if (handler != null) ValueError(this, new ExceptionEventArgs(ex));
    }
    #endregion

    #region Fields
    private DynamicPropertyInfo _PropertyInfo;
    private object _Instance;
    private bool _ReadOnly = false;
    #endregion

    #region Constructors
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="property"></param>
    public PropertyItem(object instance, object value, DynamicPropertyInfo property, bool readOnly)
    {
      _Instance = instance;
      _PropertyInfo = property;
      _value = value;
      _ReadOnly = readOnly;

      if (instance is INotifyPropertyChanged)
        ((INotifyPropertyChanged)instance).PropertyChanged += new PropertyChangedEventHandler(PropertyItem_PropertyChanged);
    }

    void PropertyItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == this.Name)
        Value = _PropertyInfo.GetValue(_Instance, null);
    }
    #endregion

    #region Properties

    public string Name
    {
      get { return _PropertyInfo.Name; }
    }

    public object Instance
    {
      get { return _Instance; }
    }

    private string _Description;
    public string Description
    {
      get
      {
        if (_Description == null)
        {
          if (!_PropertyInfo.IsStatic)
          {
            _Description = "No description";
          }
          else
          {
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(_PropertyInfo.PropertyInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
            //_Description = (attribute != null) ? attribute.Description : string.Empty;
            _Description = (attribute != null) ? attribute.Description : "No description...";
          }
        }
        return _Description;
      }
    }

    public string DisplayName
    {
      get
      {
        if (string.IsNullOrEmpty(_displayName))
        {
          if (!_PropertyInfo.IsStatic)
          {
            return Name;
          }
          else
          {
            DisplayNameAttribute attr = GetAttribute<DisplayNameAttribute>(_PropertyInfo.PropertyInfo);
            _displayName = (attr != null) ? attr.DisplayName : Name;
          }
        }

        return _displayName;
      }
    } private string _displayName;

    public string Category
    {
      get
      {
        if (string.IsNullOrEmpty(_category))
        {
          if (!_PropertyInfo.IsStatic)
          {
            _category = "Misc";
          }
          else
          {
            CategoryAttribute attr = GetAttribute<CategoryAttribute>(_PropertyInfo.PropertyInfo);
            if (attr != null && !string.IsNullOrEmpty(attr.Category))
              _category = attr.Category;
            else
              _category = "Misc";
          }
        }
        return this._category;
      }
    } private string _category;

    public object Value
    {
      get { return _value; }
      set
      {
        if (_value == value) return;
        object originalValue = _value;
        _value = value;

        if (!_PropertyInfo.IsStatic)
        {
          try
          {
            _PropertyInfo.SetValue(_Instance, value);
          }
          catch (Exception ex)
          {
            _value = originalValue;
            _ReadOnly = true;
            OnPropertyChanged("Value");
            OnPropertyChanged("CanWrite");
            OnValueError(ex);
          }
          return;
        }

        try
        {
          Type propertyType = this._PropertyInfo.PropertyType;
          if (((propertyType == typeof(object)) || ((value == null) && propertyType.IsClass)) || ((value != null) && propertyType.IsAssignableFrom(value.GetType())))
          {
            _PropertyInfo.SetValue(_Instance, value, (BindingFlags.NonPublic | BindingFlags.Public), null, null, null);
            OnPropertyChanged("Value");
          }
          else
          {
            try
            {
              if (propertyType.IsEnum)
              {
                object val = Enum.Parse(_PropertyInfo.PropertyType, value.ToString(), false);
                _PropertyInfo.SetValue(_Instance, val, (BindingFlags.NonPublic | BindingFlags.Public), null, null, null);
                OnPropertyChanged("Value");
              }
              else
              {
                TypeConverter tc = TypeConverterHelper.GetConverter(propertyType);
                if (tc != null)
                {
                  object convertedValue = tc.ConvertFrom(value);
                  _PropertyInfo.SetValue(_Instance, convertedValue);
                  OnPropertyChanged("Value");
                }
                else
                {
                  // try direct setting as a string...
                  _PropertyInfo.SetValue(_Instance, value.ToString(), (BindingFlags.NonPublic | BindingFlags.Public), null, null, null);
                  OnPropertyChanged("Value");
                }
              }
            }
            catch (Exception ex)
            {
              _value = originalValue;
              OnPropertyChanged("Value");
              OnValueError(ex);
            }
          }
        }
        catch (MethodAccessException mex)
        {
          _value = originalValue;
          _ReadOnly = true;
          OnPropertyChanged("Value");
          OnPropertyChanged("CanWrite");
          OnValueError(mex);
        }
      }
    } private object _value;

    public Type PropertyType
    {
      get { return _PropertyInfo.PropertyType; }
    }

    public bool CanWrite
    {
      get { return _PropertyInfo.CanWrite && !_ReadOnly; }
    }

    public bool ReadOnly
    {
      get { return _ReadOnly; }
      internal set { _ReadOnly = value; }
    }

    #endregion

    #region Helpers

    public static T GetAttribute<T>(PropertyInfo propertyInfo)
    {
      var attributes = propertyInfo.GetCustomAttributes(typeof(T), true);
      return (attributes.Length > 0) ? attributes.OfType<T>().First() : default(T);
    }

    public T GetAttribute<T>()
    {
      if (_PropertyInfo.IsStatic) GetAttribute<T>(_PropertyInfo.PropertyInfo);
      return default(T);
    }

    #endregion

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
#if DEBUG
      if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
#endif
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region IPropertyFilterTarget Members

    public event EventHandler<PropertyFilterAppliedEventArgs> FilterApplied;

    private bool _MatchesFilter = true;
    public bool MatchesFilter
    {
      get { return _MatchesFilter; }
      private set
      {
        if (_MatchesFilter == value) return;
        _MatchesFilter = value;
        this.OnPropertyChanged("MatchesFilter");
      }
    }

    public void ApplyFilter(PropertyFilter filter)
    {
      this.MatchesFilter = (filter == null) || filter.Match(this);
      this.OnFilterApplied(filter);
    }

    public bool MatchesPredicate(PropertyFilterPredicate predicate)
    {
      if (predicate == null) return false;
      if (!predicate.Match(this.DisplayName)) return predicate.Match(this.PropertyType.Name);
      return true;
    }

    private void OnFilterApplied(PropertyFilter filter)
    {
      if (this.FilterApplied != null)
        this.FilterApplied(this, new PropertyFilterAppliedEventArgs(filter));
    }

    #endregion
  }
}
