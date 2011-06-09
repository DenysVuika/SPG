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

using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Browser;
using System.Windows.Input;
using System.Dynamic;

namespace System.Windows.Controls.PropertyGrid
{
  using Dynamic;

  /// <summary>
  /// PropertyGrid
  /// </summary>  
  [TemplatePart(Name = "PART_PropertyFilterBox", Type = typeof(PropertyFilterBox))]
  [TemplatePart(Name = "PART_ScrollViewer", Type = typeof(ScrollViewer))]
  [TemplatePart(Name = "PART_PropertyDescriptionBox", Type = typeof(PropertyDescriptionBox))]
  public partial class PropertyGrid : Control
  {
    #region Fields

    private static readonly Type _ThisType = typeof(PropertyGrid);

    private ScrollViewer part_ScrollViewer;
    private PropertyDescriptionBox part_PropertyDescriptionBox;
    private PropertyFilterBox part_PropertyFilterBox;

    private bool loaded = false;
    private bool resetLoadedObject;

    #endregion

    #region Constructors
    /// <summary>
    /// Constructor
    /// </summary>
    public PropertyGrid()
    {
      base.DefaultStyleKey = _ThisType;
      this.Loaded += PropertyGrid_Loaded;
    }
    #endregion

    #region Properties

    #region View

    public static readonly DependencyProperty ViewProperty =
      DependencyProperty.Register("View", typeof(PropertyGridView), _ThisType, new PropertyMetadata(new CategorizedPropertyView(), OnViewChanged));

    private static void OnViewChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      PropertyGrid grid = sender as PropertyGrid;
      // Note for the moment this method will be called twice. OnLoad contains a separate call for default value (when no view was explicitly defined)
      grid.AttachToPropertyView();
    }

    public PropertyGridView View
    {
      get { return (PropertyGridView)GetValue(ViewProperty); }
      set { SetValue(ViewProperty, value); }
    }

    #endregion View

    #region SelectedObject

    public static readonly DependencyProperty SelectedObjectProperty =
      DependencyProperty.Register("SelectedObject", typeof(object), _ThisType, new PropertyMetadata(null, OnSelectedObjectChanged));

    public object SelectedObject
    {
      get { return base.GetValue(SelectedObjectProperty); }
      set { base.SetValue(SelectedObjectProperty, value); }
    }

    private static void OnSelectedObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      PropertyGrid propertyGrid = d as PropertyGrid;
      if (propertyGrid != null)
      {
        if (!propertyGrid.loaded)
          propertyGrid.resetLoadedObject = true;
        else if (null != e.NewValue)
          propertyGrid.ResetObject(e.NewValue);
        else
          propertyGrid.View.Reset();
      }
    }
    #endregion SelectedObject

    #region ObjectDisplayName

    public static readonly DependencyProperty ObjectDisplayNameProperty =
      DependencyProperty.Register("ObjectDisplayName", typeof(string), typeof(PropertyGrid), new PropertyMetadata(string.Empty));

    public string ObjectDisplayName
    {
      get { return (string)GetValue(ObjectDisplayNameProperty); }
      set { SetValue(ObjectDisplayNameProperty, value); }
    } 

    #endregion

    #region PropertyFilterVisibility

    public static readonly DependencyProperty PropertyFilterVisibilityProperty =
      DependencyProperty.Register("PropertyFilterVisibility", typeof(Visibility), typeof(PropertyGrid), new PropertyMetadata(Visibility.Visible));

    public Visibility PropertyFilterVisibility
    {
      get { return (Visibility)GetValue(PropertyFilterVisibilityProperty); }
      set { SetValue(PropertyFilterVisibilityProperty, value); }
    }

    #endregion

    #endregion

    #region Overrides
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.part_ScrollViewer = (ScrollViewer)this.GetTemplateChild("PART_SrollViewer");
      this.part_PropertyDescriptionBox = this.GetTemplateChild("PART_PropertyDescriptionBox") as PropertyDescriptionBox;
      this.part_PropertyFilterBox = this.GetTemplateChild("PART_PropertyFilterBox") as PropertyFilterBox;

      if (this.part_PropertyFilterBox != null) this.part_PropertyFilterBox.TextChanged += FlterBoxTextChanged;

      loaded = true;

      if (resetLoadedObject)
      {
        resetLoadedObject = false;
        this.ResetObject(this.SelectedObject);
      }
    }

    private void FlterBoxTextChanged(object sender, EventArgs e)
    {
      if (this.View == null) return;
      this.View.ApplyFilter(new PropertyFilter(((PropertyFilterBox)sender).Text));
    }
    #endregion

    #region Methods

    private void SetObject(object obj)
    {
      List<PropertyItem> props = new List<PropertyItem>();
      // Parse the objects properties
      props = PropertyGrid.ParseObject(obj);
      View.SetProperties(props);
      ObjectDisplayName = string.Empty;
      if (obj == null) return;

      var type = obj.GetType();

      var displayName = Attribute.GetCustomAttribute(type, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
      ObjectDisplayName = displayName != null ? displayName.DisplayName : type.Name;
    }

    private void ResetObject(object obj)
    {
      SetPropertyDescription(null);
      this.View.Reset();
      this.SetObject(obj);
    }

    public void Reload()
    {
      ResetObject(SelectedObject);
    }

    private void AttachWheelEvents()
    {
      HtmlPage.Window.AttachEvent("DOMMouseScroll", OnMouseWheel);
      HtmlPage.Window.AttachEvent("onmousewheel", OnMouseWheel);
      HtmlPage.Document.AttachEvent("onmousewheel", OnMouseWheel);
    }

    private void DetachWheelEvents()
    {
      HtmlPage.Window.DetachEvent("DOMMouseScroll", OnMouseWheel);
      HtmlPage.Window.DetachEvent("onmousewheel", OnMouseWheel);
      HtmlPage.Document.DetachEvent("onmousewheel", OnMouseWheel);
    }

    private void AttachToPropertyView()
    {
      if (this.View != null)
      {
        DetachFromPropertyView();
        this.View.SelectedRowChanged += OnSelectedRowChanged;
      }
    }

    private void DetachFromPropertyView()
    {
      if (this.View != null) this.View.SelectedRowChanged -= OnSelectedRowChanged;
    }

    static List<PropertyItem> ParseObject(object objItem)
    {
      if (null == objItem)
        return new List<PropertyItem>();

      var metaProvider = objItem as DynamicObject;
      if (metaProvider != null)
        return ParseDynamicObject(metaProvider);

      List<PropertyItem> pc = new List<PropertyItem>();
      Type t = objItem.GetType();
      var props = t.GetProperties();

      foreach (PropertyInfo pinfo in props)
      {
        bool isBrowsable = true;
        BrowsableAttribute b = PropertyItem.GetAttribute<BrowsableAttribute>(pinfo);
        if (null != b)
          isBrowsable = b.Browsable;
        if (isBrowsable)
        {
          EditorBrowsableAttribute eb = PropertyItem.GetAttribute<EditorBrowsableAttribute>(pinfo);
          if (null != eb && eb.State == EditorBrowsableState.Never)
            isBrowsable = false;
        }
        if (isBrowsable)
        {
          bool readOnly = false;
          ReadOnlyAttribute attr = PropertyItem.GetAttribute<ReadOnlyAttribute>(pinfo);
          if (attr != null)
            readOnly = attr.IsReadOnly;

          try
          {
            object value = pinfo.GetValue(objItem, null);
            PropertyItem prop = new PropertyItem(objItem, value, new DynamicPropertyInfo(pinfo), readOnly);
            pc.Add(prop);
          }
          catch { }
        }
      }

      return pc;
    }

    static List<PropertyItem> ParseDynamicObject(DynamicObject target)
    {
      var result = new List<PropertyItem>();

      foreach (var propertyName in target.GetDynamicMemberNames())
      {
        var value = DynamicHelper.GetValue(target, propertyName);
        var propertyType = value != null ? value.GetType() : typeof(object);
        var property = new PropertyItem(target, value, new DynamicPropertyInfo(propertyName, propertyType), false);
        result.Add(property);
      }

      return result;
    }

    private void SetPropertyDescription(string text)
    {
      this.part_PropertyDescriptionBox.Text = text;
    }

    #endregion

    #region Event Handlers
    private void PropertyGrid_Loaded(object sender, RoutedEventArgs e)
    {
      this.MouseEnter += new MouseEventHandler(PropertyGrid_MouseEnter);
      this.MouseLeave += new MouseEventHandler(PropertyGrid_MouseLeave);

      AttachToPropertyView();
    }

    private void PropertyGrid_MouseEnter(object sender, MouseEventArgs e)
    {
      this.AttachWheelEvents();
    }

    private void PropertyGrid_MouseLeave(object sender, MouseEventArgs e)
    {
      this.DetachWheelEvents();
    }

    // Thanks to Gavin Wignall
    // http://www.silverlightbuzz.com/2009/05/19/zoom-in-and-out-using-the-mouse-wheel-in-silverlight/
    // based on Scott Rogers comments
    private void OnMouseWheel(object sender, HtmlEventArgs args)
    {
      double mouseDelta = 0;
      ScriptObject e = args.EventObject;

      // IE, Google Chrome and Opera
      if (e.GetProperty("wheelDelta") != null)
      {
        mouseDelta = ((double)e.GetProperty("wheelDelta"));
      }
      // Mozilla and Safari
      else if (e.GetProperty("detail") != null)
      {
        mouseDelta = ((double)e.GetProperty("detail"));
      }

      mouseDelta = Math.Sign(mouseDelta);
      mouseDelta = mouseDelta * -1;
      mouseDelta = mouseDelta * 40; // Just a guess at an acceleration
      mouseDelta = part_ScrollViewer.VerticalOffset + mouseDelta;
      part_ScrollViewer.ScrollToVerticalOffset(mouseDelta);
    }

    private void OnSelectedRowChanged(object sender, EventArgs e)
    {
      this.part_PropertyDescriptionBox.Text = (View != null && View.SelectedRow != null) ? View.SelectedRow.Property.Description : string.Empty;
    }
    #endregion
  }
}
