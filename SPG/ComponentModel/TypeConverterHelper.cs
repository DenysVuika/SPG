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

using System.ComponentModel;
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.ComponentModel
{
  public static class TypeConverterHelper
  {
    public static TypeConverter GetConverter(Type type)
    {
      TypeConverter converter = null;
      converter = GetCoreConverterFromCoreType(type);
      if (converter == null)
        converter = GetCoreConverterFromCustomType(type);
      return converter;
    }

    private static TypeConverter GetCoreConverterFromCoreType(Type type)
    {
      TypeConverter converter = null;
      if (type == typeof(int))
      {
        return new Int32Converter();
      }
      if (type == typeof(short))
      {
        return new Int16Converter();
      }
      if (type == typeof(long))
      {
        return new Int64Converter();
      }
      if (type == typeof(uint))
      {
        return new UInt32Converter();
      }
      if (type == typeof(ushort))
      {
        return new UInt16Converter();
      }
      if (type == typeof(ulong))
      {
        return new UInt64Converter();
      }
      if (type == typeof(bool))
      {
        return new BooleanConverter();
      }
      if (type == typeof(double))
      {
        return new DoubleConverter();
      }
      if (type == typeof(float))
      {
        return new SingleConverter();
      }
      if (type == typeof(byte))
      {
        return new ByteConverter();
      }
      if (type == typeof(sbyte))
      {
        return new SByteConverter();
      }
      if (type == typeof(char))
      {
        return new CharConverter();
      }
      if (type == typeof(decimal))
      {
        return new DecimalConverter();
      }
      if (type == typeof(TimeSpan))
      {
        return new TimeSpanConverter();
      }
      if (type == typeof(Guid))
      {
        return new GuidConverter();
      }
      if (type == typeof(string))
      {
        return new StringConverter();
      }
      if (type == typeof(Brush))
      {
        return new ColorConverter();
      }
      //if (type == typeof(CultureInfo))
      //{
      //    return new CultureInfoConverter();
      //}
      //if (type == typeof(Type))
      //{
      //    return new TypeTypeConverter();
      //}
      //if (type == typeof(DateTime))
      //{
      //    return new DateTimeConverter2();
      //}
      //if (ReflectionHelper.IsNullableType(type))
      //{
      //    converter = new NullableConverter(type);
      //}
      return converter;
    }


    private static TypeConverter GetCoreConverterFromCustomType(Type type)
    {
      TypeConverter converter = null;
      //if (type.IsEnum)
      //{
      //    return new EnumConverter(type);
      //}
      if (typeof(int).IsAssignableFrom(type))
      {
        return new Int32Converter();
      }
      if (typeof(short).IsAssignableFrom(type))
      {
        return new Int16Converter();
      }
      if (typeof(long).IsAssignableFrom(type))
      {
        return new Int64Converter();
      }
      if (typeof(uint).IsAssignableFrom(type))
      {
        return new UInt32Converter();
      }
      if (typeof(ushort).IsAssignableFrom(type))
      {
        return new UInt16Converter();
      }
      if (typeof(ulong).IsAssignableFrom(type))
      {
        return new UInt64Converter();
      }
      if (typeof(bool).IsAssignableFrom(type))
      {
        return new BooleanConverter();
      }
      if (typeof(double).IsAssignableFrom(type))
      {
        return new DoubleConverter();
      }
      if (typeof(float).IsAssignableFrom(type))
      {
        return new SingleConverter();
      }
      if (typeof(byte).IsAssignableFrom(type))
      {
        return new ByteConverter();
      }
      if (typeof(sbyte).IsAssignableFrom(type))
      {
        return new SByteConverter();
      }
      if (typeof(char).IsAssignableFrom(type))
      {
        return new CharConverter();
      }
      if (typeof(decimal).IsAssignableFrom(type))
      {
        return new DecimalConverter();
      }
      if (typeof(TimeSpan).IsAssignableFrom(type))
      {
        return new TimeSpanConverter();
      }
      if (typeof(Guid).IsAssignableFrom(type))
      {
        return new GuidConverter();
      }
      if (typeof(string).IsAssignableFrom(type))
      {
        return new StringConverter();
      }
      //if (typeof(CultureInfo).IsAssignableFrom(type))
      //{
      //    return new CultureInfoConverter();
      //}
      //if (typeof(Type).IsAssignableFrom(type))
      //{
      //    return new TypeTypeConverter();
      //}
      //if (typeof(DateTime).IsAssignableFrom(type))
      //{
      //    converter = new DateTimeConverter2();
      //}
      return converter;
    }
  }
}
