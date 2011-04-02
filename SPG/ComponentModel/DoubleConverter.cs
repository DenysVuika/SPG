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
  public class DoubleConverter : BaseNumberConverter
  {
    internal override object FromString(string value, CultureInfo culture)
    {
      return double.Parse(value, culture);
    }

    internal override object FromString(string value, NumberFormatInfo formatInfo)
    {
      return double.Parse(value, NumberStyles.Float, (IFormatProvider)formatInfo);
    }

    internal override object FromString(string value, int radix)
    {
      return Convert.ToDouble(value, CultureInfo.CurrentCulture);
    }

    internal override string ToString(object value, NumberFormatInfo formatInfo)
    {
      double num = (double)value;
      return num.ToString("R", formatInfo);
    }

    internal override bool AllowHex
    {
      get { return false; }
    }

    internal override Type TargetType
    {
      get { return typeof(double); }
    }
  }
}
