using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace MiApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
       private readonly string _connectionString = "Server=localhost,1433;Database=test;User Id=sa;Password=Molina0107**;";
       [Tags("listar")]
       [HttpGet]
        public ActionResult<IEnumerable<Creditos_Model>> GetALl()
        {
            var datos = new List<Creditos_Model>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT id_credito, cedula, nombre, valor_credito, plazo, valor_cuota, tasa_interes FROM test.dbo.creditos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            datos.Add(new Creditos_Model
                            {
                                id_credito = Convert.ToInt32(reader["id_credito"]),
                                cedula = Convert.ToDecimal(reader["cedula"]),
                                nombre = reader["nombre"].ToString(),
                                valor_credito = Convert.ToDecimal(reader["valor_credito"]),
                                valor_cuota = Convert.ToDecimal(reader["valor_cuota"]),
                                tasa_interes = Convert.ToDecimal(reader["tasa_interes"]),
                                plazo = Convert.ToInt32(reader["plazo"])
                            });
                        }
                    }
                }
            }

            return datos;
        }

        [Tags("crear")]
        [HttpPost]
        public IActionResult PostProducto(Creditos_Model credito)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO test.dbo.creditos (cedula, nombre, valor_credito, plazo, valor_cuota, tasa_interes) VALUES(@cedula, @nombre, @valor_credito, @plazo, @valor_cuota, @tasa_interes);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cedula", credito.cedula);
                    command.Parameters.AddWithValue("@nombre", credito.nombre);
                    command.Parameters.AddWithValue("@valor_credito", credito.valor_credito);
                    command.Parameters.AddWithValue("@plazo", credito.plazo);
                    command.Parameters.AddWithValue("@valor_cuota", credito.valor_cuota);
                    command.Parameters.AddWithValue("@tasa_interes", credito.tasa_interes);
                
                    command.ExecuteNonQuery();
                }
            }

            return Ok();            

        }

        [Tags("actulizar")]
        [HttpPut("{id}")]
        public IActionResult PutProducto(int id, Creditos_Model credito)
        {
            if (id != credito.id_credito)
            {
                return BadRequest();
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE test.dbo.creditos SET cedula=@cedula, nombre=@nombre, valor_credito=@valor_credito, plazo=@plazo, valor_cuota=@valor_cuota, tasa_interes=@tasa_interes WHERE id_credito=@id_credito;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@id_credito", credito.id_credito);
                    command.Parameters.AddWithValue("@cedula", credito.cedula);
                    command.Parameters.AddWithValue("@nombre", credito.nombre);
                    command.Parameters.AddWithValue("@valor_credito", credito.valor_credito);
                    command.Parameters.AddWithValue("@plazo", credito.plazo);
                    command.Parameters.AddWithValue("@valor_cuota", credito.valor_cuota);
                    command.Parameters.AddWithValue("@tasa_interes", credito.tasa_interes);

                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }

        [Tags("Eliminar")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id, Creditos_Model credito)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM test.dbo.creditos WHERE id_credito=@id_credito;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_credito", id);

                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }
    

  

    }
}