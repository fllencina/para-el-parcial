using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Archivos
{
    public class Xml<T>:IArchivo<T>
    {
        public void Guardar(string archivo, T datos)
        {
            XmlSerializer sr = new XmlSerializer(typeof(T));
            XmlTextWriter writer = new XmlTextWriter(archivo, null);
            try
            {   
                sr.Serialize(writer, datos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                writer.Close();
            }

        }
        public void Leer(string archivo, out T datos)
        {
            XmlTextReader reader;   
            XmlSerializer sr;
            reader = new XmlTextReader(archivo);
            sr = new XmlSerializer(typeof(T));
            datos = (T)sr.Deserialize(reader);
            reader.Close();
            
        }

    }
}
