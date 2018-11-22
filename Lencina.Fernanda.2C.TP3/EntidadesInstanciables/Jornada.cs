using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;
using EntidadesInstanciables;
using Archivos;
using Excepciones;
namespace EntidadesInstanciables
{
    public class Jornada
    {
        private List<Alumno> alumnos;
        private Universidad.EClases clase;
        private Profesor instructor;

        #region "Propiedades"
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
        public Universidad.EClases Clase
        {
            get
            {
                return this.clase;
            }
            set
            {
                this.clase = value;
            }
        }
        public Profesor Instructor
        {
            get
            {
                return this.instructor;
            }
            set
            {
                this.instructor = value;
            }
        }
        #endregion

        #region "Constructores"
        public Jornada()
        {
            alumnos = new List<Alumno>();
        }
        public Jornada(Universidad.EClases clase, Profesor instructor) : this()
        {
            Clase = clase;
            Instructor = instructor;
        }
        #endregion

        #region "Sobrecargas"
        /// <summary>
        ///la joranada y el alumno son iguales si el alumno participa de la clase
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns>true si son iguales, false si no</returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            if (j.alumnos.Contains(a))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///la joranada y el alumno son diferentes si el alumno no participa de la clase
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns>false si son iguales, true si no</returns>
        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }
        /// <summary>
        /// agrega el alumno a la jornada validando que no este previamente cargado
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns> jornada</returns>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            if (j != a)
            {
                foreach (Alumno e in j.alumnos)
                {
                    if (e.DNI == a.DNI)
                    {
                        throw new AlumnoRepetidoException();
                    }
                }
                j.alumnos.Add(a);
            }
            return j;
        }
        #endregion

        #region "Metodos"
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CLASE DE {0} POR {1}", this.Clase, this.Instructor.ToString());
            sb.AppendLine("ALUMNOS:");
            foreach (Alumno alumno in this.Alumnos)
            {
                if(alumno.DNI==this.instructor.DNI)
                { continue; }
                sb.AppendLine(alumno.ToString());
            }
            return sb.ToString();
        }
        public static void Guardar(Jornada jornada)
        {
            Texto archivoTexto = new Texto();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = path + "\\Jornada.txt";
            try
            {
                archivoTexto.Guardar(path, jornada.ToString());
            }
            catch (Exception e)
            {
                throw new ArchivosException(e.InnerException);
            }
        }
        public static string Leer()
        {
            Texto archivoTexto = new Texto();
            string datosJornada;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            path = path + "\\Jornada.txt";
            try
            {
                archivoTexto.Leer(path, out  datosJornada);
            }
           catch(Exception e)
            { 
                throw new ArchivosException(e.InnerException);
            }
            return datosJornada;
        }
        #endregion
    }
}
