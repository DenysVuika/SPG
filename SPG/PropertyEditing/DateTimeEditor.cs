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
using System.Diagnostics;
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.PropertyEditing
{
  public class DateTimeEditor : EditorBase
  {
    #region Fields
    private object currentValue;
    private bool showingDatePicker;
    private readonly StackPanel contentPanel;
    protected TextBox textBox;
    protected DatePicker datePicker;
    #endregion

    #region Constructors
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="label"></param>
    /// <param name="property"></param>
    public DateTimeEditor(PropertyLabel label, PropertyItem property)
      : base(property)
    {
      currentValue = property.Value;
      property.PropertyChanged += property_PropertyChanged;
      property.ValueError += property_ValueError;

      contentPanel = new StackPanel();
      this.Content = contentPanel;

      datePicker = new DatePicker
      {
        Visibility = Visibility.Visible,
        Margin = new Thickness(0),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch
      };

      datePicker.CalendarOpened += dtp_CalendarOpened;
      datePicker.CalendarClosed += dtp_CalendarClosed;
      datePicker.LostFocus += dtp_LostFocus;
      contentPanel.Children.Add(datePicker);
      datePicker.Focus();

      this.ShowTextBox();
    }
    #endregion

    #region Overrides
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnGotFocus(RoutedEventArgs e)
    {
      if (showingDatePicker) return;

      base.OnGotFocus(e);

      if (this.Property.CanWrite)
        this.ShowDatePicker();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLostFocus(RoutedEventArgs e)
    {
      Debug.WriteLine("DateTimeValueEditor : OnLostFocus");

      if (showingDatePicker)
        return;

      base.OnLostFocus(e);
    }
    #endregion

    #region Methods
    void ShowDatePicker()
    {
      if (textBox == null) return;

      datePicker.SelectedDateChanged -= dtp_SelectedDateChanged;
      datePicker.Visibility = Visibility.Visible;
      datePicker.Focus();

      textBox.Visibility = Visibility.Collapsed;
      contentPanel.Children.Remove(textBox);
      textBox = null;

      datePicker.SelectedDate = (DateTime)currentValue;
      datePicker.SelectedDateChanged += dtp_SelectedDateChanged;

    }

    void ShowTextBox()
    {
      if (textBox != null) return;

      textBox = new TextBox
      {
        Height = 20,
        BorderThickness = new Thickness(0),
        Margin = new Thickness(0),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        IsReadOnly = !this.Property.CanWrite,
        Foreground = this.Property.CanWrite ? Brushes.Black : Brushes.Gray,
        Text = ((DateTime)this.Property.Value).ToShortDateString()
      };

      contentPanel.Children.Add(textBox);
      showingDatePicker = false;
      datePicker.Visibility = Visibility.Collapsed;
    }
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
        if (!this.Property.CanWrite && showingDatePicker)
          ShowTextBox();
      }
    }

    private void dtp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
      currentValue = e.AddedItems[0];
      this.Property.Value = currentValue;
    }

    private void dtp_CalendarOpened(object sender, RoutedEventArgs e)
    {
      showingDatePicker = true;
    }

    private void dtp_CalendarClosed(object sender, RoutedEventArgs e)
    {
      datePicker.Focus();
    }

    private void dtp_LostFocus(object sender, RoutedEventArgs e)
    {
      currentValue = datePicker.SelectedDate;
      this.Property.Value = currentValue;
      if (datePicker.IsDropDownOpen) return;
      ShowTextBox();
    }

    #endregion
  }
}
