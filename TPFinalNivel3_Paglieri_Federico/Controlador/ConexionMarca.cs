using MetodosElementales;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ConexionMarca
    {
       public  List<Marca> ListaMarca()
        {
            ConexionCatalogoDB catalogoDB = new ConexionCatalogoDB();
            try
            {
                List<Marca> listaMarcas = new List<Marca>();
                catalogoDB.EstablecerConexionDB();
                catalogoDB.SetearConsulta("Select Id, Descripcion from MARCAS\r\n");
                catalogoDB.EjecutarLectura();

                while (catalogoDB.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)catalogoDB.Lector["Id"];
                    marca.Descripcion = (string)catalogoDB.Lector["Descripcion"];

                    listaMarcas.Add(marca);
                }

                return listaMarcas;

            }
            catch (SqlException)
            {
                throw;
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                catalogoDB.CerrarConexionDB();
            }

        }



    }
}
