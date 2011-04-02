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

using System.Globalization;

namespace System.Windows.Controls.PropertyGrid.ComponentModel
{
  public class UInt64Converter : BaseNumberConverter
  {
    internal override object FromString(string value, CultureInfo culture)
    {
      return ulong.Parse(value, culture);
    }

    internal override object FromString(string value, NumberFormatInfo formatInfo)
    {
      return ulong.Parse(value, NumberStyles.Integer, formatInfo);
    }

    internal override object FromString(string value, int radix)
    {
      return Convert.ToUInt64(value, radix);
    }

    internal override string ToString(object value, NumberFormatInfo formatInfo)
    {
      ulong num = (ulong)value;
      return num.ToString("G", formatInfo);
    }

    internal override Type TargetType
    {
      get { return typeof(ulong); }
    }
  }
}
