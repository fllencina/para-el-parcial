using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lencina.Fernanda._2C.TP3
{
    enum ENacionalidad
    {
        Argentino, Extranjero
    }

    abstract class Persona
    {
        private string apellido;
        private string nombre;
        private int dni;
        ENacionalidad nacionalidad;

        #region "Constructores sin terminar"
        public Persona()
        {

        }
        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {

        }
        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad)
        {

        }
        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad)
        {

        }

        #endregion

        #region "Validaciones"
        private int ValidarDni(ENacionalidad nacionalidad, int dato)//falta validar si son solo numeros, y formato --DniInvalidoException
        {           
            switch (nacionalidad)
            {
                case ENacionalidad.Argentino:
                    try
                    {
                        if (dato > 1 && dato < 89999999)
                        {
                            return dato;
                        }
                    }
                    catch
                    {
                        // throw new NacionalidadInvalidaException;
                    }
                    break;
                case ENacionalidad.Extranjero:
                    try
                    {
                        if (dato > 90000000 && dato < 99999999)
                        {
                            return dato;
                        }
                    }
                    catch
                    {
                        // throw new NacionalidadInvalidaException;
                    }
                    break;
            }
            return -1;
        }
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int retorno = -1;
            if (int.TryParse(dato, out int conver))
            {
                retorno = ValidarDni(nacionalidad, conver);
            }

            return retorno;
        }
        private string ValidarNombre(string dato)
        {
            Regex Val = new Regex("[a-zA-Z]");
            if(Val.IsMatch(dato))
            {
                if(dato.Length>3)
                {
                    return dato;
                }
            }
            return " ";
        }

        #endregion

        #region "Propiedades"
        public string Apellido
        {
            get
            {
                return this.apellido;
            }
            set
            {
                if(ValidarNombre(value)!=" ")
                {
                    this.apellido = value;
                }
            }
        }
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                if(ValidarNombre(value)!=" ")
                {
                    this.nombre = value;
                }
            }
        }
        public int DNI
        {
            get
            {
                return this.dni;
            }
            set
            {
                (ValidarDni(value)!=-1)
            }
        }
        #endregion
    }
}
