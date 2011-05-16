using System.Collections.Generic;

namespace System.Windows.Controls.PropertyGrid.Dynamic
{
  public class PropertyProvider : IPropertyProvider
  {
    internal Dictionary<string, object> _properties = new Dictionary<string, object>();

    public virtual IEnumerable<string> GetMemberNames()
    {
      return _properties.Keys;
    }

    public virtual bool TryGetPropertyValue(string propertyName, out object value)
    {
      return _properties.TryGetValue(propertyName, out value);
    }

    public virtual bool SetPropertyValue(string propertyName, object value)
    {
      _properties[propertyName] = value;
      return true;
    }
  }
}
