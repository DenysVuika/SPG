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
using System.Reflection;

namespace System.Windows.Controls.PropertyGrid.ComponentModel
{
  public static class EnumHelper
  {
    private static Dictionary<Type, EnumWrapper[]> _enumCache = new Dictionary<Type, EnumWrapper[]>();

    public static T[] GetValues<T>()
    {
      Type enumType = typeof(T);

      if (!enumType.IsEnum)
      {
        throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
      }

      List<T> values = new List<T>();

      var fields = from field in enumType.GetFields()
                   where field.IsLiteral
                   select field;

      foreach (FieldInfo field in fields)
      {
        object value = field.GetValue(enumType);
        values.Add((T)value);
      }

      return values.ToArray();
    }
    public static List<object> GetValues(Type enumType)
    {
      if (!enumType.IsEnum)
        throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");

      List<object> values = new List<object>();

      var fields = from field in enumType.GetFields()
                   where field.IsLiteral
                   select field;

      foreach (FieldInfo field in fields)
      {
        object value = field.GetValue(enumType);
        values.Add(value);
      }

      return values;
    }
    public static EnumWrapper[] GetValuesWrapped(Type enumType)
    {
      if (!enumType.IsEnum)
      {
        throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
      }
      if (_enumCache.ContainsKey(enumType))
        return _enumCache[enumType];

      List<EnumWrapper> values = new List<EnumWrapper>();

      var fields = from field in enumType.GetFields()
                   where field.IsLiteral
                   select field;

      foreach (FieldInfo field in fields)
      {
        object value = field.GetValue(enumType);
        //values.Add(value);
        values.Add(new EnumWrapper { Name = value.ToString(), Value = value });
      }
      EnumWrapper[] ret = values.ToArray();
      _enumCache.Add(enumType, ret);
      return ret;
    }
    public static EnumWrapper GetValueWrapped(object o)
    {
      Type enumType = o.GetType();
      if (!enumType.IsEnum)
      {
        throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
      }

      EnumWrapper[] values = GetValuesWrapped(enumType);
      EnumWrapper v = values.FirstOrDefault(ew => ew.Value.Equals(o));
      return v;
    }
  }

  public class EnumWrapper
  {
    public string Name { get; set; }
    public object Value { get; set; }
    public override string ToString()
    {
      return Name;
    }

    public override int GetHashCode()
    {
      return this.Value.GetHashCode();
    }
    public override bool Equals(object obj)
    {
      return this.Value.Equals(obj);
    }
  }

  // Inspired by the blog entry 
  // http://www.dolittle.com/blogs/einar/archive/2008/01/13/missing-enum-getvalues-when-doing-silverlight-for-instance.aspx
  //public static class EnumHelper
  //{
  //  private static Dictionary<Type, EnumWrapper[]> _enumCache = new Dictionary<Type, EnumWrapper[]>();

  //  public static T[] GetValues<T>()
  //  {
  //    Type enumType = typeof(T);

  //    if (!enumType.IsEnum)
  //    {
  //      throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
  //    }

  //    List<T> values = new List<T>();

  //    var fields = from field in enumType.GetFields()
  //                 where field.IsLiteral
  //                 select field;

  //    foreach (FieldInfo field in fields)
  //    {
  //      object value = field.GetValue(enumType);
  //      values.Add((T)value);
  //    }

  //    return values.ToArray();
  //  }

  //  public static EnumWrapper[] GetValues(Type enumType)
  //  {
  //    if (!enumType.IsEnum)
  //    {
  //      throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
  //    }
  //    if (_enumCache.ContainsKey(enumType))
  //      return _enumCache[enumType];

  //    List<EnumWrapper> values = new List<EnumWrapper>();

  //    var fields = from field in enumType.GetFields()
  //                 where field.IsLiteral
  //                 select field;

  //    foreach (FieldInfo field in fields)
  //    {
  //      object value = field.GetValue(enumType);
  //      //values.Add(value);
  //      values.Add(new EnumWrapper { Name = value.ToString(), Value = value });
  //    }
  //    EnumWrapper[] ret = values.ToArray();
  //    _enumCache.Add(enumType, ret);
  //    return ret;
  //  }

  //  public static EnumWrapper GetValue(object o)
  //  {
  //    Type enumType = o.GetType();
  //    if (!enumType.IsEnum)
  //    {
  //      throw new ArgumentException("Type '" + enumType.Name + "' is not an enum");
  //    }

  //    EnumWrapper[] values = GetValues(enumType);
  //    EnumWrapper v = values.FirstOrDefault(ew => ew.Value.Equals(o));
  //    return v;
  //  }
  //}

  //public class EnumWrapper
  //{
  //  public string Name { get; set; }
  //  public object Value { get; set; }
  //}
}
