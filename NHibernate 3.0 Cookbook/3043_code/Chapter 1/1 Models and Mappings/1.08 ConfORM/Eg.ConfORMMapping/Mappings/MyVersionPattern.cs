using System;
using System.Reflection;
using ConfOrm;

namespace Eg.ConfORMMapping.Mappings
{
    public class MyVersionPattern : IPattern<MemberInfo>
    {
      public bool Match(MemberInfo subject)
      {
        return IsInt(subject) && 
          IsNamedVersion(subject);
      }

      private bool IsInt(MemberInfo subject)
      {
        if (subject == null)
        {
          return false;
        }
        var propertyOrFieldType = 
          subject.GetPropertyOrFieldType();
        return propertyOrFieldType == typeof (int);
      }

      private bool IsNamedVersion(MemberInfo subject)
      {
        var name = subject.Name;
        return name.Equals("version", 
          StringComparison.InvariantCultureIgnoreCase);
      }

    }
}
