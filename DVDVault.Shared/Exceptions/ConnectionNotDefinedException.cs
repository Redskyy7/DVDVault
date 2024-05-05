using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDVault.Shared.Exceptions;
public class ConnectionNotDefinedException : Exception
{
    public ConnectionNotDefinedException(string message) : base(message) { }
}
