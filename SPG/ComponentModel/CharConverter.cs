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
using System.Globalization;

namespace System.Windows.Controls.PropertyGrid.ComponentModel
{
  public class CharConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (!(value is string)) return base.ConvertFrom(context, culture, value);

      string str = (string)value;
      if (str.Length > 1) str = str.Trim();
      if ((str == null) || (str.Length <= 0)) return '\0';
      if (str.Length != 1) throw new FormatException();

      return str[0];
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (((destinationType == typeof(string)) && (value is char)) && (((char)value) == '\0')) return "";
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
