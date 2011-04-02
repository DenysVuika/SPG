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

namespace System.Windows.Controls.PropertyGrid
{
  public class PropertyFilterPredicate
  {
    private string _matchText;

    public PropertyFilterPredicate(string matchText)
    {
      if (matchText == null) throw new ArgumentNullException("matchText");
      this._matchText = matchText.ToUpper(CultureInfo.CurrentCulture);
    }

    public virtual bool Match(string target)
    {
      return ((target != null) && target.ToUpper(CultureInfo.CurrentCulture).Contains(this._matchText));
    }

    protected string MatchText
    {
      get { return this._matchText; }
    }
  }
}
