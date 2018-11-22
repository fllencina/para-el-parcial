using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Interfaces;

namespace Entidades
{
    public class LosHilos : IRespuesta<int>
    {
        public delegate void AvisoFinHandler(string mensaje); 
        public event AvisoFinHandler AvisoFin;

        private int id;
        private List<InfoHilo> misHilos;

        public string Bitacora
        {
            get
            {
                StreamReader sr = null;
                string retorno = "";
                try
                {
                    sr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/bitacora.txt");
                    retorno = sr.ReadToEnd();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sr.Close();
                }
                return retorno;
            }
            set
            {
                StreamWriter sw = null;

                try
                {
                    sw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/bitacora.txt",true);
                    sw.WriteLine(value);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sw.Close();
                }               
            }
        }

        public LosHilos()
        {
            this.id = 0;
            misHilos = new List<InfoHilo>();
        }

        private static LosHilos AgregarHilo(LosHilos hilos)
        {
            hilos.id++;
            InfoHilo ih = new InfoHilo(hilos.id,hilos);
            hilos.misHilos.Add(ih);
            return hilos;
        }

        public void RespuestaHilo(int id)
        {
            string mensaje = String.Format("Terminó el hilo {0}", id);
            this.Bitacora = mensaje;
            AvisoFin(mensaje);
        }

        public static LosHilos operator + (LosHilos hilos, int cantidad)
        {
            if(cantidad < 1)
            {
                throw new CantidadInvalidaException();
            }
            else
            {
                for(int i = 0; i < cantidad; i++)
                {
                    LosHilos.AgregarHilo(hilos);
                }                
            }

            return hilos;
        }
    }
}
