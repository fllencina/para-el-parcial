using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace EntidadesHechas
{
    public static class GuardaString
    {
        public static bool Guardar(this string texto, string archivo)
        {
            string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + archivo;
            StreamWriter sr=default(StreamWriter);
            bool retorno = false;
            try
            {
                if (File.Exists(archivo))
                {
                    sr = new StreamWriter(ruta, true);
                    sr.WriteLine(texto);
                    retorno= true;
                }
                else
                {
                    sr = new StreamWriter(ruta);
                    sr.WriteLine(texto);
                    retorno= true;
                }
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
    }
}
