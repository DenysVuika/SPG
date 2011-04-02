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

namespace System.Windows.Controls.PropertyGrid
{
  [TemplatePart(Name = "PART_Editor", Type = typeof(TextBox))]
  public sealed class PropertyFilterBox : Control
  {
    private TextBox editor;

    public static readonly DependencyProperty TextProperty =
     DependencyProperty.Register("Text", typeof(string), typeof(PropertyFilterBox), new PropertyMetadata(string.Empty, OnTextPropertyChanged));

    private static void OnTextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      PropertyFilterBox filterBox = (PropertyFilterBox)sender;
      filterBox.OnTextChanged();
    }

    public string Text
    {
      get { return (string)GetValue(TextProperty); }
      set { SetValue(TextProperty, value); }
    }

    public event EventHandler TextChanged;

    private void OnTextChanged()
    {
      EventHandler handler = TextChanged;
      if (handler != null) handler(this, EventArgs.Empty);
    }

    public PropertyFilterBox()
    {
      DefaultStyleKey = typeof(PropertyFilterBox);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      this.editor = this.GetTemplateChild("PART_Editor") as TextBox;
      if (this.editor != null) this.editor.TextChanged += EditorTextChanged;
    }

    private void EditorTextChanged(object sender, TextChangedEventArgs e)
    {
      this.Text = ((TextBox)sender).Text;
    }
  }
}
