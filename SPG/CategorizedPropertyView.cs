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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace System.Windows.Controls.PropertyGrid
{
  public class CategorizedPropertyView : PropertyGridView
  {
    private static GridLength DefaultWidth = new GridLength(1, GridUnitType.Star);
    private ColumnDefinition expanderColumn = new ColumnDefinition { Width = new GridLength(16) };
    private ColumnDefinition labelColumn = new ColumnDefinition { Width = DefaultWidth, MinWidth = 50 };
    private ColumnDefinition valueColumn = new ColumnDefinition();

    #region LabelWidth

    /// <summary>
    /// Identifies the <see cref="DefaultLabelWidth"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LabelWidthProperty =
     DependencyProperty.Register("LabelWidth", typeof(GridLength), typeof(PropertyGridView), new PropertyMetadata(DefaultWidth, OnDefaultLabelWidthChanged));

    /// <summary>
    /// Gets or sets the Default Width for the labels. This is a dependency property.
    /// </summary>
    public GridLength LabelWidth
    {
      get { return (GridLength)base.GetValue(LabelWidthProperty); }
      set { base.SetValue(LabelWidthProperty, value); }
    }

    private static void OnDefaultLabelWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      CategorizedPropertyView view = (CategorizedPropertyView)sender;
      view.labelColumn.Width = (GridLength)e.NewValue;
    }

    #endregion LabelWidth

    public CategorizedPropertyView()
    {
      this.VerticalAlignment = VerticalAlignment.Top;
      this.ColumnDefinitions.Add(expanderColumn);
      this.ColumnDefinitions.Add(labelColumn);
      this.ColumnDefinitions.Add(valueColumn);
    }

    #region Category Builder

    protected virtual void CreateCategoryRow(string category, ref int rowIndex)
    {
      rowIndex++;
      this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(21) });

      #region Column 0 - Margin

      FrameworkElement brd = CreateCategoryMargin(
        category,
        GetCategoryCollapseImage(Visibility.Visible),
        GetCategoryExpandImage(Visibility.Collapsed));

      this.Children.Add(brd);
      Grid.SetRow(brd, rowIndex);
      Grid.SetColumn(brd, 0);

      #endregion

      #region Column 1 & 2 - Category Header

      brd = CreateCategoryHeader(category);
      this.Children.Add(brd);
      Grid.SetRow(brd, rowIndex);
      Grid.SetColumn(brd, 1);
      Grid.SetColumnSpan(brd, 2);

      #endregion
    }

    protected virtual FrameworkElement CreateCategoryMargin(string category, Image collapseImage, Image expandImage)
    {
      Border border = new Border
      {
        Background = this.CommonBackground,
        Child = new StackPanel
        {
          Name = Guid.NewGuid().ToString("N"),
          HorizontalAlignment = HorizontalAlignment.Center,
          VerticalAlignment = VerticalAlignment.Center,
          Tag = category,
          Children = { collapseImage, expandImage }
        }
      };

      return border;
    }

    protected virtual FrameworkElement CreateCategoryHeader(string category)
    {
      Border border = new Border
      {
        Background = CommonBackground,
        Child = new TextBlock
        {
          Name = Guid.NewGuid().ToString("N"),
          Text = category,
          VerticalAlignment = VerticalAlignment.Center,
          HorizontalAlignment = HorizontalAlignment.Left,
          Foreground = Brushes.Gray,
          Margin = new Thickness(3, 0, 0, 0),
          FontWeight = FontWeights.Bold,
          //FontFamily = new FontFamily("Arial Narrow")
        }
      };

      Canvas.SetZIndex(border, 1);
      return border;
    }

    protected virtual Image GetCategoryExpandImage(Visibility visibility)
    {
      Image img = GetImage("/System.Windows.Controls.PropertyGrid;component/resources/CategoryCollapsed.png");
      img.Visibility = visibility;
      img.MouseLeftButtonUp += OnCategoryExpand;
      return img;
    }

    protected virtual Image GetCategoryCollapseImage(Visibility visibility)
    {
      Image img = GetImage("/System.Windows.Controls.PropertyGrid;component/resources/CategoryExpanded.png");
      img.Visibility = visibility;
      img.MouseLeftButtonUp += OnCategoryCollapse;
      return img;
    }

    protected virtual void OnCategoryExpand(object sender, MouseButtonEventArgs e)
    {
      FrameworkElement ctl = sender as FrameworkElement;
      Panel stp = ctl.Parent as Panel;
      string tagValue = (string)stp.Tag;
      stp.Children[0].Visibility = Visibility.Visible;
      stp.Children[1].Visibility = Visibility.Collapsed;
      this.Dispatcher.BeginInvoke(delegate()
      {
        ToggleCategoryVisible(true, tagValue);
      });
    }

    protected virtual void OnCategoryCollapse(object sender, MouseButtonEventArgs e)
    {
      FrameworkElement ctl = sender as FrameworkElement;
      Panel stp = ctl.Parent as Panel;
      string tagValue = (string)stp.Tag;
      stp.Children[0].Visibility = Visibility.Collapsed;
      stp.Children[1].Visibility = Visibility.Visible;
      this.Dispatcher.BeginInvoke(delegate()
      {
        ToggleCategoryVisible(false, tagValue);
      });
    }

    protected virtual void ToggleCategoryVisible(bool show, string tagValue)
    {
      foreach (FrameworkElement element in this.Children)
      {
        object value = element.Tag;
        if (value != null)
        {
          string tag = (string)value;
          if (tagValue == tag)
            element.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }
      }
    }

    #endregion Category Builder

    #region Property Builder

    protected virtual void CreatePropertyRow(PropertyItem property, ref int rowIndex)
    {
      #region Create Display Objects
      PropertyLabel label = CreateLabel(property.DisplayName);
      EditorBase editor = EditorService.GetEditor(property, label);
      if (null == editor) return;
      this.rows.Add(new PropertyRow(this, property, label, editor));
      #endregion

      rowIndex++;
      this.RowDefinitions.Add(new RowDefinition());
      string category = property.Category;

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

    private static Image GetImage(string imageUri)
    {
      return new Image()
      {
        Name = Guid.NewGuid().ToString("N"),
        Source = new BitmapImage(new Uri(imageUri, UriKind.Relative)),
        Height = 9,
        Width = 9,
        VerticalAlignment = VerticalAlignment.Center,
        HorizontalAlignment = HorizontalAlignment.Center
      };
    }

    public override void SetProperties(IEnumerable<PropertyItem> properties)
    {
      Reset();

      if (properties == null) return;

      int rowCount = -1;

      var categories = (from p in properties
                        orderby p.Category
                        select p.Category).Distinct();

      foreach (string category in categories)
      {
        CreateCategoryRow(category, ref rowCount);

        var items = from p in properties
                    where p.Category == category
                    orderby p.DisplayName
                    select p;

        foreach (var item in items)
          CreatePropertyRow(item, ref rowCount);
      }

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

    public override void ApplyFilter(PropertyFilter filter)
    {
      //throw new NotImplementedException();
    }
  }
}
