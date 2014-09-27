using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace EncryptedStringExample
{

  public class EncryptedString : IUserType, IParameterizedType 
  {

    private IEncryptor _encryptor;

    public object NullSafeGet(
      IDataReader rs,
      string[] names,
      object owner)
    {
      //treat for the posibility of null values
      object passwordString =
        NHibernateUtil.String.NullSafeGet(rs, names[0]);
      if (passwordString != null)
      {
        return _encryptor.Decrypt((string)passwordString);
      }
      return null;
    }

    public void NullSafeSet(
      IDbCommand cmd,
      object value,
      int index)
    {
      if (value == null)
      {
        NHibernateUtil.String.NullSafeSet(cmd, null, index);
        return;
      }

      string encryptedValue = _encryptor.Encrypt((string)value);
      NHibernateUtil.String.NullSafeSet(
        cmd, encryptedValue, index);
    }

    public object DeepCopy(object value)
    {
      return value == null ? null :
        string.Copy((string)value);
    }

    public object Replace(object original,
      object target, object owner)
    {
      return original;
    }

    public object Assemble(object cached, object owner)
    {
      return DeepCopy(cached);
    }

    public object Disassemble(object value)
    {
      return DeepCopy(value);
    }

    public SqlType[] SqlTypes
    {
      get
      {
        return new[] { new SqlType(DbType.String) };
      }
    }

    public Type ReturnedType
    {
      get { return typeof(string); }
    }

    public bool IsMutable
    {
      get { return false; }
    }

    public new bool Equals(object x, object y)
    {
      if (ReferenceEquals(x, y))
      {
        return true;
      }
      if (x == null || y == null)
      {
        return false;
      }
      return x.Equals(y);
    }

    public int GetHashCode(object x)
    {
      if (x == null)
      {
        throw new ArgumentNullException("x");
      }
      return x.GetHashCode();
    }


    public void SetParameterValues(
      IDictionary<string, string> parameters)
    {
      if (parameters != null)
      {
        var encryptorTypeName = parameters["encryptor"];
        _encryptor = !string.IsNullOrEmpty(encryptorTypeName)
                      ? (IEncryptor) Instantiate(encryptorTypeName)
                      : new DESEncryptor();
        var encryptionKey = parameters["encryptionKey"];
        if (!string.IsNullOrEmpty(encryptionKey))
          _encryptor.EncryptionKey = encryptionKey;
      }
      else
      {
        _encryptor = new DESEncryptor();
      }
    }

    private static object Instantiate(string typeName)
    {
      var type = Type.GetType(typeName);
      return Activator.CreateInstance(type);
    }

  }
}
