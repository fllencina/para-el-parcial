using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Excepciones;

namespace EntidadesAbstractas
{
    

    abstract public class Persona
    {
        public enum ENacionalidad
        {
            Argentino,
            Extranjero
        }

        private string apellido;
        private string nombre;
        private int dni;
        private ENacionalidad nacionalidad;

        #region "Constructores"
        public Persona()
        {
        }
        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            Nombre = nombre;
            Apellido = apellido;
            Nacionalidad = nacionalidad;
        }
        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad) : this(nombre, apellido, nacionalidad)
        {
            DNI = dni;
        }
        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad) : this(nombre, apellido, nacionalidad)
        {
            StringToDNI = dni;
        }
        #endregion

        #region "Validaciones"
        /// <summary>
        /// valida que el dato int pasado como parametro cumpla con formato y condicion de nacionalidad por rango de DNI
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns>numero validado</returns>
        private int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            if (dato < 1 || dato > 99999999) //Si está fuera de los rangos permitidos para cualquier nacionalidad.
            {
                throw new DniInvalidoException("DNI en rango inválido.");
            }

            switch (nacionalidad)
            {
                case ENacionalidad.Argentino:
                    if (dato > 89999999)
                    {
                        throw new NacionalidadInvalidaException();
                    }
                    break;
                case ENacionalidad.Extranjero:
                    if (dato <= 89999999)
                    {
                        throw new NacionalidadInvalidaException();
                    }
                    break;
            }
            return dato;
        }
        /// <summary>
        /// valida que el dato string pasado como parametro cumpla con formato y condicion de nacionalidad por rango de DNI
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns>numero validado</returns>
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int datoInt;
            Regex Val = new Regex(@"^[0-9]+[0-9\.]*$");
            if (Val.IsMatch(dato))
            {
                dato = dato.Replace(".", "");
                int.TryParse(dato, out datoInt);
            }
            else
            {
                throw new DniInvalidoException("DNI ingresado tiene un formato inválido.");
            }

            return ValidarDni(nacionalidad, datoInt);

        }
        /// <summary>
        /// valida que el dato string pasado como parametro cumpla con formato de nombre o apellido
        /// </summary>
        /// <param name="dato"></param>
        /// <returns>nombre/apellido validado</returns>
        private string ValidarNombre(string dato)
        {
            Regex Val = new Regex("[a-zA-Z]");
            if (Val.IsMatch(dato))
            {
                if (dato.Length > 3)
                {
                    return dato;
                }
            }
            return " ";
        }
        #endregion

        #region "Propiedades"
        /// <summary>
        /// hace publicos los atributos
        /// </summary>
        public string Apellido
        {
            get
            {
                return this.apellido;
            }
            set
            {
                this.apellido = ValidarNombre(value);
            }
        }
        /// <summary>
        /// hace publicos los atributos
        /// </summary>
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = ValidarNombre(value);
            }
        }
        /// <summary>
        /// hace publicos los atributos
        /// </summary>
        public int DNI
        {
            get
            {
                return this.dni;
            }
            set
            {
                this.dni = ValidarDni(this.nacionalidad, value);
            }
        }
        /// <summary>
        /// hace publicos los atributos
        /// </summary>
        public ENacionalidad Nacionalidad
        {
            get
            {
                return this.nacionalidad;
            }
            set
            {
                this.nacionalidad = value;
            }
        }
        /// <summary>
        /// hace publicos los atributos
        /// </summary>
        public string StringToDNI
        {
            set
            {
               this.dni = ValidarDni(this.nacionalidad, value);
            }
        }
        #endregion

        #region "Metodo"
        /// <summary>
        /// muestra datos de nombre y apellido mas la nacionalidad de la persona.
        /// </summary>
        /// <returns>string datos</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("NOMBRE COMPLETO: {0}, {1}\nNACIONALIDAD: {2} ", Apellido, Nombre, Nacionalidad);
            return sb.ToString();
        }
        #endregion
    }
}
