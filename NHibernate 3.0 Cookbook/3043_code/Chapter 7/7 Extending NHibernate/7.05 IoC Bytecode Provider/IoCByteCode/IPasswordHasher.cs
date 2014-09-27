using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCByteCode
{

  public interface IPasswordHasher
  {

string HashPassword(string email, string password);

  }

}
