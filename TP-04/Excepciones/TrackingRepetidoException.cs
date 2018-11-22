using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class TrackingRepetidoException : Exception
    {
        public TrackingRepetidoException(string mensaje):base (mensaje,null)
        { }
        public TrackingRepetidoException(string mensaje, Exception inner):base(mensaje,inner)
        { }
    }
}
