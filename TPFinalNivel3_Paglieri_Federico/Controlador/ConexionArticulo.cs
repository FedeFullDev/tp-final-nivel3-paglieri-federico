using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MetodosElementales;
using Modelo;

namespace Controlador
{
    public class ConexionArticulo
    {


        // método utilizado

        public List<Articulo> ListaArticulo()
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            List<Articulo> listaArticulos = new List<Articulo>();
            try
            {


                conexionCatalogo.EstablecerConexionDB();
                conexionCatalogo.SetearConsulta("Select A.Id as Id, Codigo, Nombre, A.Descripcion as Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, IdMarca, IdCategoria, ImagenUrl, Precio from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = IdMarca AND C.Id = IdCategoria");


                conexionCatalogo.EjecutarLectura();

                while (conexionCatalogo.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)conexionCatalogo.Lector["Id"];
                    articulo.CodigoArticulo = (string)conexionCatalogo.Lector["Codigo"];
                    articulo.Nombre = (string)conexionCatalogo.Lector["Nombre"];
                    articulo.Descripcion = (string)conexionCatalogo.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Descripcion = (string)conexionCatalogo.Lector["Marca"];
                    articulo.Marca.Id = (int)conexionCatalogo.Lector["IdMarca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Descripcion = (string)conexionCatalogo.Lector["Categoria"];
                    articulo.Categoria.Id = (int)conexionCatalogo.Lector["IdCategoria"];


                    articulo.ImagenUrl = (string)conexionCatalogo.Lector["ImagenUrl"];


                    articulo.Precio = (decimal)conexionCatalogo.Lector["Precio"];

                    listaArticulos.Add(articulo);
                }

                return listaArticulos;
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
                conexionCatalogo.CerrarConexionDB();
            }

        }




        public Articulo FiltrarRegistroPorId(int id)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            List<Articulo> listaArticulos = new List<Articulo>();
            try
            {

                conexionCatalogo.EstablecerConexionDB();
                conexionCatalogo.SetearConsulta($"Select ARTICULOS.Id , Codigo , Nombre, ARTICULOS.Descripcion, IdMarca, MARCAS.Descripcion as DescripcionMarca, IdCategoria, CATEGORIAS.Descripcion as DescripcionCategoria ,ImagenUrl,Precio from ARTICULOS Join MARCAS ON IdMarca = MARCAS.Id Join CATEGORIAS ON IdCategoria = CATEGORIAS.Id WHERE ARTICULOS.Id = {id}");
                conexionCatalogo.EjecutarLectura();
                Articulo articulo = new Articulo();

                while (conexionCatalogo.Lector.Read())
                {

                    articulo.Id = (int)conexionCatalogo.Lector["Id"];
                    articulo.CodigoArticulo = (string)conexionCatalogo.Lector["Codigo"];
                    articulo.Nombre = (string)conexionCatalogo.Lector["Nombre"];
                    articulo.Descripcion = (string)conexionCatalogo.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Descripcion = (string)conexionCatalogo.Lector["DescripcionMarca"];
                    articulo.Marca.Id = (int)conexionCatalogo.Lector["IdMarca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Descripcion = (string)conexionCatalogo.Lector["DescripcionCategoria"];
                    articulo.Categoria.Id = (int)conexionCatalogo.Lector["IdCategoria"];

                    articulo.ImagenUrl = (string)conexionCatalogo.Lector["ImagenUrl"];

                    articulo.Precio = (decimal)conexionCatalogo.Lector["Precio"];


                }

                return articulo;

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
                conexionCatalogo.CerrarConexionDB();
            }
        }

        // Método utilizado

        public List<Articulo> FiltrarRegistrosPorId(int id)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            List<Articulo> listaArticulos = new List<Articulo>();
            try
            {

                conexionCatalogo.EstablecerConexionDB();
                conexionCatalogo.SetearConsulta($"Select ARTICULOS.Id , Codigo , Nombre, ARTICULOS.Descripcion, IdMarca, MARCAS.Descripcion as DescripcionMarca, IdCategoria, CATEGORIAS.Descripcion as DescripcionCategoria ,ImagenUrl,Precio from ARTICULOS Join MARCAS ON IdMarca = MARCAS.Id Join CATEGORIAS ON IdCategoria = CATEGORIAS.Id WHERE ARTICULOS.Id = {id}");
                conexionCatalogo.EjecutarLectura();


                while (conexionCatalogo.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = (int)conexionCatalogo.Lector["Id"];
                    articulo.CodigoArticulo = (string)conexionCatalogo.Lector["Codigo"];
                    articulo.Nombre = (string)conexionCatalogo.Lector["Nombre"];
                    articulo.Descripcion = (string)conexionCatalogo.Lector["Descripcion"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Descripcion = (string)conexionCatalogo.Lector["DescripcionMarca"];
                    articulo.Marca.Id = (int)conexionCatalogo.Lector["IdMarca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Descripcion = (string)conexionCatalogo.Lector["DescripcionCategoria"];
                    articulo.Categoria.Id = (int)conexionCatalogo.Lector["IdCategoria"];

                    articulo.ImagenUrl = (string)conexionCatalogo.Lector["ImagenUrl"];

                    articulo.Precio = (decimal)conexionCatalogo.Lector["Precio"];

                    listaArticulos.Add(articulo);

                }

                return listaArticulos;

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
                conexionCatalogo.CerrarConexionDB();
            }
        }

    }
}
