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

namespace System.Windows.Controls.PropertyGrid
{
  public class PropertyFilter
  {
    private List<PropertyFilterPredicate> _predicates;

    public PropertyFilter(IEnumerable<PropertyFilterPredicate> predicates)
    {
      this._predicates = new List<PropertyFilterPredicate>();
      this.SetPredicates(predicates);
    }

    public PropertyFilter(string filterText)
    {
      this._predicates = new List<PropertyFilterPredicate>();
      this.SetPredicates(filterText);
    }

    public bool Match(IPropertyFilterTarget target)
    {
      if (target == null) throw new ArgumentNullException("target");
      if (this.IsEmpty) return true;

      for (int i = 0; i < this._predicates.Count; i++)
        if (target.MatchesPredicate(this._predicates[i]))
          return true;

      return false;
    }

    private void SetPredicates(IEnumerable<PropertyFilterPredicate> predicates)
    {
      if (predicates != null)
        foreach (PropertyFilterPredicate predicate in predicates)
          if (predicate != null)
            this._predicates.Add(predicate);
    }

    private void SetPredicates(string filterText)
    {
      if (!string.IsNullOrEmpty(filterText))
      {
        string[] strArray = filterText.Split(new char[] { ' ' });
        for (int i = 0; i < strArray.Length; i++)
          if (!string.IsNullOrEmpty(strArray[i]))
            this._predicates.Add(new PropertyFilterPredicate(strArray[i]));
      }
    }

    public bool IsEmpty
    {
      get
      {
        if (this._predicates != null) return (this._predicates.Count == 0);
        return true;
      }
    }
  }
}
