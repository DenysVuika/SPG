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
using System.Windows.Controls.PropertyGrid.PropertyEditing;
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid
{
  public abstract class PropertyGridView : Grid
  {
    public event EventHandler SelectedRowChanged;

    protected virtual void OnSelectedRowChanged()
    {
      EventHandler handler = SelectedRowChanged;
      if (handler != null) handler(this, EventArgs.Empty);
    }

    protected List<PropertyRow> rows = new List<PropertyRow>();

    public virtual Brush CommonBackground
    {
      get { return EditorBase.DefaultCommonBackground; }
    }

    public virtual Brush FocusedBackground
    {
      get { return EditorBase.DefaultFocusedBackground; }
    }

    private PropertyRow _SelectedRow;
    public PropertyRow SelectedRow
    {
      get { return _SelectedRow; }
      set
      {
        if (_SelectedRow != null)
        {
          // remove selection
          _SelectedRow.Label.Background = Brushes.White;
          _SelectedRow.Label.Foreground = _SelectedRow.Property.CanWrite ? Brushes.Black : Brushes.Gray;
        }

        _SelectedRow = value;

        if (_SelectedRow != null)
        {
          // set selection
          _SelectedRow.Label.Background = EditorBase.DefaultFocusedBackground;
          _SelectedRow.Label.Foreground = Brushes.White;
        }

        OnSelectedRowChanged();
      }
    }

    public abstract void SetProperties(IEnumerable<PropertyItem> properties);
    public abstract void Reset();
    public abstract void ApplyFilter(PropertyFilter filter);
  }
}
