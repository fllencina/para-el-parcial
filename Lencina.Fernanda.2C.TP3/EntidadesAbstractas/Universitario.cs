using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesAbstractas
{
    public abstract class Universitario:Persona
    {
        protected int legajo;

        #region "Constructores"
        public Universitario()
        { }
        public Universitario(int legajo,string nombre, string apellido, string dni,ENacionalidad nacionalidad):base(nombre,apellido,dni,nacionalidad)
        {
            this.legajo = legajo;
        }
        #endregion

        #region "Metodos"
        /// <summary>
        /// Metodo protegido y abstracto
        /// </summary>
        protected abstract string ParticiparEnClase();
        /// <summary>
        /// metodo protegido y virtual
        /// </summary>
        /// <returns>string datos de la persona mas numero de legajo de universitario</returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} \nLEGAJO NÚMERO: {1}\n", base.ToString(), this.legajo);
            return sb.ToString();
        }
        #endregion

        #region "Sobrecargas"
        /// <summary>
        /// compara igualdad entre dos universitarios
        /// </summary>
        /// <param name="pg1">universitario</param>
        /// <param name="pg2">universitario</param>
        /// <returns>true si son iguales o false si no lo son</returns>
        public static bool operator==(Universitario pg1, Universitario pg2)
        {
            return (pg1.Equals(pg2));
        }
        /// <summary>
        /// compara diferencia entre dos universitarios
        /// </summary>
        /// <param name="pg1">universitario</param>
        /// <param name="pg2">universitario</param>
        /// <returns>true si son diferentes o false si no lo son</returns>
        public static bool operator!=(Universitario pg1,Universitario pg2)
        {
            return !(pg1 == pg2);
        }
        /// <summary>
        /// compara igualdad entre dos objetos,primero valida que ambos sean de tipo Universitario,luego son iguales si sus numeros de legajo y dni son iguales
        /// </summary>
        /// <param name="pg1">universitario</param>
        /// <param name="pg2">universitario</param>
        /// <returns>true si son iguales o false si no lo son</returns>
        public override bool Equals(object obj)
        {
            if(obj is Universitario && obj !=null)
            {
                Universitario unUniversitario = (Universitario)obj;
                if (unUniversitario.legajo == this.legajo && unUniversitario.DNI == this.DNI)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
