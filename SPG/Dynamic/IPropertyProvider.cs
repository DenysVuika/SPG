using System.Collections.Generic;

namespace System.Windows.Controls.PropertyGrid.Dynamic
{
  public interface IPropertyProvider
  {
    IEnumerable<string> GetMemberNames();
    bool TryGetPropertyValue(string propertyName, out object value);
    bool SetPropertyValue(string propertyName, object value);
  }
}
