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

namespace System.ComponentModel
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class EditorAttribute : Attribute
  {
    public string EditorTypeName { get; private set; }

    public EditorAttribute(string typeName)
    {
      if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException("typeName");
      this.EditorTypeName = typeName;
    }

    public EditorAttribute(Type type)
    {
      if (type == null) throw new ArgumentNullException("type");
      this.EditorTypeName = type.AssemblyQualifiedName;
    }

    public override bool Equals(object obj)
    {
      if (obj == this)
      {
        return true;
      }
      EditorAttribute attribute = obj as EditorAttribute;
      return (((attribute != null) && (attribute.EditorTypeName == this.EditorTypeName)));
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
