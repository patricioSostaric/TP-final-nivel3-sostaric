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
            if (idUser <= 0)
                throw new ArgumentException("El ID de usuario debe ser mayor a cero.");

            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, 
                                              A.IdMarca, M.Descripcion TipoMarca, 
                                              A.IdCategoria, C.Descripcion TipoCategoria, 
                                              A.ImagenUrl, A.Precio 
                                       FROM FAVORITOS F 
                                       INNER JOIN ARTICULOS A ON F.IdArticulo = A.Id 
                                       INNER JOIN MARCAS M ON A.IdMarca = M.Id 
                                       INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id 
                                       WHERE F.IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        TipoMarca = new Marca
                        {
                            Id = (int)datos.Lector["IdMarca"],
                            Descripcion = (string)datos.Lector["TipoMarca"]
                        },
                        TipoCategoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Descripcion = (string)datos.Lector["TipoCategoria"]
                        },
                        Precio = (decimal)datos.Lector["Precio"]
                    };

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar favoritos del usuario", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }


        }





        public void insertarNuevoFavorito(ArticuloFavorito nuevo)
        {

            if (nuevo == null)
                throw new ArgumentNullException(nameof(nuevo));
            if (nuevo.IdUser <= 0 || nuevo.IdArticulo <= 0)
                throw new ArgumentException("Favorito inválido: IDs deben ser mayores a cero.");

            if (!ExisteFavorito(nuevo.IdArticulo, nuevo.IdUser))
            {
                AccesoDatos datos = new AccesoDatos();
                try
                {
                    datos.setearConsulta("INSERT INTO FAVORITOS (IdUser, IdArticulo) VALUES (@idUser, @idArticulo)");
                    datos.setearParametro("@idUser", nuevo.IdUser);
                    datos.setearParametro("@idArticulo", nuevo.IdArticulo);
                    datos.ejecutarAccion();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar favorito", ex);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }




        }
        public void eliminarFavorito(int idArticulo, int idUser)
        {
            if (idUser <= 0 || idArticulo <= 0)
                throw new ArgumentException("IDs inválidos: deben ser mayores a cero.");

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM FAVORITOS WHERE IdArticulo = @idArticulo AND IdUser = @idUser");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar favorito", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }



        }

        public bool ExisteFavorito(int idArticulo, int idUser)
        {
            if (idUser <= 0 || idArticulo <= 0)
                return false; // defensivo: no consultar si los IDs son inválidos

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
                throw new Exception("Error al verificar existencia de favorito", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }


        }



    }
}
