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

using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.PropertyEditing
{
  public abstract class EditorBase : ContentControl, IPropertyValueEditor
  {
    public static readonly Brush DefaultCommonBackground = new SolidColorBrush(Color.FromArgb(255, 233, 236, 250));
    public static readonly Brush DefaultFocusedBackground = new SolidColorBrush(Color.FromArgb(255, 94, 170, 255));

    protected EditorBase() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="label">The associated label for this Editor control</param>
    /// <param name="property">The associated PropertyItem for this control</param>
    public EditorBase(PropertyItem property)
    {
      this.Name = "textBox" + property.Name;
      this.Property = property;
      this.BorderThickness = new Thickness(0);
      this.Margin = new Thickness(0);
      this.HorizontalAlignment = HorizontalAlignment.Stretch;
      this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
    }

    #region IPropertyValueEditor Members

    /// <summary>
    /// Gets the associated PropertyItem for this control
    /// </summary>
    public PropertyItem Property { get; private set; }

    #endregion
  }
}
