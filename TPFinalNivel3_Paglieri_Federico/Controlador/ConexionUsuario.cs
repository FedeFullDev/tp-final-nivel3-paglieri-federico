using MetodosElementales;
using Modelo;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class ConexionUsuario
    {

        public static bool validarLogin(Usuario usuario, bool pageLogin)
        {


            ConexionCatalogoDB conexionDB = new ConexionCatalogoDB();

            bool validarDatosRepetidos = false;

            /* Este método FiltrarRegistroPorId no está mal, pero yo solo necesito validar si existe el usuario y contraseña en la tabla de usuarios
              en la base de datos, pero ese método lee mucha más información, por lo que es un consumo innecesario de la DB.Es mejor una consulta
              embebida ya que solo necesito leer dos campos de la base de datos, al que hace referencia el id.
            */

            //conexion.FiltrarRegistroPorId(usuario.id);

            if (pageLogin == false)
            {


                try
                {
                    conexionDB.EstablecerConexionDB();
                    conexionDB.SetearConsulta("Select Id, admin, email, pass, nombre, apellido, urlImagenPerfil from USERS where email = @email");
                    conexionDB.SetearParametros("@email", usuario.email);
                    //conexionDB.SetearParametros("@contraseña", usuario.contraseña);

                    conexionDB.EjecutarLectura();


                    while (conexionDB.Lector.Read())
                    {


                        if ((usuario.email == (string)conexionDB.Lector["email"]))
                        {

                            validarDatosRepetidos = true;

                            usuario.id = (int)conexionDB.Lector["id"];
                            bool validarPermisoAdmin = (bool)conexionDB.Lector["admin"];
                            if (!(conexionDB.Lector["nombre"] is DBNull))
                            {
                                usuario.nombre = (string)conexionDB.Lector["nombre"];
                            }
                            if (!(conexionDB.Lector["apellido"] is DBNull))
                            {
                                usuario.apellido = (string)conexionDB.Lector["apellido"];
                            }
                            if (!(conexionDB.Lector["urlImagenPerfil"] is DBNull))
                            {
                                usuario.UrlImagen = (string)conexionDB.Lector["urlImagenPerfil"];

                            }

                            if (validarPermisoAdmin == true)
                            {
                                usuario.TIPOUSUARIO = TIPOUSUARIO.si_admin;
                            }
                            else if (validarPermisoAdmin == false)
                            {
                                usuario.TIPOUSUARIO = TIPOUSUARIO.no_admin;
                            }



                        }
                        else
                        {
                            validarDatosRepetidos = false;
                        }

                    }
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
                    conexionDB.CerrarConexionDB();
                }

            }
            else if (pageLogin == true)
            {

                try
                {
                    conexionDB.EstablecerConexionDB();

                    conexionDB.SetearConsulta("Select Id, admin, email, pass, nombre, apellido, urlImagenPerfil from USERS where email = @email AND pass = @contraseña");
                    conexionDB.SetearParametros("@email", usuario.email);
                    conexionDB.SetearParametros("@contraseña", usuario.contraseña);

                    conexionDB.EjecutarLectura();


                    while (conexionDB.Lector.Read())
                    {


                        if (usuario.email == (string)conexionDB.Lector["email"] && usuario.contraseña == (string)conexionDB.Lector["pass"])
                        {

                            validarDatosRepetidos = true;

                            usuario.id = (int)conexionDB.Lector["id"];
                            bool validarPermisoAdmin = (bool)conexionDB.Lector["admin"];
                            if (!(conexionDB.Lector["nombre"] is DBNull))
                            {
                                usuario.nombre = (string)conexionDB.Lector["nombre"];
                            }
                            if (!(conexionDB.Lector["apellido"] is DBNull))
                            {
                                usuario.apellido = (string)conexionDB.Lector["apellido"];
                            }
                            if (!(conexionDB.Lector["urlImagenPerfil"] is DBNull))
                            {
                                usuario.UrlImagen = (string)conexionDB.Lector["urlImagenPerfil"];

                            }

                            if (validarPermisoAdmin == true)
                            {
                                usuario.TIPOUSUARIO = TIPOUSUARIO.si_admin;
                            }
                            else if (validarPermisoAdmin == false)
                            {
                                usuario.TIPOUSUARIO = TIPOUSUARIO.no_admin;
                            }



                        }
                        else
                        {
                            validarDatosRepetidos = false;
                        }

                    }

                }
                catch (SqlException)
                {
                    throw;
                }
                catch (TimeoutException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conexionDB.CerrarConexionDB();
                }

            }


            return validarDatosRepetidos;




        }


        public void ModificarCampoUsuario(Usuario usuario, string nombreCampo)
        {
            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {


                conexion.EstablecerConexionDB();

                conexion.SetearConsulta("Update USERS set " + nombreCampo + " = @" + nombreCampo + " where Id = @id ");
                if (nombreCampo == "nombre")
                {
                    conexion.SetearParametros("@" + nombreCampo + "", usuario.nombre);
                    conexion.SetearParametros("@id", usuario.id);
                }
                else if (nombreCampo == "apellido")
                {
                    conexion.SetearParametros("@" + nombreCampo.Trim().Replace(" ", "") + "", usuario.apellido);
                    conexion.SetearParametros("@id", usuario.id);
                }
                else if (nombreCampo == "urlImagenPerfil" /*&& !string.IsNullOrEmpty(nombreCampo)*/)
                {
                    conexion.SetearParametros("@" + nombreCampo.Trim().Replace(" ", "") + "", usuario.UrlImagen);
                    conexion.SetearParametros("@id", usuario.id);
                }




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

        public Usuario SeleccionarUsuarioId(int Id)
        {

            ConexionCatalogoDB conexion = new ConexionCatalogoDB();
            try
            {
                conexion.EstablecerConexionDB();
                conexion.SetearConsulta($"SELECT Id, email, pass, nombre, apellido, urlImagenPerfil FROM USERS WHERE Id = {Id}");
                conexion.EjecutarLectura();

                Usuario usuario = new Usuario();

                while (conexion.Lector.Read())
                {
                    usuario.id = (int)conexion.Lector["Id"];

                    if (!(conexion.Lector["email"] is DBNull))
                    {
                        usuario.email = (string)conexion.Lector["email"];
                    }
                    else
                    {
                        usuario.email = string.Empty;
                    }

                    if (!(conexion.Lector["pass"] is DBNull))
                    {
                        usuario.contraseña = (string)conexion.Lector["pass"];
                    }
                    else
                    {
                        usuario.contraseña = "";
                    }

                    if (!(conexion.Lector["nombre"] is DBNull))
                    {
                        usuario.nombre = (string)conexion.Lector["nombre"];
                    }
                    else
                    {
                        usuario.nombre = string.Empty;
                    }

                    if (!(conexion.Lector["apellido"] is DBNull))
                    {
                        usuario.apellido = (string)conexion.Lector["apellido"];
                    }
                    else
                    {
                        usuario.apellido = string.Empty;
                    }
                    if (!(conexion.Lector["UrlImagenPerfil"] is DBNull))
                    {
                        usuario.UrlImagen = (string)conexion.Lector["urlImagenPerfil"];
                    }
                    else
                    {
                        usuario.UrlImagen = string.Empty;

                    }
                    


                }
                return usuario;
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
