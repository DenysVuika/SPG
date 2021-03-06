﻿/*
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
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.ComponentModel
{
  public class ColorConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value is string)
      {
        string str = ((string)value).Trim();
        try
        {
          return new SolidColorBrush(getColorFromHexString(str));
        }
        catch (FormatException exception)
        {
          throw new FormatException(string.Format("Unable to convert {0} - {1}", (string)value, "Boolean"), exception);
        }
      }
      return base.ConvertFrom(context, culture, value);
    }

    public Color getColorFromHexString(string s)
    {
      byte a = System.Convert.ToByte(s.Substring(0, 2), 16);
      byte r = System.Convert.ToByte(s.Substring(2, 2), 16);
      byte g = System.Convert.ToByte(s.Substring(4, 2), 16);
      byte b = System.Convert.ToByte(s.Substring(6, 2), 16);
      return Color.FromArgb(a, r, g, b);
    }
  }
}
