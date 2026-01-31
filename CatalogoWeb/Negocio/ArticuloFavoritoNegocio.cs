using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloFavoritoNegocio
    {
        public List<Articulo> ListarFavoritosPorUsuario(int idUser)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion TipoMarca, " +
                                     "A.IdCategoria, C.Descripcion TipoCategoria, A.ImagenUrl, A.Precio " +
                                     "FROM FAVORITOS F " +
                                     "INNER JOIN ARTICULOS A ON F.IdArticulo = A.Id " +
                                     "INNER JOIN MARCAS M ON A.IdMarca = M.Id " +
                                     "INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id " +
                                     "WHERE F.IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.TipoMarca = new Marca { Id = (int)datos.Lector["IdMarca"], Descripcion = (string)datos.Lector["TipoMarca"] };
                    aux.TipoCategoria = new Categoria { Id = (int)datos.Lector["IdCategoria"], Descripcion = (string)datos.Lector["TipoCategoria"] };
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        
        

        public void insertarNuevoFavorito(ArticuloFavorito nuevo)
        {
            if (!ExisteFavorito(nuevo.IdArticulo, nuevo.IdUser))
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO FAVORITOS (IdUser, IdArticulo)VALUES(@idUser, @idArticulo)");
                    datos.setearParametro("@idUser", nuevo.IdUser);
                    datos.setearParametro("@idArticulo", nuevo.IdArticulo);
                    datos.ejecutarAccion();
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
        public void eliminarFavorito(int idArticulo, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
               
                datos.setearConsulta("DELETE FROM FAVORITOS WHERE IdArticulo = @idArticulo AND IdUser = @idUser");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarAccion();
                datos.cerrarConexion();
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

        public bool ExisteFavorito(int idArticulo, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @idUser AND IdArticulo = @idArticulo");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    int cantidad = Convert.ToInt32(datos.Lector[0]);
                    return cantidad > 0;
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



    }
}
