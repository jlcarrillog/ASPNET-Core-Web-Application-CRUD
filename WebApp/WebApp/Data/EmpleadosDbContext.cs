using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Controllers
{
    internal class EmpleadosDbContext
    {
        private readonly IConfiguration _config;
        public EmpleadosDbContext(IConfiguration config)
        {
            _config = config;
        }

        internal List<Empleado> ToList()
        {
            List<Empleado> model = new List<Empleado>();
            SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            SqlCommand cmd = new SqlCommand("SELECT [EmpleadoID], [Nombre], [Edad] FROM [Empleados]", con);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    model.Add(new Empleado
                    {
                        EmpleadoID = (Guid)dr["EmpleadoID"],
                        Nombre = (string)dr["Nombre"],
                        Edad = dr["Edad"].Equals(DBNull.Value) ? null : (int?)dr["Edad"]
                    });
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        internal Empleado Find(Guid? id)
        {
            Empleado model = new Empleado();
            SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            SqlCommand cmd = new SqlCommand("SELECT [EmpleadoID], [Nombre], [Edad] FROM [Empleados] WHERE [EmpleadoID] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    model.EmpleadoID = (Guid)dr["EmpleadoID"];
                    model.Nombre = (string)dr["Nombre"];
                    model.Edad = dr["Edad"].Equals(DBNull.Value) ? null : (int?)dr["Edad"];
                }
                return model;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        internal void Add(Empleado model)
        {
            SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Empleados] ([EmpleadoID], [Nombre], [Edad]) VALUES (@EmpleadoID, @Nombre, @Edad);", con);
            cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = model.EmpleadoID;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = model.Nombre;
            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = (object)model.Edad ?? DBNull.Value;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        internal void Update(Empleado model)
        {
            SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            SqlCommand cmd = new SqlCommand(@"UPDATE [Empleados] SET [Nombre] = @Nombre, [Edad] = @Edad WHERE [EmpleadoID] = @EmpleadoID;", con);
            cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = model.EmpleadoID;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = model.Nombre;
            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = (object)model.Edad ?? DBNull.Value;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        internal void Remove(Guid id)
        {
            SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            SqlCommand cmd = new SqlCommand(@"DELETE FROM [Empleados] WHERE [EmpleadoID] = @EmpleadoID;", con);
            cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}