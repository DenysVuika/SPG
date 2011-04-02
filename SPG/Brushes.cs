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

namespace System.Windows.Media
{
  /// <summary>
  /// Implements a set of predefined System.Windows.Media.SolidColorBrush objects.
  /// <remarks>
  /// Brushes class is present within WPF but yet missing in Silverlight 4 and earlier versions. 
  /// Might be useful when developing compatible WPF/Silverlight components.
  /// </remarks>
  /// </summary>
  public static class Brushes
  {
    private static readonly Lazy<Brush> _Black = new Lazy<Brush>(() => new SolidColorBrush(Colors.Black), true);
    private static readonly Lazy<Brush> _Blue = new Lazy<Brush>(() => new SolidColorBrush(Colors.Blue), true);
    private static readonly Lazy<Brush> _Brown = new Lazy<Brush>(() => new SolidColorBrush(Colors.Brown), true);
    private static readonly Lazy<Brush> _Cyan = new Lazy<Brush>(() => new SolidColorBrush(Colors.Cyan), true);
    private static readonly Lazy<Brush> _DarkGray = new Lazy<Brush>(() => new SolidColorBrush(Colors.DarkGray), true);
    private static readonly Lazy<Brush> _Gray = new Lazy<Brush>(() => new SolidColorBrush(Colors.Gray), true);
    private static readonly Lazy<Brush> _Green = new Lazy<Brush>(() => new SolidColorBrush(Colors.Green), true);
    private static readonly Lazy<Brush> _LightGray = new Lazy<Brush>(() => new SolidColorBrush(Colors.LightGray), true);
    private static readonly Lazy<Brush> _Magenta = new Lazy<Brush>(() => new SolidColorBrush(Colors.Magenta), true);
    private static readonly Lazy<Brush> _Orange = new Lazy<Brush>(() => new SolidColorBrush(Colors.Orange), true);
    private static readonly Lazy<Brush> _Purple = new Lazy<Brush>(() => new SolidColorBrush(Colors.Purple), true);
    private static readonly Lazy<Brush> _Red = new Lazy<Brush>(() => new SolidColorBrush(Colors.Red), true);
    private static readonly Lazy<Brush> _Transparent = new Lazy<Brush>(() => new SolidColorBrush(Colors.Transparent), true);
    private static readonly Lazy<Brush> _White = new Lazy<Brush>(() => new SolidColorBrush(Colors.White), true);
    private static readonly Lazy<Brush> _Yellow = new Lazy<Brush>(() => new SolidColorBrush(Colors.Yellow), true);

    public static Brush Black { get { return _Black.Value; } }
    public static Brush Blue { get { return _Blue.Value; } }
    public static Brush Brown { get { return _Brown.Value; } }
    public static Brush Cyan { get { return _Cyan.Value; } }
    public static Brush DarkGray { get { return _DarkGray.Value; } }
    public static Brush Gray { get { return _Gray.Value; } }
    public static Brush Green { get { return _Green.Value; } }
    public static Brush LightGray { get { return _LightGray.Value; } }
    public static Brush Magenta { get { return _Magenta.Value; } }
    public static Brush Orange { get { return _Orange.Value; } }
    public static Brush Purple { get { return _Purple.Value; } }
    public static Brush Red { get { return _Red.Value; } }
    public static Brush Transparent { get { return _Transparent.Value; } }
    public static Brush White { get { return _White.Value; } }
    public static Brush Yellow { get { return _Yellow.Value; } }
  }
}
