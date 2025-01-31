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
   public class ConexionCategoria
    {
       public List<Categoria> ListaCategoria()
        {
            ConexionCatalogoDB catalogoDB = new ConexionCatalogoDB();

            try
            {
                List<Categoria> listaCategorias = new List<Categoria>();
                catalogoDB.EstablecerConexionDB();
                catalogoDB.SetearConsulta("Select Id, Descripcion from CATEGORIAS\r\n");
                catalogoDB.EjecutarLectura();

                while (catalogoDB.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)catalogoDB.Lector["Id"];
                    categoria.Descripcion = (string)catalogoDB.Lector["Descripcion"];

                    listaCategorias.Add(categoria);
                }

                return listaCategorias;

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
