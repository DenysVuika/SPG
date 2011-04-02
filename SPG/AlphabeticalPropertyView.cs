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
using System.Linq;
using System.Windows.Controls.PropertyGrid.PropertyEditing;
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid
{
  public class AlphabeticalPropertyView : PropertyGridView
  {
    private static GridLength DefaultWidth = new GridLength(1, GridUnitType.Star);
    private ColumnDefinition expanderColumn = new ColumnDefinition { Width = new GridLength(16) };
    private ColumnDefinition labelColumn = new ColumnDefinition { Width = DefaultWidth, MinWidth = 50 };
    private ColumnDefinition valueColumn = new ColumnDefinition();

    private IEnumerable<PropertyItem> properties;

    #region LabelWidth

    /// <summary>
    /// Identifies the <see cref="LabelWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelWidthProperty =
     DependencyProperty.Register("LabelWidth", typeof(GridLength), typeof(AlphabeticalPropertyView), new PropertyMetadata(DefaultWidth, OnLabelWidthChanged));

    /// <summary>
    /// Gets or sets the Default Width for the labels. This is a dependency property.
    /// </summary>
    public GridLength LabelWidth
    {
      get { return (GridLength)base.GetValue(LabelWidthProperty); }
      set { base.SetValue(LabelWidthProperty, value); }
    }

    private static void OnLabelWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      AlphabeticalPropertyView view = (AlphabeticalPropertyView)sender;
      view.labelColumn.Width = (GridLength)e.NewValue;
    }

    #endregion LabelWidth

    public AlphabeticalPropertyView()
    {
      this.VerticalAlignment = VerticalAlignment.Top;
      this.ColumnDefinitions.Add(expanderColumn);
      this.ColumnDefinitions.Add(labelColumn);
      this.ColumnDefinitions.Add(valueColumn);
    }

    #region Property Builder

    protected virtual void CreatePropertyRow(PropertyItem property, ref int rowIndex)
    {
      #region Create Display Objects
      PropertyLabel label = CreateLabel(property.DisplayName);
      EditorBase editor = EditorService.GetEditor(property, label);
      if (editor == null) return;

      this.rows.Add(new PropertyRow(this, property, label, editor));
      #endregion

      rowIndex++;
      this.RowDefinitions.Add(new RowDefinition());
      //string category = property.Category;
      string category = property.Name;

      #region Column 0 - Margin
      FrameworkElement brd = GetItemMargin(category);
      this.Children.Add(brd);
      Grid.SetRow(brd, rowIndex);
      Grid.SetColumn(brd, 0);
      #endregion

      #region Column 1 - Label
      brd = GetItemLabel(label, category);
      this.Children.Add(brd);
      Grid.SetRow(brd, rowIndex);
      Grid.SetColumn(brd, 1);
      #endregion

      #region Column 2 - Editor
      brd = GetItemEditor(editor, category);
      this.Children.Add(brd);
      Grid.SetRow(brd, rowIndex);
      Grid.SetColumn(brd, 2);
      #endregion
    }

    protected virtual PropertyLabel CreateLabel(string displayName)
    {
      return new PropertyLabel()
      {
        Name = Guid.NewGuid().ToString("N"),
        Content = new TextBlock
        {
          Text = displayName,
          Margin = new Thickness(0)
        }
      };
    }

    protected virtual FrameworkElement GetItemMargin(string category)
    {
      return new Border()
      {
        Name = Guid.NewGuid().ToString("N"),
        Margin = new Thickness(0),
        BorderThickness = new Thickness(0),
        Background = CommonBackground,
        Tag = category
      };
    }

    protected virtual FrameworkElement GetItemLabel(PropertyLabel label, string category)
    {
      return new Border()
      {
        Name = Guid.NewGuid().ToString("N"),
        Margin = new Thickness(0),
        BorderBrush = CommonBackground,
        BorderThickness = new Thickness(0, 0, 1, 1),
        Child = label,
        Tag = category
      };
    }

    protected virtual FrameworkElement GetItemEditor(EditorBase editor, string category)
    {
      return new Border()
      {
        Name = Guid.NewGuid().ToString("N"),
        Margin = new Thickness(1, 0, 0, 0),
        BorderThickness = new Thickness(0, 0, 0, 1),
        BorderBrush = CommonBackground,
        Child = editor,
        Tag = category
      };
    }

    protected virtual void AddGridSplitter(int rowCount)
    {
      GridSplitter gsp = new GridSplitter()
      {
        IsTabStop = false,
        HorizontalAlignment = HorizontalAlignment.Left,
        VerticalAlignment = VerticalAlignment.Stretch,
        Background = Brushes.Transparent,
        ShowsPreview = false,
        Width = 2
      };
      Grid.SetColumn(gsp, 2);
      Grid.SetRowSpan(gsp, rowCount);
      Canvas.SetZIndex(gsp, 1);
      this.Children.Add(gsp);
    }

    #endregion

    public override void SetProperties(IEnumerable<PropertyItem> properties)
    {
      this.properties = properties;

      Reset();

      if (properties == null) return;

      int rowCount = -1;

      foreach (var prop in properties.OrderBy(p => p.DisplayName))
        CreatePropertyRow(prop, ref rowCount);

      if (rowCount++ > 0)
        AddGridSplitter(rowCount);
    }

    public override void Reset()
    {
      // TODO: Should be moved to base type
      this.rows.Clear();
      this.Children.Clear();
      this.RowDefinitions.Clear();
    }

    // TODO: Optimize performance
    public override void ApplyFilter(PropertyFilter filter)
    {
      foreach (PropertyItem property in properties)
      {
        if (PropertyMatchesFilter(filter, property))
        {
          foreach (FrameworkElement element in Children)
          {
            if (!object.Equals(element.Tag, property.Name)) continue;
            element.Visibility = Visibility.Visible;
          }
          continue;
        }

        foreach (FrameworkElement element in Children)
        {
          if (!object.Equals(element.Tag, property.Name)) continue;
          element.Visibility = Visibility.Collapsed;
        }
      }
    }

    private bool PropertyMatchesFilter(PropertyFilter filter, PropertyItem entry)
    {
      entry.ApplyFilter(filter);
      return entry.MatchesFilter;
    }
  }
}
