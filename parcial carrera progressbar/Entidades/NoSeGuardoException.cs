using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class NoSeGuardoException : Exception
    {
        public NoSeGuardoException(Exception innerException) 
            :base("No se guardó correctamente", innerException)
        {

        }
    }
}
