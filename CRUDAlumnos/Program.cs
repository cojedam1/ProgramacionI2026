using System;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using Npgsql;

namespace CrudAlumnos
{
    class Program
    {
        static string connectionString = "Host=localhost; Username=postgres; Password=postgresql; Database=escuela";

        static void Main(string[] args)
        {
            Menu();
        }
        static void Menu()
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("MANTENIMIENTO DE ALUMNOS");
                Console.WriteLine("1. Crear alumno");
                Console.WriteLine("2. Listar alumnos");
                Console.WriteLine("3. Actualizar alumno");
                Console.WriteLine("4. Eliminar alumno");
                Console.WriteLine("5. Salir");
                Console.WriteLine("Seleccione una opcion (1-5)");

                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        CrearAlumno();
                        break;
                    case "2":
                        ListarAlumnos();
                        break;
                    case "3":
                        ActualizarAlumno();
                        break;
                    case "4":
                        EliminarAlumno();
                        break;
                    case "5":
                        Console.WriteLine("Saliendo del Sistema");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opcion no Valida. Intente de nuevo");
                        break;
                }
            }
        }

        static void CrearAlumno()
        {
            Console.WriteLine("Registrar Nuevo Alumno");
            Console.Write("Nombre:");
            string? Nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string? Apellido = Console.ReadLine();
            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Correo: ");
            string? Correo = Console.ReadLine();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO alumnos (nombre, apellido, edad, correo) VALUES (@nombre, @apellido, @edad, @correo)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("Nombre", Nombre ?? "");
                        cmd.Parameters.AddWithValue("Apellido", Apellido ?? "");
                        cmd.Parameters.AddWithValue("Edad", edad);
                        cmd.Parameters.AddWithValue("Correo", Correo ?? "");

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alumno registrado con exito!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message}");
                }
            }
        }

        static void ListarAlumnos()
        {
            Console.WriteLine("Lista de Alumnos");
            Console.WriteLine(string.Format("{0,-5} | {1,-15} | {2,-15} | {3,-5} | {4,-25}", "ID", "Nombre", "Apellido", "Edad", "Correo"));
            Console.WriteLine(new string('-', 75));

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, nombre, apellido, edad, correo FROM alumnos ORDER BY id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No hay alumnos registrados");
                            return;
                        }
                        while (reader.Read())
                        {
                            Console.WriteLine(string.Format("{0,-5} | {1,-15} | {2,-15} | {3,-5} | {4,-25}",
                            reader["id"], reader["nombre"], reader["apellido"], reader["edad"], reader["correo"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
        }

        static void ActualizarAlumno()
        {
            Console.WriteLine("Actualizar Alumno");
            Console.WriteLine("Ingrese el ID del alumno a modificar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nuevo Nombre: ");
            string nombre = Console.ReadLine() ?? "0";
            Console.Write("Nuevo Apellido: ");
            string apellido = Console.ReadLine() ?? "0";
            Console.Write("Nueva Edad: ");
            int edad = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Nuevo Correo: ");
            string correo = Console.ReadLine() ?? "0";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE alumnos SET nombre = @nombre, apellido = @apellido, edad = @edad, correo = @correo WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("nombre", nombre);
                        cmd.Parameters.AddWithValue("apellido", apellido);
                        cmd.Parameters.AddWithValue("edad", edad);
                        cmd.Parameters.AddWithValue("correo", correo);

                        int filasafectadas = cmd.ExecuteNonQuery();
                        if (filasafectadas > 0)
                        {
                            Console.WriteLine("Alumno Actualizado con exito");
                        }
                        else
                        {
                            Console.WriteLine("No se encontro ningun alumno con ese ID");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void EliminarAlumno()
        {
            Console.WriteLine("Eliminar Alumno");
            Console.Write("Ingrese el ID del alumno que desea borrar: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write($"¿Está seguro que desea eliminar al ID {id}? (s/n): ");
            string confirmar = Console.ReadLine() ?? "0";

            if (confirmar.ToLower() != "s")
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM alumnos WHERE id = @id";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id", id);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                            Console.WriteLine("Alumno eliminado con éxito!");
                        else
                            Console.WriteLine("No se encontró ningún alumno con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
        }
    }
}