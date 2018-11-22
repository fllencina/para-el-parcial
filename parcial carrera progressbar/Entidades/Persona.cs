using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Entidades
{
    public class Persona : Corredor
    {
        public delegate void CorrenCallback(int avance, Corredor corredor);
        public event CorrenCallback Corriendo;

        private string _nombre;

        public Persona(string nombre, short velocidadMax, Carril carril)
            :base(velocidadMax,carril)
        {
            this._nombre = nombre;
        }

        public override void Correr()
        {
            int avance;
            while(true)
            {
                avance = Corredor._avance.Next(0, this.VelocidadMaxima);
                Corriendo(avance, this);
                Thread.Sleep(500);
            }            
        }

        public override void Guardar(string path)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(path, true);
                sw.WriteLine(this.ToString());
            }
            catch (Exception ex)
            {
                throw new NoSeGuardoException(ex);
            }
            finally
            {
                sw.Close();
            }            
        }

        public override string ToString()
        {
            return String.Format("{0} en carril {1} a una velocidad máxima de {2}",this._nombre,base.CarrilElegido,base.VelocidadMaxima);
        }

    }
}
