using System;
using System.Globalization;
using System.Reflection;
using System.Dynamic;

namespace System.Windows.Controls.PropertyGrid.Dynamic
{
  public sealed class DynamicPropertyInfo //: PropertyInfo
  {
    public PropertyInfo PropertyInfo { get; private set; }
    private string _propertyName;
    private Type _propertyType = typeof(object);

    public bool IsStatic
    {
      get { return PropertyInfo != null; }
    }

    public DynamicPropertyInfo(PropertyInfo propertyInfo)
    {
      this.PropertyInfo = propertyInfo;
    }

    public DynamicPropertyInfo(string propertyName, Type propertyType)
    {
      if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");
      _propertyName = propertyName;
      _propertyType = propertyType != null ? propertyType : typeof(object);
    }

    public PropertyAttributes Attributes
    {
      get
      {
        if (IsStatic) return PropertyInfo.Attributes;
        return PropertyAttributes.None;
      }
    }

    public bool CanRead
    {
      get
      {
        if (IsStatic) return PropertyInfo.CanRead;
        return true;
      }
    }

    public bool CanWrite
    {
      get
      {
        if (IsStatic) return PropertyInfo.CanWrite;
        return true;
      }
    }

    public MethodInfo[] GetAccessors(bool nonPublic)
    {
      if (IsStatic) return PropertyInfo.GetAccessors(nonPublic);
      throw new NotImplementedException();
    }

    public MethodInfo GetGetMethod(bool nonPublic)
    {
      if (IsStatic) return PropertyInfo.GetGetMethod(nonPublic);
      throw new NotImplementedException();
    }

    public ParameterInfo[] GetIndexParameters()
    {
      if (IsStatic) return PropertyInfo.GetIndexParameters();
      throw new NotImplementedException();
    }

    public MethodInfo GetSetMethod(bool nonPublic)
    {
      if (IsStatic) return PropertyInfo.GetSetMethod(nonPublic);
      throw new NotImplementedException();
    }

    private void CheckDynamicObject(object obj)
    {
      var dynamicProvider = obj as IDynamicMetaObjectProvider;
      if (dynamicProvider == null) throw new NotSupportedException("This type of object is not supported");
    }

    public object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      if (IsStatic) return PropertyInfo.GetValue(obj, invokeAttr, binder, index, culture);
      CheckDynamicObject(obj);
      return DynamicHelper.GetValue(obj, _propertyName);
    }

    public object GetValue(object obj, object[] index)
    {
      return this.GetValue(obj, BindingFlags.Default, null, index, null);
    }

    public Type PropertyType
    {
      get
      {
        if (IsStatic) return PropertyInfo.PropertyType;
        return _propertyType;
      }
    }

    public void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
      if (IsStatic)
      {
        PropertyInfo.SetValue(obj, value, invokeAttr, binder, index, culture);
        return;
      }

      CheckDynamicObject(obj);
      DynamicHelper.SetValue(obj, _propertyName, value);
    }

    public void SetValue(object obj, object value)
    {
      this.SetValue(obj, value, (BindingFlags.NonPublic | BindingFlags.Public), null, null, null);
    }

    public Type DeclaringType
    {
      get
      {
        if (IsStatic) return PropertyInfo.DeclaringType;
        throw new NotImplementedException();
      }
    }

    public object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      if (IsStatic) return PropertyInfo.GetCustomAttributes(attributeType, inherit);
      throw new NotImplementedException();
    }

    public object[] GetCustomAttributes(bool inherit)
    {
      if (IsStatic) return PropertyInfo.GetCustomAttributes(inherit);
      throw new NotImplementedException();
    }

    public bool IsDefined(Type attributeType, bool inherit)
    {
      if (IsStatic) return PropertyInfo.IsDefined(attributeType, inherit);
      throw new NotImplementedException();
    }

    public string Name
    {
      get
      {
        if (IsStatic) return PropertyInfo.Name;
        return _propertyName;
      }
    }

    public Type ReflectedType
    {
      get
      {
        if (IsStatic) return PropertyInfo.ReflectedType;
        throw new NotImplementedException();
      }
    }
  }
}
