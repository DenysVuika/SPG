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

using System.Windows.Controls.PropertyGrid.PropertyEditing;
using System.Windows.Input;
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid
{
  public sealed class PropertyRow
  {
    public PropertyGridView View { get; private set; }
    public PropertyItem Property { get; private set; }
    public PropertyLabel Label { get; private set; }
    public EditorBase Editor { get; private set; }

    public PropertyRow(PropertyGridView view, PropertyItem property, PropertyLabel label, EditorBase editor)
    {
      Requires.NotNull<PropertyGridView>(view, "view");
      Requires.NotNull<PropertyItem>(property, "property");
      Requires.NotNull<PropertyLabel>(label, "label");
      Requires.NotNull<EditorBase>(editor, "editor");

      this.View = view;
      this.Property = property;
      this.Label = label;
      this.Editor = editor;

      label.Name = "lbl" + property.Name;
      label.Foreground = property.CanWrite ? Brushes.Black : Brushes.Gray;

      label.MouseLeftButtonDown += LabelMouseLeftButtonDown;
      label.MouseLeftButtonUp += LabelMouseLeftButtonUp;

      editor.GotFocus += EditorGotFocus;
      editor.LostFocus += EditorLostFocus;
    }

    private void LabelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      e.Handled = true;
    }

    private void LabelMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      Editor.Focus();
    }

    private void EditorGotFocus(object sender, RoutedEventArgs e)
    {
      this.View.SelectedRow = this;
    }

    private void EditorLostFocus(object sender, RoutedEventArgs e)
    {
      // Removes label selection when editor looses focus
      //Label.Background = Brushes.White;
      //Label.Foreground = Property.CanWrite ? Brushes.Black : Brushes.Gray;

      // Unselects row when editor looses focus
      //this.View.SelectedRow = null;
    }
  }
}
