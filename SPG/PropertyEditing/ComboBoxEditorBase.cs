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
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.PropertyEditing
{
  /// <summary>
  /// Basic ComboBox-like editor.
  /// </summary>
  public abstract class ComboBoxEditorBase : EditorBase
  {
    #region Fields
    private object currentValue;
    private bool showingCBO;
    private StackPanel pnl;
    protected TextBox txt;
    protected ComboBox cbo;
    #endregion

    #region Constructors
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="label"></param>
    /// <param name="property"></param>
    public ComboBoxEditorBase(PropertyLabel label, PropertyItem property)
      : base(property)
    {

      currentValue = property.Value;
      property.PropertyChanged += property_PropertyChanged;
      property.ValueError += property_ValueError;

      cbo = new ComboBox
      {
        Visibility = Visibility.Collapsed,
        Margin = new Thickness(0),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };

      cbo.DropDownOpened += cbo_DropDownOpened;
      cbo.LostFocus += cbo_LostFocus;

      this.InitializeCombo();

      pnl = new StackPanel();
      pnl.Children.Add(cbo);

      this.ShowTextBox();

      this.Content = pnl;
    }
    #endregion

    #region Overrides
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnGotFocus(RoutedEventArgs e)
    {
      if (showingCBO) return;

      base.OnGotFocus(e);

      if (this.Property.CanWrite)
        this.ShowComboBox();

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLostFocus(RoutedEventArgs e)
    {
      if (cbo.IsDropDownOpen) return;
      base.OnLostFocus(e);
    }
    #endregion

    #region Methods
    void ShowComboBox()
    {
      if (null == txt)
        return;

      cbo.Visibility = Visibility.Visible;
      cbo.Focus();

      #region Sync current value
      this.cbo.SelectionChanged -= cbo_SelectionChanged;
      for (int i = 0; i < this.cbo.Items.Count; i++)
      {
        object val = this.cbo.Items[i];
        if (val.Equals(currentValue) || val.ToString() == currentValue.ToString())
        {
          this.cbo.SelectedIndex = i;
          break;
        }
      }
      this.cbo.SelectionChanged += cbo_SelectionChanged;
      #endregion

      txt.Visibility = Visibility.Collapsed;
      pnl.Children.Remove(txt);
      txt = null;
    }

    private void ShowTextBox()
    {
      if (null != txt) return;

      txt = new TextBox
      {
        Height = 20,
        BorderThickness = new Thickness(0),
        Margin = new Thickness(0),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        Text = currentValue.ToString(),
        IsReadOnly = !this.Property.CanWrite,
        Foreground = (this.Property.CanWrite) ? Brushes.Black : Brushes.Gray
      };

      pnl.Children.Add(txt);
      cbo.Visibility = Visibility.Collapsed;
      showingCBO = false;
    }

    protected virtual void LoadItems(IEnumerable<object> items)
    {
      foreach (var item in items)
        this.cbo.Items.Add(item.ToString());
    }

    #endregion

    #region Abstract
    /// <summary>
    /// Initalize the combo box by calling LoadItems passing the list of items for the combobox
    /// </summary>
    public abstract void InitializeCombo();
    #endregion

    #region Event Handlers

    private void property_ValueError(object sender, ExceptionEventArgs e)
    {
      MessageBox.Show(e.EventException.Message);
    }

    private void property_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Value")
        currentValue = this.Property.Value;

      if (e.PropertyName == "CanWrite")
      {
        if (!this.Property.CanWrite && showingCBO)
          ShowTextBox();
      }
    }

    private void cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      currentValue = e.AddedItems[0];
      this.Property.Value = currentValue;
    }

    private void cbo_DropDownOpened(object sender, EventArgs e)
    {
      showingCBO = true;
    }

    private void cbo_LostFocus(object sender, RoutedEventArgs e)
    {
      if (cbo.IsDropDownOpen) return;
      ShowTextBox();
    }

    #endregion
  }
}
