using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.PropertyGrid.Dynamic;

namespace SPG.Samples.DynamicObjects
{
  public partial class MainPage : UserControl
  {
    private IPropertyProvider provider = new PropertyProvider();

    public MainPage()
    {
      InitializeComponent();

      // Manipulate properties on the Property Provider level
      provider.SetPropertyValue("Hello", "World");

      // manipulate object via 'dynamic'
      dynamic context = new RuntimeObject(provider);
      context.FirstName = "Denys";
      context.LastName = "Vuika";
      context.IsEnabled = true;

      propertyGrid.SelectedObject = context;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      // create a new property
      ((dynamic)propertyGrid.SelectedObject).NewProperty = "Hello world";

      // reload control in order to apply latest changes
      propertyGrid.Reload();
    }
  }
}
