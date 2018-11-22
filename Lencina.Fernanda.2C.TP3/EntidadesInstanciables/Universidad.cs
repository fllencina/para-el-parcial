using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesInstanciables;
using EntidadesAbstractas;
using Archivos;
using Excepciones;

namespace EntidadesInstanciables
{
    
    public class Universidad
    {
        public enum EClases
        {
            Programacion, Laboratorio, Legislacion, SPD
        }

        private List<Alumno> alumnos;
        private List<Jornada> jornada;
        private List<Profesor> profesores;

        #region "Constructor"
        public Universidad()
        {
            alumnos = new List<Alumno>();
            jornada = new List<Jornada>();
            profesores = new List<Profesor>();
        }
        #endregion

        #region "Propiedades"

        /// <summary>
        /// hace publicos los atributos
        /// </summary>
        public List<Alumno> Alumnos
        {
            get
            {
                return this.alumnos;
            }
            set
            {
                this.alumnos = value;
            }
        }
        public List<Profesor> Instructores
        {
            get
            {
                return this.profesores;
            }
            set
            {
                this.profesores = value;
            }
        }
        public List<Jornada> Jornada
        {
            get
            {
                return this.jornada;
            }
            set
            {
                this.jornada = value;
            }
        }
        public Jornada this[int i]
        {
            get
            {
                if (i >= 0 && i < this.jornada.Count)
                    return this.jornada[i];
                else
                    return null;
            }
            set
            {
                if (i >= 0 && i < this.jornada.Count)
                    this.jornada[i] = value;
            }
        }
        #endregion

        #region "Metodos"
        private static string MostrarDatos(Universidad uni)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Jornada a in uni.Jornada)
            {
                sb.AppendLine("<------------------------------------------------------------------------>");
                sb.AppendLine("JORNADA:");
                sb.AppendLine(a.ToString());
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return MostrarDatos(this);
        }
        /// <summary>
        /// guarda en el escritorio archivo xml de los datos de la universidad, profesores, clases, alumnos
        /// </summary>
        /// <param name="uni">recibe como parametro a la universidad</param>
        public static void Guardar(Universidad uni)
        {
            Xml<Universidad> aux = new Xml<Universidad>();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = path + "\\Universidad.xml";
            try
            {
                aux.Guardar(path, uni);
            }
            catch(Exception e)
            {
                throw new ArchivosException(e.InnerException);
            }
            
        }
        /// <summary>
        /// lee de un xml los datos de la universidad.
        /// </summary>
        /// <returns></returns>
        public static Universidad Leer()
        {
            Xml<Universidad> aux = new Xml<Universidad>();
            Universidad universidad = null;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = path + "\\Universidad.xml";
            try
            {
                aux.Leer(path, out  universidad);
            }
            catch (Exception e)
            {
                throw new ArchivosException(e.InnerException);
            }
            return universidad;
        }
        #endregion

        #region "Sobrecarga"
        /// <summary>
        /// alumno sera igual a la universidad si esta inscipto en ella
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns>true si esta incripto, false si no</returns>
        public static bool operator ==(Universidad g, Alumno a)
        {
            if (g.alumnos.Contains(a))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// alumno sera diferente a la universidad si no esta inscipto en ella
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns>false si  esta incripto, true si no</returns>
        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }
        /// <summary>
        /// el porfesor sera igual a la universidad si dicta clases en ella
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns>true si da clases, false si no</returns>
        public static bool operator ==(Universidad g, Profesor i)
        {
            if (g.profesores.Contains(i))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// el porfesor sera diferente a la universidad si no dicta clases en ella
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns>false si da clases, true si no</returns>
        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }
        /// <summary>
        /// la clase sera igual a la universidad si hay un porfesor que pueda dar la clase
        /// </summary>
        /// <param name="u"></param>
        /// <param name="clase"></param>
        /// <returns>primer profesor que  puede dar la clase,sino lanza excepcion</returns>
        public static Profesor operator ==(Universidad u, EClases clase)
        {
            foreach (Profesor i in u.profesores)
            {
                if (i == clase)
                {
                    return i;
                }
            }
            throw new SinProfesorException();
        }
        /// <summary>
        /// la clase sera diferente a la universidad si no hay un porfesor que pueda dar la clase
        /// </summary>
        /// <param name="u"></param>
        /// <param name="clase"></param>
        /// <returns>primer profesor que no puede dar la clase</returns>
        public static Profesor operator !=(Universidad u, EClases clase)
        {
            foreach (Profesor i in u.profesores)
            {
                if (i != clase)
                {
                    return i;
                }
            }
            throw new SinProfesorException();
        }
        /// <summary>
        /// agrega una jornada indicando clase, y profesor y la lista de alumnos
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns>universidad</returns>
        public static Universidad operator +(Universidad g, EClases clase)
        {
            Jornada nuevaJornada = new Jornada(clase, g == clase);//(g==clase) me retorna el profesor de la clase, el constructor de jornada recibe por parametro la clase y el profesor
            foreach(Alumno a in g.alumnos)
            {
                if(a==clase)//si el alumno toma la clase 
                {
                    nuevaJornada.Alumnos.Add(a);
                }
            }
            g.jornada.Add(nuevaJornada);
            return g;
        }
        /// <summary>
        /// se agrega un alumno a la universidad validando que no este previamente cargado
        /// </summary>
        /// <param name="u"></param>
        /// <param name="a"></param>
        /// <returns>universidad</returns>
        public static Universidad operator +(Universidad u, Alumno a)
        {        
            if(u!=a)
            {
                foreach (Alumno e in u.alumnos)
                {
                   // System.Diagnostics.Debug.Assert(a.DNI != 12234456);

                    if (e.DNI == a.DNI)
                    {                        
                        throw new AlumnoRepetidoException();        
                    }    
                }                
                 u.alumnos.Add(a); 
            }         
            return u;    
        }
        /// <summary>
        /// agrega un profesor a la universidad validando que no este previamente cargado
        /// </summary>
        /// <param name="u"></param>
        /// <param name="i"></param>
        /// <returns>universidad</returns>
        public static Universidad operator +(Universidad u, Profesor i)
        {
            if(u!=i)
            {
                u.profesores.Add(i);     
            }
            return u;
        }
        #endregion
    }
}
