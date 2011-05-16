using System;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Windows.Controls.PropertyGrid.Dynamic
{
  public class DynamicHelper
  {
    public static object GetValue(object context, string propertyName)
    {
      var callsite = CallSite<Func<CallSite, object, object>>.Create(
        Binder.GetMember(
          CSharpBinderFlags.None, propertyName, typeof(DynamicHelper),
          new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) }
          ));
      var value = callsite.Target(callsite, context);
      return value;
    }

    public static void SetValue(object context, string propertyName, object value)
    {
      var callsite = CallSite<Func<CallSite, object, object, object>>.Create(
        Binder.SetMember(
          CSharpBinderFlags.None, propertyName, null,
          new[] 
          { 
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) 
          }
          ));

      callsite.Target(callsite, context, value);
    }
  }
}
