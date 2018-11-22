using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Excepciones;
namespace EntidadesHechas
{
    static class PaqueteDAO
    {
         static SqlCommand comando;
         static SqlConnection conexion;

        static PaqueteDAO()
        {
            conexion = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=correo-sp-2017;integrated Security=true");
        }

        public static bool Insertar(Paquete p)
        {
            comando = new SqlCommand();
            bool retorno = false;
            try
            {
                conexion.Open();
                comando = new SqlCommand("INSERT INTO Paquetes(DireccionEntrega,TrackingId,Alumno) VALUES ('"+p.DireccionEntrega + "','" + p.TrackingID + "','Lencina.Fernanda.2C')", conexion);
                comando.ExecuteNonQuery();
                retorno = true;
               
            }
            catch (TrackingRepetidoException ex)
            {
                throw ex;
            }
            finally
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return retorno;
        }
    }
}
