// Type: System.Collections.Specialized.NameValueCollection
// Assembly: System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.dll

using System;
using System.Collections;
using System.Globalization;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Specialized
{
  [Serializable]
  public class NameValueCollection : NameObjectCollectionBase
  {
    private string[] _all;
    private string[] _allKeys;

    public string this[string name]
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.Get(name);
      }
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] set
      {
        this.Set(name, value);
      }
    }

    public string this[int index]
    {
      [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")] get
      {
        return this.Get(index);
      }
    }

    public virtual string[] AllKeys
    {
      get
      {
        if (this._allKeys == null)
          this._allKeys = this.BaseGetAllKeys();
        return this._allKeys;
      }
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public NameValueCollection()
    {
    }

    public NameValueCollection(NameValueCollection col)
      : base(col != null ? col.Comparer : (IEqualityComparer) null)
    {
      this.Add(col);
    }

    [Obsolete("Please use NameValueCollection(IEqualityComparer) instead.")]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public NameValueCollection(IHashCodeProvider hashProvider, IComparer comparer)
      : base(hashProvider, comparer)
    {
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public NameValueCollection(int capacity)
      : base(capacity)
    {
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public NameValueCollection(IEqualityComparer equalityComparer)
      : base(equalityComparer)
    {
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public NameValueCollection(int capacity, IEqualityComparer equalityComparer)
      : base(capacity, equalityComparer)
    {
    }

    public NameValueCollection(int capacity, NameValueCollection col)
      : base(capacity, col != null ? col.Comparer : (IEqualityComparer) null)
    {
      if (col == null)
        throw new ArgumentNullException("col");
      this.Comparer = col.Comparer;
      this.Add(col);
    }

    [Obsolete("Please use NameValueCollection(Int32, IEqualityComparer) instead.")]
    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public NameValueCollection(int capacity, IHashCodeProvider hashProvider, IComparer comparer)
      : base(capacity, hashProvider, comparer)
    {
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    protected NameValueCollection(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    internal NameValueCollection(DBNull dummy)
      : base(dummy)
    {
    }

    protected void InvalidateCachedArrays()
    {
      this._all = (string[]) null;
      this._allKeys = (string[]) null;
    }

    public void Add(NameValueCollection c)
    {
      if (c == null)
        throw new ArgumentNullException("c");
      this.InvalidateCachedArrays();
      int count = c.Count;
      for (int index1 = 0; index1 < count; ++index1)
      {
        string key = c.GetKey(index1);
        string[] values = c.GetValues(index1);
        if (values != null)
        {
          for (int index2 = 0; index2 < values.Length; ++index2)
            this.Add(key, values[index2]);
        }
        else
          this.Add(key, (string) null);
      }
    }

    public virtual void Clear()
    {
      if (this.IsReadOnly)
        throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
      this.InvalidateCachedArrays();
      this.BaseClear();
    }

    public void CopyTo(Array dest, int index)
    {
      if (dest == null)
        throw new ArgumentNullException("dest");
      if (dest.Rank != 1)
        throw new ArgumentException(SR.GetString("Arg_MultiRank"));
      if (index < 0)
      {
        throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[1]
        {
          (object) index.ToString((IFormatProvider) CultureInfo.CurrentCulture)
        }));
      }
      else
      {
        if (dest.Length - index < this.Count)
          throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
        int count = this.Count;
        if (this._all == null)
        {
          this._all = new string[count];
          for (int index1 = 0; index1 < count; ++index1)
          {
            this._all[index1] = this.Get(index1);
            dest.SetValue((object) this._all[index1], index1 + index);
          }
        }
        else
        {
          for (int index1 = 0; index1 < count; ++index1)
            dest.SetValue((object) this._all[index1], index1 + index);
        }
      }
    }

    [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public bool HasKeys()
    {
      return this.InternalHasKeys();
    }

    internal virtual bool InternalHasKeys()
    {
      return this.BaseHasKeys();
    }

    public virtual void Add(string name, string value)
    {
      if (this.IsReadOnly)
        throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
      this.InvalidateCachedArrays();
      ArrayList arrayList1 = (ArrayList) this.BaseGet(name);
      if (arrayList1 == null)
      {
        ArrayList arrayList2 = new ArrayList(1);
        if (value != null)
          arrayList2.Add((object) value);
        this.BaseAdd(name, (object) arrayList2);
      }
      else
      {
        if (value == null)
          return;
        arrayList1.Add((object) value);
      }
    }

    public virtual string Get(string name)
    {
      return NameValueCollection.GetAsOneString((ArrayList) this.BaseGet(name));
    }

    public virtual string[] GetValues(string name)
    {
      return NameValueCollection.GetAsStringArray((ArrayList) this.BaseGet(name));
    }

    public virtual void Set(string name, string value)
    {
      if (this.IsReadOnly)
        throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
      this.InvalidateCachedArrays();
      this.BaseSet(name, (object) new ArrayList(1)
      {
        (object) value
      });
    }

    public virtual void Remove(string name)
    {
      this.InvalidateCachedArrays();
      this.BaseRemove(name);
    }

    public virtual string Get(int index)
    {
      return NameValueCollection.GetAsOneString((ArrayList) this.BaseGet(index));
    }

    public virtual string[] GetValues(int index)
    {
      return NameValueCollection.GetAsStringArray((ArrayList) this.BaseGet(index));
    }

    public virtual string GetKey(int index)
    {
      return this.BaseGetKey(index);
    }

    private static string GetAsOneString(ArrayList list)
    {
      int num = list != null ? list.Count : 0;
      if (num == 1)
        return (string) list[0];
      if (num <= 1)
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder((string) list[0]);
      for (int index = 1; index < num; ++index)
      {
        stringBuilder.Append(',');
        stringBuilder.Append((string) list[index]);
      }
      return ((object) stringBuilder).ToString();
    }

    private static string[] GetAsStringArray(ArrayList list)
    {
      int count = list != null ? list.Count : 0;
      if (count == 0)
        return (string[]) null;
      string[] strArray = new string[count];
      list.CopyTo(0, (Array) strArray, 0, count);
      return strArray;
    }
  }
}
