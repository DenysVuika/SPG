using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace System.Windows.Controls.PropertyGrid.Dynamic
{
  public class RuntimeObject : DynamicObject, INotifyPropertyChanged
  {
    private readonly IPropertyProvider _propertyProvider;

    public object this[string propertyName]
    {
      get
      {
        object result;
        return _propertyProvider.TryGetPropertyValue(propertyName, out result)
          ? result
          : null;
      }
      set
      {
        _propertyProvider.SetPropertyValue(propertyName, value);
        OnPropertyChanged(propertyName);
        //OnPropertyChanged("Item[]");        
        OnPropertyChanged(string.Format("Item[{0}]", propertyName));
      }
    }

    public RuntimeObject(IPropertyProvider propertyProvider)
    {
      _propertyProvider = propertyProvider;
    }

    public override IEnumerable<string> GetDynamicMemberNames()
    {
      return _propertyProvider.GetMemberNames();
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      return _propertyProvider.TryGetPropertyValue(binder.Name, out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
      var result = _propertyProvider.SetPropertyValue(binder.Name, value);
      if (result)
      {
        OnPropertyChanged(binder.Name);
        //OnPropertyChanged("Item[]");
        OnPropertyChanged(string.Format("Item[{0}]", binder.Name));
      }
      return result;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      var handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
  }  
}
