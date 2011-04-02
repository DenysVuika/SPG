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

using System;
using System.ComponentModel;
using System.Windows.Media;

namespace SPG.Samples.Overview
{
  public class Person : INotifyPropertyChanged
  {
    #region Fields
    private double _Double;
    private decimal _Decimal;
    private int _Int;
    private short _Short;
    private long _Long;
    private uint _Uint;
    private ushort _Ushort;
    private ulong _Ulong;
    private string _String;
    private char _Char;
    private DateTime _DateTime;
    private TimeSpan _TimeSpan;
    private bool _Boolean;
    private DayOfWeek _Enum;
    private Brush brush;
    #endregion

    [Editor(typeof(ButtonEditor))]
    [DisplayName("Show dialog")]
    public string SimpleAction { get; set; }

    [Category("Numeric Float")]
    [Description("System.Double based property")]
    public double Double
    {
      get { return _Double; }
      set
      {
        if (_Double == value) return;
        _Double = value;
        OnPropertyChanged("Double");
      }
    }

    [Category("Numeric Float")]
    [Description("System.Decimal based property")]
    public decimal Decimal
    {
      get { return _Decimal; }
      set
      {
        if (_Decimal == value) return;
        _Decimal = value;
        OnPropertyChanged("Decimal");
      }
    }

    [Category("Numeric Signed")]
    [Description("System.Int32 based property")]
    public int Int
    {
      get { return _Int; }
      set
      {
        if (_Int == value) return;
        _Int = value;
        OnPropertyChanged("Int");
      }
    }

    [Category("Numeric Signed")]
    [Description("System.Int16 based property")]
    public short Short
    {
      get { return _Short; }
      set
      {
        if (_Short == value) return;
        _Short = value;
        OnPropertyChanged("Short");
      }
    }

    [Category("Numeric Signed")]
    [Description("System.Int64 based property")]
    public long Long
    {
      get { return _Long; }
      set
      {
        if (_Long == value) return;
        _Long = value;
        OnPropertyChanged("Long");
      }
    }

    [Category("Numeric Unsigned")]
    [Description("System.UInt32 based property")]
    public uint Uint
    {
      get { return _Uint; }
      set
      {
        if (_Uint == value) return;
        _Uint = value;
        OnPropertyChanged("Uint");
      }
    }

    [Category("Numeric Unsigned")]
    [Description("System.UInt16 based property")]
    public ushort Ushort
    {
      get { return _Ushort; }
      set
      {
        if (_Ushort == value) return;
        _Ushort = value;
        OnPropertyChanged("Ushort");
      }
    }
    [Category("Solid brush")]
    [Description("Solid color brush property")]
    public Brush Brush
    {
      get { return brush; }
      set
      {
        if (brush == value) return;
        brush = value;
        OnPropertyChanged("Brush");
      }
    }

    [Category("Numeric Unsigned")]
    [Description("System.UInt64 based property")]
    public ulong Ulong
    {
      get { return _Ulong; }
      set
      {
        if (_Ulong == value) return;
        _Ulong = value;
        OnPropertyChanged("Ulong");
      }
    }

    [Category("Read Only")]
    [DisplayName("The answer to everything")]
    [Description("The answer to everything")]
    public double Answer
    {
      get { return 42; }
    }

    [Category("Read Only")]
    [Description("System.DateTime based property")]
    public DateTime Now
    {
      get { return DateTime.Now; }
    }

    [Category("Read Only")]
    [Description("System.Boolean based readonly property")]
    public bool True
    {
      get { return true; }
    }

    [Category("Read Only")]
    [Description("System.DayOfWeek based readonly property")]
    public DayOfWeek Today
    {
      get { return DateTime.Now.DayOfWeek; }
    }

    [Category("Strings")]
    [Description("System.String based property")]
    public string String
    {
      get { return _String; }
      set
      {
        if (_String == value) return;
        _String = value;
        OnPropertyChanged("String");
      }
    }

    [Category("Strings")]
    [Description("System.Char based property")]
    public char Char
    {
      get { return _Char; }
      set
      {
        if (_Char == value) return;
        _Char = value;
        OnPropertyChanged("Char");
      }
    }

    [Category("Date & Time")]
    [Description("System.DateTime based property")]
    public DateTime Datetime
    {
      get { return _DateTime; }
      set
      {
        if (_DateTime == value) return;
        _DateTime = value;
        OnPropertyChanged("DateTime");
      }
    }

    [Category("Date & Time")]
    [Description("System.TimeSpan based property")]
    public TimeSpan TimeSpan
    {
      get { return _TimeSpan; }
      set
      {
        if (_TimeSpan == value) return;
        _TimeSpan = value;
        OnPropertyChanged("TimeSpan");
      }
    }

    [Category("Others")]
    [Description("System.Boolean based property")]
    public bool Boolean
    {
      get { return _Boolean; }
      set
      {
        if (_Boolean == value) return;
        _Boolean = value;
        OnPropertyChanged("Boolean");
      }
    }

    [Category("Others")]
    [Description("System.DayOfWeek based property")]
    public DayOfWeek Enum
    {
      get { return _Enum; }
      set
      {
        if (_Enum == value) return;
        _Enum = value;
        OnPropertyChanged("Enum");
      }
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
#if DEBUG
      if (string.IsNullOrWhiteSpace(propertyName))
        throw new ArgumentNullException("propertyName");
#endif

      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}
