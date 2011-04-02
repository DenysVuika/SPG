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

using System.Reflection;
using System.Windows.Media;

namespace System.Windows.Controls.PropertyGrid.PropertyEditing
{
  public sealed class ColorExtension
  {
    #region Predefined
    public static readonly ColorExtension AliceBlue = 0xFFF0F8FF;
    public static readonly ColorExtension AntiqueWhite = 0xFFFAEBD7;
    public static readonly ColorExtension Aqua = 0xFF00FFFF;
    public static readonly ColorExtension Aquamarine = 0xFF7FFFD4;
    public static readonly ColorExtension Azure = 0xFFF0FFFF;
    public static readonly ColorExtension Beige = 0xFFF5F5DC;
    public static readonly ColorExtension Bisque = 0xFFFFE4C4;
    public static readonly ColorExtension Black = 0xFF000000;
    public static readonly ColorExtension BlanchedAlmond = 0xFFFFEBCD;
    public static readonly ColorExtension Blue = 0xFF0000FF;
    public static readonly ColorExtension BlueViolet = 0xFF8A2BE2;
    public static readonly ColorExtension Brown = 0xFFA52A2A;
    public static readonly ColorExtension BurlyWood = 0xFFDEB887;
    public static readonly ColorExtension CadetBlue = 0xFF5F9EA0;
    public static readonly ColorExtension Chartreuse = 0xFF7FFF00;
    public static readonly ColorExtension Chocolate = 0xFFD2691E;
    public static readonly ColorExtension Coral = 0xFFFF7F50;
    public static readonly ColorExtension CornflowerBlue = 0xFF6495ED;
    public static readonly ColorExtension Cornsilk = 0xFFFFF8DC;
    public static readonly ColorExtension Crimson = 0xFFDC143C;
    public static readonly ColorExtension Cyan = 0xFF00FFFF;
    public static readonly ColorExtension DarkBlue = 0xFF00008B;
    public static readonly ColorExtension DarkCyan = 0xFF008B8B;
    public static readonly ColorExtension DarkGoldenrod = 0xFFB8860B;
    public static readonly ColorExtension DarkGray = 0xFFA9A9A9;
    public static readonly ColorExtension DarkGreen = 0xFF006400;
    public static readonly ColorExtension DarkKhaki = 0xFFBDB76B;
    public static readonly ColorExtension DarkMagenta = 0xFF8B008B;
    public static readonly ColorExtension DarkOliveGreen = 0xFF556B2F;
    public static readonly ColorExtension DarkOrange = 0xFFFF8C00;
    public static readonly ColorExtension DarkOrchid = 0xFF9932CC;
    public static readonly ColorExtension DarkRed = 0xFF8B0000;
    public static readonly ColorExtension DarkSalmon = 0xFFE9967A;
    public static readonly ColorExtension DarkSeaGreen = 0xFF8FBC8F;
    public static readonly ColorExtension DarkSlateBlue = 0xFF483D8B;
    public static readonly ColorExtension DarkSlateGray = 0xFF2F4F4F;
    public static readonly ColorExtension DarkTurquoise = 0xFF00CED1;
    public static readonly ColorExtension DarkViolet = 0xFF9400D3;
    public static readonly ColorExtension DeepPink = 0xFFFF1493;
    public static readonly ColorExtension DeepSkyBlue = 0xFF00BFFF;
    public static readonly ColorExtension DimGray = 0xFF696969;
    public static readonly ColorExtension DodgerBlue = 0xFF1E90FF;
    public static readonly ColorExtension Firebrick = 0xFFB22222;
    public static readonly ColorExtension FloralWhite = 0xFFFFFAF0;
    public static readonly ColorExtension ForestGreen = 0xFF228B22;
    public static readonly ColorExtension Fuchsia = 0xFFFF00FF;
    public static readonly ColorExtension Gainsboro = 0xFFDCDCDC;
    public static readonly ColorExtension GhostWhite = 0xFFF8F8FF;
    public static readonly ColorExtension Gold = 0xFFFFD700;
    public static readonly ColorExtension Goldenrod = 0xFFDAA520;
    public static readonly ColorExtension Gray = 0xFF808080;
    public static readonly ColorExtension Green = 0xFF008000;
    public static readonly ColorExtension GreenYellow = 0xFFADFF2F;
    public static readonly ColorExtension Honeydew = 0xFFF0FFF0;
    public static readonly ColorExtension HotPink = 0xFFFF69B4;
    public static readonly ColorExtension IndianRed = 0xFFCD5C5C;
    public static readonly ColorExtension Indigo = 0xFF4B0082;
    public static readonly ColorExtension Ivory = 0xFFFFFFF0;
    public static readonly ColorExtension Khaki = 0xFFF0E68C;
    public static readonly ColorExtension Lavender = 0xFFE6E6FA;
    public static readonly ColorExtension LavenderBlush = 0xFFFFF0F5;
    public static readonly ColorExtension LawnGreen = 0xFF7CFC00;
    public static readonly ColorExtension LemonChiffon = 0xFFFFFACD;
    public static readonly ColorExtension LightBlue = 0xFFADD8E6;
    public static readonly ColorExtension LightCoral = 0xFFF08080;
    public static readonly ColorExtension LightCyan = 0xFFE0FFFF;
    public static readonly ColorExtension LightGoldenrodYellow = 0xFFFAFAD2;
    public static readonly ColorExtension LightGray = 0xFFD3D3D3;
    public static readonly ColorExtension LightGreen = 0xFF90EE90;
    public static readonly ColorExtension LightPink = 0xFFFFB6C1;
    public static readonly ColorExtension LightSalmon = 0xFFFFA07A;
    public static readonly ColorExtension LightSeaGreen = 0xFF20B2AA;
    public static readonly ColorExtension LightSkyBlue = 0xFF87CEFA;
    public static readonly ColorExtension LightSlateGray = 0xFF778899;
    public static readonly ColorExtension LightSteelBlue = 0xFFB0C4DE;
    public static readonly ColorExtension LightYellow = 0xFFFFFFE0;
    public static readonly ColorExtension Lime = 0xFF00FF00;
    public static readonly ColorExtension LimeGreen = 0xFF32CD32;
    public static readonly ColorExtension Linen = 0xFFFAF0E6;
    public static readonly ColorExtension Magenta = 0xFFFF00FF;
    public static readonly ColorExtension Maroon = 0xFF800000;
    public static readonly ColorExtension MediumAquamarine = 0xFF66CDAA;
    public static readonly ColorExtension MediumBlue = 0xFF0000CD;
    public static readonly ColorExtension MediumOrchid = 0xFFBA55D3;
    public static readonly ColorExtension MediumPurple = 0xFF9370DB;
    public static readonly ColorExtension MediumSeaGreen = 0xFF3CB371;
    public static readonly ColorExtension MediumSlateBlue = 0xFF7B68EE;
    public static readonly ColorExtension MediumSpringGreen = 0xFF00FA9A;
    public static readonly ColorExtension MediumTurquoise = 0xFF48D1CC;
    public static readonly ColorExtension MediumVioletRed = 0xFFC71585;
    public static readonly ColorExtension MidnightBlue = 0xFF191970;
    public static readonly ColorExtension MintCream = 0xFFF5FFFA;
    public static readonly ColorExtension MistyRose = 0xFFFFE4E1;
    public static readonly ColorExtension Moccasin = 0xFFFFE4B5;
    public static readonly ColorExtension NavajoWhite = 0xFFFFDEAD;
    public static readonly ColorExtension Navy = 0xFF000080;
    public static readonly ColorExtension OldLace = 0xFFFDF5E6;
    public static readonly ColorExtension Olive = 0xFF808000;
    public static readonly ColorExtension OliveDrab = 0xFF6B8E23;
    public static readonly ColorExtension Orange = 0xFFFFA500;
    public static readonly ColorExtension OrangeRed = 0xFFFF4500;
    public static readonly ColorExtension Orchid = 0xFFDA70D6;
    public static readonly ColorExtension PaleGoldenrod = 0xFFEEE8AA;
    public static readonly ColorExtension PaleGreen = 0xFF98FB98;
    public static readonly ColorExtension PaleTurquoise = 0xFFAFEEEE;
    public static readonly ColorExtension PaleVioletRed = 0xFFDB7093;
    public static readonly ColorExtension PapayaWhip = 0xFFFFEFD5;
    public static readonly ColorExtension PeachPuff = 0xFFFFDAB9;
    public static readonly ColorExtension Peru = 0xFFCD853F;
    public static readonly ColorExtension Pink = 0xFFFFC0CB;
    public static readonly ColorExtension Plum = 0xFFDDA0DD;
    public static readonly ColorExtension PowderBlue = 0xFFB0E0E6;
    public static readonly ColorExtension Purple = 0xFF800080;
    public static readonly ColorExtension Red = 0xFFFF0000;
    public static readonly ColorExtension RosyBrown = 0xFFBC8F8F;
    public static readonly ColorExtension RoyalBlue = 0xFF4169E1;
    public static readonly ColorExtension SaddleBrown = 0xFF8B4513;
    public static readonly ColorExtension Salmon = 0xFFFA8072;
    public static readonly ColorExtension SandyBrown = 0xFFF4A460;
    public static readonly ColorExtension SeaGreen = 0xFF2E8B57;
    public static readonly ColorExtension SeaShell = 0xFFFFF5EE;
    public static readonly ColorExtension Sienna = 0xFFA0522D;
    public static readonly ColorExtension Silver = 0xFFC0C0C0;
    public static readonly ColorExtension SkyBlue = 0xFF87CEEB;
    public static readonly ColorExtension SlateBlue = 0xFF6A5ACD;
    public static readonly ColorExtension SlateGray = 0xFF708090;
    public static readonly ColorExtension Snow = 0xFFFFFAFA;
    public static readonly ColorExtension SpringGreen = 0xFF00FF7F;
    public static readonly ColorExtension SteelBlue = 0xFF4682B4;
    public static readonly ColorExtension Tan = 0xFFD2B48C;
    public static readonly ColorExtension Teal = 0xFF008080;
    public static readonly ColorExtension Thistle = 0xFFD8BFD8;
    public static readonly ColorExtension Tomato = 0xFFFF6347;
    public static readonly ColorExtension Transparent = 0x00FFFFFF;
    public static readonly ColorExtension Turquoise = 0xFF40E0D0;
    public static readonly ColorExtension Violet = 0xFFEE82EE;
    public static readonly ColorExtension Wheat = 0xFFF5DEB3;
    public static readonly ColorExtension White = 0xFFFFFFFF;
    public static readonly ColorExtension WhiteSmoke = 0xFFF5F5F5;
    public static readonly ColorExtension Yellow = 0xFFFFFF00;
    public static readonly ColorExtension YellowGreen = 0xFF9ACD32;
    #endregion

    private readonly uint value;

    private ColorExtension()
    {
    }

    private ColorExtension(uint color)
    {
      this.value = color;
    }

    public static implicit operator ColorExtension(uint color)
    {
      return new ColorExtension(color);
    }

    public static implicit operator Color(ColorExtension color)
    {
      uint colorValue = color;

      return Color.FromArgb(
              (byte)(colorValue >> 24),
              (byte)(colorValue >> 16),
              (byte)(colorValue >> 8),
              (byte)(colorValue));
    }

    public static implicit operator ColorExtension(string name)
    {
      FieldInfo[] info = typeof(ColorExtension).GetFields();

      foreach (FieldInfo fieldInfo in info)
      {
        if (fieldInfo.Name.ToLower() == name.ToLower())
          return fieldInfo.GetValue(null) as ColorExtension;
      }
      return null;
    }

    public static implicit operator Brush(ColorExtension color)
    {
      return new SolidColorBrush(color);
    }

    public static implicit operator uint(ColorExtension color)
    {
      if (color == null) return 0;
      return color.value;
    }
  }
}