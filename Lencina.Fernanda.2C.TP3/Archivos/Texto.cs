using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        public bool Guardar(string archivo, string datos)
        {
            StreamWriter fileText = null;
            bool retorno = true; 
            try
            {
                fileText = new StreamWriter(archivo);
                fileText.Write(datos);
            }
            catch (Exception)
            {
                retorno = false;  
            }
            finally
            {
                fileText.Close();
            }
            return retorno;
        }
        public bool Leer(string archivo, out string datos)
        {
            StreamReader fileText = null;
            bool retorno = true; 
            try
            {
                fileText = new StreamReader(archivo);
                datos = fileText.ReadToEnd();
            }
            catch (Exception)
            {
                datos = null;
                retorno = false; 
            }
            finally
            {
                fileText.Close();
            }
            return retorno;
        }
    }
    
}
