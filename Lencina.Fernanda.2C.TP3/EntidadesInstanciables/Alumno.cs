using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesInstanciables;
using EntidadesAbstractas;

namespace EntidadesInstanciables
{
    
    sealed public class Alumno:Universitario
    {
        public enum EEstadoCuenta
        {
            AlDia,
            Deudor,
            Becado
        }

        Universidad.EClases claseQueToma;
        EEstadoCuenta estadoCuenta;

        #region "Constructores"
        public Alumno()
        { }
        public Alumno(int id,string nombre,string apellido,string dni,ENacionalidad nacionalidad,Universidad.EClases claseQueToma):base(id,nombre,apellido,dni,nacionalidad)
        {
            this.claseQueToma =claseQueToma;
        }
        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad,Universidad.EClases claseQueToma, EEstadoCuenta estadoCuenta) : this(id, nombre, apellido, dni, nacionalidad,claseQueToma)
        {
            this.estadoCuenta = estadoCuenta;
        }
        #endregion

        #region "Metodos"
        /// <summary>
        /// muestra datos del alumno, estado de cuenta del mismo y las clases que toma-Usa metodo ParticiparEnClase
        /// </summary>
        /// <returns>string datos del alumno</returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", base.MostrarDatos());
            if (this.estadoCuenta == EEstadoCuenta.AlDia)
            {
                sb.AppendLine("ESTADO DE CUENTA: Cuota al día");
            }
            else
            {
                sb.AppendFormat("ESTADO DE CUENTA: {0}", this.estadoCuenta);
            }
            sb.AppendLine(this.ParticiparEnClase());
            return sb.ToString();
        }
        /// <summary>
        /// muestra clases que toma el alumno
        /// </summary>
        /// <returns>tring datos de clases por alumno</returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("TOMA CLASE DE {0}", this.claseQueToma);
            return sb.ToString();
        }
        /// <summary>
        /// sobreescribo ToString para que haga publicos los datos del alumno
        /// </summary>
        public override string ToString()
        {
            return this.MostrarDatos();
        }
        #endregion

        #region "Sobrecarga"
        /// <summary>
        /// alumno sera igual a la clase cuando toma la clase y estadoCuenta no es deudor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns>true cuando es igual, false cuando no lo es</returns>
        public static bool operator==(Alumno a,Universidad.EClases clase)
        {
            if(a.estadoCuenta!=EEstadoCuenta.Deudor && a.claseQueToma==clase)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// alumno sera distinto solo cuando no toma la clase
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns>true cuando es distinto y false cuando no lo es</returns>
        public static bool operator !=(Alumno a,Universidad.EClases clase)
        {
            return !(a == clase);
        }

            #endregion
    }
}
