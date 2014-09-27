using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace ContextInfoExample
{

  public class UsernameContextDataProvider : IContextDataProvider 
  {

    public string GetData()
    {
      return WindowsIdentity.GetCurrent().Name;
    }

    public string GetEmptyData()
    {
      return string.Empty;
    }

  }

}
