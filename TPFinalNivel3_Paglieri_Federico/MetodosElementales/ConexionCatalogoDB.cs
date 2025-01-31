using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosElementales
{
    public class ConexionCatalogoDB
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }



        public void EstablecerConexionDB()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                conexion = new SqlConnection(connectionString);
                


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetearConsulta(string consulta)
        {
            try
            {
                

                comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void EjecutarLectura()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

            conexion = new SqlConnection(connectionString);

           
            comando.Connection = conexion;
            conexion.Open();

            lector = comando.ExecuteReader();

        }

        public void InsertarModificarEliminar()
        {
            try
            {
               

               
                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ConsultaConStoreProcedure(string procedimientoAlmacenado)
        {
            try
            {
                comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = procedimientoAlmacenado;

            }
            catch (Exception)
            {
                throw;
            }


        }


        public void CerrarConexionDB()
        {

            try
            {
                conexion.Close();

            }
            catch (Exception)
            {
                throw;
            }

        }


        public void SetearParametros(string parametro, object valor)
        {
            try
            {
                comando.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




    }
}
