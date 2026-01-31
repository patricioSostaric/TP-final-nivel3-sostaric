using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class ArticuloNegocio

    {
        private string cadenaConexion = ConfigurationManager.AppSettings["cadenaConexion"];
        // Listar todos o uno por Id
        public List<Articulo> Listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT A.Id, Codigo, Nombre, A.Descripcion, A.IdMarca, M.Descripcion TipoMarca, " +
                                      "A.IdCategoria, C.Descripcion TipoCategoria, ImagenUrl, A.Precio " +
                                      "FROM ARTICULOS A " +
                                      "INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria " +
                                      "INNER JOIN MARCAS M ON M.Id = A.IdMarca";

                if (!string.IsNullOrEmpty(id))
                {
                    comando.CommandText += " WHERE A.Id = @id";
                    comando.Parameters.AddWithValue("@id", int.Parse(id));
                }

                comando.Connection = conexion;
                conexion.Open();
                SqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    if (!(lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)lector["Descripcion"];
                    aux.TipoMarca = new Marca { Id = (int)lector["IdMarca"], Descripcion = (string)lector["TipoMarca"] };
                    aux.TipoCategoria = new Categoria { Id = (int)lector["IdCategoria"], Descripcion = (string)lector["TipoCategoria"] };
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Precio = (decimal)lector["Precio"];

                    lista.Add(aux);
                }
            }
            return lista;
        }

        // Buscar uno solo por Id
        public Articulo BuscarPorId(int id)
        {
            var lista = Listar(id.ToString());
            return lista.Count > 0 ? lista[0] : null;
        }

        // Alta
        public void Agregar(Articulo nuevo)
        {
            if (ExisteArticulo(nuevo.Codigo, nuevo.Nombre))
                throw new Exception("El artículo ya existe en el catálogo.");

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) " +
                                      "VALUES (@codigo, @nombre, @desc, @idMarca, @idCategoria, @imagenUrl, @precio)";
                comando.Parameters.AddWithValue("@codigo", nuevo.Codigo);
                comando.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                comando.Parameters.AddWithValue("@desc", nuevo.Descripcion);
                comando.Parameters.AddWithValue("@idMarca", nuevo.TipoMarca.Id);
                comando.Parameters.AddWithValue("@idCategoria", nuevo.TipoCategoria.Id);
                comando.Parameters.AddWithValue("@imagenUrl", nuevo.ImagenUrl);
                comando.Parameters.AddWithValue("@precio", nuevo.Precio);

                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        
        public void Modificar(Articulo art)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "UPDATE ARTICULOS SET Codigo=@codigo, Nombre=@nombre, Descripcion=@desc, " +
                                      "IdMarca=@idMarca, IdCategoria=@idCategoria, ImagenUrl=@img, Precio=@precio " +
                                      "WHERE Id=@id";
                comando.Parameters.AddWithValue("@codigo", art.Codigo);
                comando.Parameters.AddWithValue("@nombre", art.Nombre);
                comando.Parameters.AddWithValue("@desc", art.Descripcion);
                comando.Parameters.AddWithValue("@idMarca", art.TipoMarca.Id);
                comando.Parameters.AddWithValue("@idCategoria", art.TipoCategoria.Id);
                comando.Parameters.AddWithValue("@img", art.ImagenUrl);
                comando.Parameters.AddWithValue("@precio", art.Precio);
                comando.Parameters.AddWithValue("@id", art.Id);

                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // Baja
        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "DELETE FROM ARTICULOS WHERE Id=@id";
                comando.Parameters.AddWithValue("@id", id);

                comando.Connection = conexion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        
        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand())
            {
                string consulta = "SELECT A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion TipoMarca, " +
                                  "C.Descripcion TipoCategoria, ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria " +
                                  "FROM ARTICULOS A " +
                                  "INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria " +
                                  "INNER JOIN MARCAS M ON M.Id = A.IdMarca WHERE ";

              
                if (campo == "Codigo" || campo == "Nombre")
                {
                    consulta += "A." + campo + " LIKE @filtro";
                    switch (criterio)
                    {
                        case "Comienza con":
                            comando.Parameters.AddWithValue("@filtro", filtro + "%");
                            break;
                        case "Termina con":
                            comando.Parameters.AddWithValue("@filtro", "%" + filtro);
                            break;
                        default:
                            comando.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                            break;
                    }
                }
               
                else if (campo == "Precio")
                {
                    if (!decimal.TryParse(filtro, out decimal precioFiltro))
                        throw new FormatException("El filtro ingresado no es un número válido.");

                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > @filtro";
                            break;
                        case "Menor a":
                            consulta += "A.Precio < @filtro";
                            break;
                        default:
                            consulta += "A.Precio = @filtro";
                            break;
                    }

                    comando.Parameters.Add("@filtro", SqlDbType.Decimal).Value = precioFiltro;
                }
                
                else if (campo == "Marca")
                {
                    consulta += "M.Descripcion LIKE @filtro";
                    comando.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }
               
                else if (campo == "Categoria")
                {
                    consulta += "C.Descripcion LIKE @filtro";
                    comando.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }

                comando.CommandText = consulta;
                comando.Connection = conexion;
                conexion.Open();

                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)lector["Id"],
                        Codigo = (string)lector["Codigo"],
                        Nombre = (string)lector["Nombre"],
                        Descripcion = lector["Descripcion"] is DBNull ? null : (string)lector["Descripcion"],
                        TipoMarca = new Marca { Id = (int)lector["IdMarca"], Descripcion = (string)lector["TipoMarca"] },
                        TipoCategoria = new Categoria { Id = (int)lector["IdCategoria"], Descripcion = (string)lector["TipoCategoria"] },
                        ImagenUrl = lector["ImagenUrl"] is DBNull ? null : (string)lector["ImagenUrl"],
                        Precio = (decimal)lector["Precio"]
                    };

                    lista.Add(aux);
                }
            }

            return lista;





        }

        // Validación
        public bool ExisteArticulo(string codigo, string nombre)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT COUNT(*) FROM ARTICULOS WHERE Codigo=@codigo OR Nombre=@nombre";
                comando.Parameters.AddWithValue("@codigo", codigo);
                comando.Parameters.AddWithValue("@nombre", nombre);

                comando.Connection = conexion;
                conexion.Open();
                int count = (int)comando.ExecuteScalar();
                return count > 0;
            }
        }


    }
}
