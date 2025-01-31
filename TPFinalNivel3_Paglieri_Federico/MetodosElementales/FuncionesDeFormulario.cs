using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosElementales
{
    public class FuncionesDeFormulario
    {

        // método utilizado
        public void Agregar(Articulo nuevoArticulo)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            try
            {


                conexionCatalogo.SetearConsulta("Insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca , @IdCategoria, @ImagenUrl, @Precio)");
                conexionCatalogo.SetearParametros("@Codigo", nuevoArticulo.CodigoArticulo);
                conexionCatalogo.SetearParametros("@Nombre", nuevoArticulo.Nombre);
                conexionCatalogo.SetearParametros("@Descripcion", nuevoArticulo.Descripcion);
                conexionCatalogo.SetearParametros("@IdMarca", nuevoArticulo.Marca.Id);
                conexionCatalogo.SetearParametros("@IdCategoria", nuevoArticulo.Categoria.Id);
                conexionCatalogo.SetearParametros("@ImagenUrl", nuevoArticulo.ImagenUrl);
                conexionCatalogo.SetearParametros("@Precio", nuevoArticulo.Precio);

                conexionCatalogo.InsertarModificarEliminar();
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



        // método utilizado
        public void AgregarconStoreProcedure(Articulo nuevoArticulo)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            try
            {


                conexionCatalogo.ConsultaConStoreProcedure("AgregarArticulo");
                conexionCatalogo.SetearParametros("@Codigo", nuevoArticulo.CodigoArticulo);
                conexionCatalogo.SetearParametros("@Nombre", nuevoArticulo.Nombre);
                conexionCatalogo.SetearParametros("@Descripcion", nuevoArticulo.Descripcion);
                conexionCatalogo.SetearParametros("@IdMarca", nuevoArticulo.Marca.Id);
                conexionCatalogo.SetearParametros("@IdCategoria", nuevoArticulo.Categoria.Id);
                conexionCatalogo.SetearParametros("@ImagenUrl", nuevoArticulo.ImagenUrl);
                conexionCatalogo.SetearParametros("@Precio", nuevoArticulo.Precio);
                conexionCatalogo.InsertarModificarEliminar();




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



        public void AgregarUsuario(Usuario usuario)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta("insert into USERS(email, pass) values(@email, @contraseña)");
                conexion.SetearParametros("@email", usuario.email);
                conexion.SetearParametros("@contraseña", usuario.contraseña);
                conexion.InsertarModificarEliminar();
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
                conexion.CerrarConexionDB();
            }
        }







        // método utilizado


        public void Modificar(Articulo modificar)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            try
            {


                conexionCatalogo.SetearConsulta("Update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id ");
                conexionCatalogo.SetearParametros("@Codigo", modificar.CodigoArticulo);
                conexionCatalogo.SetearParametros("@Nombre", modificar.Nombre);
                conexionCatalogo.SetearParametros("@Descripcion", modificar.Descripcion);
                conexionCatalogo.SetearParametros("@IdMarca", modificar.Marca.Id);
                conexionCatalogo.SetearParametros("@IdCategoria", modificar.Categoria.Id);
                conexionCatalogo.SetearParametros("@ImagenUrl", modificar.ImagenUrl);
                conexionCatalogo.SetearParametros("@Precio", modificar.Precio);
                conexionCatalogo.SetearParametros("@Id", modificar.Id);

                conexionCatalogo.InsertarModificarEliminar();
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


        public void Eliminar(int id)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            try
            {
                conexionCatalogo.SetearConsulta("delete from ARTICULOS where id = @id");
                conexionCatalogo.SetearParametros("@id", id);
                conexionCatalogo.InsertarModificarEliminar();
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


        public void AgregarMarca(string marca)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            try
            {
                conexionCatalogo.SetearConsulta("insert into MARCAS(Descripcion) values (@Descripcion)");
                conexionCatalogo.SetearParametros("@Descripcion", marca);
                conexionCatalogo.InsertarModificarEliminar();
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

        public void ModificarMarca(Marca marca)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                conexion.SetearConsulta("update MARCAS set Descripcion = @Descripcion where Id = @Id");
                conexion.SetearParametros("@Descripcion", marca.Descripcion);
                conexion.SetearParametros("@Id", marca.Id);

                conexion.InsertarModificarEliminar();

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
                conexion.CerrarConexionDB();
            }

        }

        public void EliminarMarca(int Id)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                conexion.SetearConsulta("delete from MARCAS where Id = @Id");
                conexion.SetearParametros("@Id", Id);
                conexion.InsertarModificarEliminar();
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
                conexion.CerrarConexionDB();
            }
        }

        public void AgregarCategoria(string categoria)
        {
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            try
            {
                conexionCatalogo.SetearConsulta("insert into CATEGORIAS(Descripcion) values(@Descripcion)");
                conexionCatalogo.SetearParametros("@Descripcion", categoria);
                conexionCatalogo.InsertarModificarEliminar();
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

        public void ModificarCategoria(Categoria categoria)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {

                conexion.SetearConsulta("update CATEGORIAS set Descripcion = @Descripcion where Id = @Id");
                conexion.SetearParametros("@Descripcion", categoria.Descripcion);
                conexion.SetearParametros("@Id", categoria.Id);
                conexion.InsertarModificarEliminar();
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
                conexion.CerrarConexionDB();
            }
        }

        public void EliminarCategoria(int Id)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                conexion.SetearConsulta("delete from CATEGORIAS where Id = @Id");
                conexion.SetearParametros("@Id", Id);
                conexion.InsertarModificarEliminar();
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
                conexion.CerrarConexionDB();
            }

        }


        // Método utilizado
        public List<Articulo> Filtrar(string Campo, string Criterio, string Filtro)
        {
            List<Articulo> articulos = new List<Articulo>();
            ConexionCatalogoDB conexionCatalogo = new ConexionCatalogoDB();
            string consulta = "Select Codigo, Nombre, Precio, ImagenUrl from ARTICULOS where ";
            try
            {



                if (Campo == "Código")
                {
                    switch (Criterio)
                    {
                        case "Empieza con la letra/palabra":
                            consulta += "Codigo like '" + Filtro + "%'";
                            break;
                        case "Contiene la letra/palabra":
                            consulta += "Codigo like '%" + Filtro + "%'";
                            break;
                        case "Termina con la letra/palabra":
                            consulta += "Codigo like '%" + Filtro + "'";
                            break;

                    }
                }
                else if (Campo == "Modelo")
                {
                    switch (Criterio)
                    {
                        case "Empieza con la letra/palabra":
                            consulta += "Nombre like '" + Filtro + "%'";
                            break;
                        case "Contiene la letra/palabra":
                            consulta += "Nombre like '%" + Filtro + "%'";
                            break;
                        case "Termina con la letra/palabra":
                            consulta += "Nombre like '%" + Filtro + "'";
                            break;

                    }



                }
                else if (Campo == "Precio")
                {
                    switch (Criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio >" + Filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio <" + Filtro;
                            break;
                        case "Igual a":
                            consulta += "Precio =" + Filtro;
                            break;

                    }

                }



                conexionCatalogo.EstablecerConexionDB();
                conexionCatalogo.SetearConsulta(consulta);
                conexionCatalogo.EjecutarLectura();

                while (conexionCatalogo.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.CodigoArticulo = (string)conexionCatalogo.Lector["Codigo"];
                    articulo.Nombre = (string)conexionCatalogo.Lector["Nombre"];
                    articulo.Precio = (decimal)conexionCatalogo.Lector["Precio"];
                    articulo.ImagenUrl = (string)conexionCatalogo.Lector["ImagenUrl"];
                    articulos.Add(articulo);
                }


                return articulos;

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
        //Funcion para validar el cambio decontraseña en página PerfilUsuario
        //Método utilizado

        public bool validarContraseña(string contraseña)
        {
            bool validarContraseña = false;

            ConexionCatalogoDB conexion = new ConexionCatalogoDB();

            try
            {
                List<Usuario> listUsuario = new List<Usuario>();
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta("Select pass from USERS");
                conexion.EjecutarLectura();
                while (conexion.Lector.Read())
                {
                    Usuario usuario = new Usuario();

                    usuario.contraseña = (string)conexion.Lector["pass"];
                    listUsuario.Add(usuario);
                }

                foreach (Usuario password in listUsuario)
                {
                    if (contraseña == password.contraseña)
                    {
                        validarContraseña = true;

                        break;

                    }
                    else
                    {
                        validarContraseña = false;
                    }
                }

                return validarContraseña;

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
                conexion.CerrarConexionDB();
            }


        }

        //Funcion para validar el cambio decontraseña en página PerfilUsuario
        //Método Utilizado

        public void ModificarContraseña(string contraseñaModificada, string contraseñaNueva)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            List<Usuario> listUsuario = new List<Usuario>();

            try
            {
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta("SELECT Id, pass from USERS");
                conexion.EjecutarLectura();
                while (conexion.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.id = (int)conexion.Lector["Id"];
                    usuario.contraseña = (string)conexion.Lector["pass"];
                    listUsuario.Add(usuario);
                }

                /*Filtré la lista para ahora biscar el registro en donde está la contraseña que cambió el usuario*/

                Usuario userModificado = listUsuario.Find(registro => registro.contraseña == contraseñaModificada);

                conexion.SetearConsulta($"update USERS set pass = '{contraseñaNueva}' where Id = {userModificado.id}");
                conexion.InsertarModificarEliminar();




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
                conexion.CerrarConexionDB();
            }
        }

        public void EliminarCuenta(int id)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();

            try
            {
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta($"delete from USERS where id = {id}");
                conexion.InsertarModificarEliminar();

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
                conexion.CerrarConexionDB();
            }
        }


        public List<Articulo> SelectProductosFavoritos(int idUsuarioFromSessionUsuarioLogin)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                List<Articulo> listProductosFavoritos = new List<Articulo>();

                conexion.EstablecerConexionDB();
                conexion.ConsultaConStoreProcedure("CARGAR_PRODUCTOS_FAVORITOS");
                conexion.SetearParametros("@SessionIdUsuario", idUsuarioFromSessionUsuarioLogin);
                conexion.EjecutarLectura();
                while (conexion.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)conexion.Lector["Id"];
                    articulo.Nombre = (string)conexion.Lector["Nombre"];
                    articulo.CodigoArticulo = (string)conexion.Lector["Codigo"];
                    articulo.Marca = new Marca();
                    articulo.Marca.Descripcion = (string)conexion.Lector["Marca"];
                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Descripcion = (string)conexion.Lector["Categoria"];
                    articulo.Precio = (decimal)conexion.Lector["Precio"];


                    listProductosFavoritos.Add(articulo);
                }

                return listProductosFavoritos;
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
                conexion.CerrarConexionDB();
            }
        }


        public void AgregarProdutoFavorito(int idProducto, int idUsuario)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();

            try
            {
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta("insert into PRODUCTOS_FAVORITOS(IdArticulo, IdUser) values(@IdArticulo, @IdUsuario)");
                conexion.SetearParametros("@IdArticulo", idProducto);
                conexion.SetearParametros("@IdUsuario", idUsuario);
                conexion.InsertarModificarEliminar();
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
                conexion.CerrarConexionDB();
            }



        }

        public void EliminarProductoFavorito(int idProducto, int idUsuario)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta("delete from PRODUCTOS_FAVORITOS where IdArticulo = @IdArticulo and IdUser = @IdUsuario");
                conexion.SetearParametros("@IdArticulo", idProducto);
                conexion.SetearParametros("@IdUsuario", idUsuario);
                conexion.InsertarModificarEliminar();
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
                conexion.CerrarConexionDB();
            }



        }

    }
}
