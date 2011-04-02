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
  public class StringEditor : EditorBase
  {
    readonly TextBox textBox;

    public StringEditor(PropertyLabel label, PropertyItem property)
      : base(property)
    {
      if (property.PropertyType == typeof(Char))
      {
        if ((char)property.Value == '\0')
          property.Value = "";
      }

      property.PropertyChanged += property_PropertyChanged;
      property.ValueError += property_ValueError;

      textBox = new TextBox
      {
        Height = 20,
        Foreground = Property.CanWrite ? Brushes.Black : Brushes.Gray,
        BorderThickness = new Thickness(0),
        Margin = new Thickness(0),
        IsReadOnly = !Property.CanWrite
      };

      if (null != property.Value)
        textBox.Text = property.Value.ToString();

      if (Property.CanWrite)
        textBox.TextChanged += Control_TextChanged;

      Content = textBox;
      GotFocus += StringValueEditor_GotFocus;
    }

    private static void property_ValueError(object sender, ExceptionEventArgs e)
    {
      MessageBox.Show(e.EventException.Message);
    }

    private void property_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Value")
        textBox.Text = (Property.Value != null) ? Property.Value.ToString() : string.Empty;

      if (e.PropertyName == "CanWrite")
      {
        if (!Property.CanWrite)
          textBox.TextChanged -= Control_TextChanged;
        else
          textBox.TextChanged += Control_TextChanged;

        textBox.IsReadOnly = !Property.CanWrite;
        textBox.Foreground = Property.CanWrite ? Brushes.Black : Brushes.Gray;
      }
    }

    private void StringValueEditor_GotFocus(object sender, RoutedEventArgs e)
    {
      textBox.Focus();
    }

    private void Control_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (Property.CanWrite)
        Property.Value = textBox.Text;
    }
  }
}
