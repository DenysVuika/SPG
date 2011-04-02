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
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.PropertyEditing
{
  public class BrushEditor : EditorBase
  {
    private object currentValue;
    protected TextBox textBox;

    private readonly StackPanel contentPanel;

    public BrushEditor(PropertyLabel label, PropertyItem property)
      : base(property)
    {
      currentValue = property.Value;
      property.PropertyChanged += property_PropertyChanged;
      property.ValueError += property_ValueError;
      contentPanel = new StackPanel();
      this.Content = contentPanel;
      ShowTextBox();
    }

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
        if (!this.Property.CanWrite)
          ShowTextBox();
      }
    }

    private void ShowTextBox()
    {
      if (textBox != null) return;
      if (Property.Value == null) return;
      textBox = new TextBox
      {
        Height = 20,
        BorderThickness = new Thickness(0),
        Margin = new Thickness(0),
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        IsReadOnly = !this.Property.CanWrite,
        Foreground = this.Property.CanWrite ? Brushes.Black : Brushes.Gray,

      };
      if (Property.Value is SolidColorBrush)
      {
        Color c = (Property.Value as SolidColorBrush).Color;
        textBox.Text = c.ToString();
      }
      if (Property.CanWrite)
        textBox.LostFocus += TextBoxOnLostFocus;
      contentPanel.Children.Add(textBox);
    }

    private void TextBoxOnLostFocus(object sender, RoutedEventArgs args)
    {
      if (Property.CanWrite)
      {
        Color c = getColorFromHexString(textBox.Text.Trim());
        // Color c = (ColorExtension)textBox.Text.Trim();
        Property.Value = new SolidColorBrush(c);
      }
    }

    public Color getColorFromHexString(string s)
    {
      if (s.StartsWith("#"))
        s = s.Remove(0, 1);
      byte a;
      int shift = 0;
      if (s.Length == 6)
        a = 255;
      else
      {
        a = System.Convert.ToByte(s.Substring(0, 2), 16);
        shift = 2;
      }
      byte r = System.Convert.ToByte(s.Substring(shift, 2), 16);
      byte g = System.Convert.ToByte(s.Substring(shift + 2, 2), 16);
      byte b = System.Convert.ToByte(s.Substring(shift + 4, 2), 16);
      return Color.FromArgb(a, r, g, b);
    }
  }  
}