using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace Archivos
{
    public class Sql : IArchivo<Queue<Patente>>
    {
        SqlCommand comando;
        SqlConnection conexion;
        
        public Sql()
        {
            comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            string str = @"Data source=.\sqlexpress; initial catalog=patentes-sp-2018;integrated security=true";
            conexion = new SqlConnection(str);

        }
        public void Guardar(string tabla, Queue<Patente> datos)
        {
             foreach (Patente p in datos)
            {
                comando.CommandText = "INSERT INTO " + tabla + "(patente,tipo) values('" + p.CodigoPatente + "','" + p.TipoCodigo + "')";
                try
                {
                    conexion.Open();
                    comando.Connection = conexion;
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (this.conexion.State == ConnectionState.Open)
                    {
                        this.conexion.Close();
                    }
                }
            }
        }
        public void Leer(string tabla, out Queue<Patente> datos)
        {
            string str = @"Data source=.\sqlexpress; initial catalog=patentes-sp-2018;integrated security=true";
            conexion = new SqlConnection(str);

            datos = new Queue<Patente>();
            
            comando.CommandText = "select patente,tipo from " + tabla;
            
            try
            {
                conexion.Open();
                comando.Connection = conexion;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    string codPatente = reader["patente"].ToString();
                    string tipoPatente = reader["tipo"].ToString();
                    Patente p=default(Patente);
                    if (tipoPatente == "Mercosur")
                    {
                        p = new Patente(codPatente, Patente.Tipo.Mercosur);           
                    }
                    if (tipoPatente == "Vieja")
                    {
                        p = new Patente(codPatente, Patente.Tipo.Vieja);
                    }
                    if (p != default(Patente))
                    {
                        datos.Enqueue(p);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }
        }
    }
}
