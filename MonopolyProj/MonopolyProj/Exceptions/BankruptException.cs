using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyProj.Exceptions
{
    public class BankruptException : Exception
    {
        public BankruptException() { }

        public BankruptException(string message) : base(message) { }
    }
}
