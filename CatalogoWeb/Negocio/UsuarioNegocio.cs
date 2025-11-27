using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Login(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select id, email, pass, nombre, apellido, urlImagenPerfil, admin from USERS where email = @email and pass = @pass");
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@pass", usuario.Pass);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["id"];
                    usuario.Admin = (bool)datos.Lector["admin"];
                    if (!(datos.Lector["nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil  = (string)datos.Lector["urlImagenPerfil"];
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int insertarNuevo(Usuario nuevo)
        {
   
            nuevo.Email = NormalizarEmail(nuevo.Email);

            // Validamos duplicados
            if (ExisteUsuario(nuevo.Email))
                throw new Exception("Ya existe un usuario con ese email.");



            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("insertarNuevo");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);
                return datos.ejecutarAccionScalar();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

            public void actualizar(Usuario user)
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("update USERS set urlImagenPerfil = @imagen, nombre = @nombre, apellido = @apellido where Id = @id");
                    //datos.setearParametro("@imagen", user.UrlImagenPerfil != null ? user.ImagenPerfil : (object)DBNull.Value);
                    datos.setearParametro("@imagen", (object)user.UrlImagenPerfil ?? DBNull.Value);
                    datos.setearParametro("@nombre", user.Nombre);
                    datos.setearParametro("@apellido", user.Apellido);
                    datos.setearParametro("id", user.Id);
                    datos.ejecutarAccion();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }

        private static string NormalizarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return email;
            return email.Trim().ToLowerInvariant();
            // Trim() quita espacios al inicio y al final
            // ToLowerInvariant() convierte todo a minúsculas
        }

        public bool ExisteUsuario(string email)
        {
            string normalizado = NormalizarEmail(email);

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM USERS WHERE LOWER(email) = @email");
                datos.setearParametro("@email", normalizado);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = Convert.ToInt32(datos.Lector[0]);
                    return count > 0;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

       
        

    }
   
}

