using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Entidades;

namespace Archivos
{
    public class Texto: IArchivo<Queue<Patente>>
    {
        public void Guardar(string archivo, Queue<Patente> datos)
        {
            StreamWriter sw = new StreamWriter(archivo);         
            for(int i=0;i< datos.ToList().Count;i++)
            {
                sw.WriteLine(datos);
            }
            sw.Close();

        }
        public void Leer(string archivo, out Queue<Patente> datos)
        {
            datos = new Queue<Patente>();
            StreamReader sr = new StreamReader(archivo);

            string leido;
            while(sr.ReadLine()!=null)
            {
                leido = sr.ReadLine();
                Patente p;
                try
                {
                    p=PatenteStringExtension.ValidarPatente(leido);
                    datos.Enqueue(p);
                }
                catch(PatenteInvalidaException ex)
                {
                    continue;
                }
            }   
            sr.Close();
        }
    }
}
