using Microsoft.AspNetCore.Http;
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
        private readonly string connectionString;
        public EmpleadosDbContext(IConfiguration config) { connectionString = config["ConnectionStrings:DefaultConnection"]; }

        internal List<Empleado> ToList()
        {
            var data = new List<Empleado>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT [EmpleadoID], [Nombre], [Direccion], [Edad], [Foto] FROM [Empleados]", con);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(Mapper.Map<Empleado>(dr));
                }
                return data;
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
            var data = new Empleado();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT [EmpleadoID], [Nombre], [Direccion], [Edad], [Foto] FROM [Empleados] WHERE [EmpleadoID] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data = Mapper.Map<Empleado>(dr);
                }
                return data;
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
        internal void Add(Empleado data)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Empleados] ([EmpleadoID], [Nombre], [Direccion], [Edad], [Foto]) VALUES (@EmpleadoID, @Nombre, @Direccion, @Edad, @Foto);", con);
            cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = data.EmpleadoID;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = data.Nombre;
            cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = (object)data.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = (object)data.Edad ?? DBNull.Value;
            cmd.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = (object)data.Foto ?? DBNull.Value;

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
        internal void Update(Guid id, Empleado data)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(@"UPDATE [Empleados] SET [EmpleadoID] = @EmpleadoID, [Nombre] = @Nombre, [Direccion] = @Direccion, [Edad] = @Edad, [Foto] = @Foto WHERE [EmpleadoID] = @ID;", con);
            cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;
            cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = data.EmpleadoID;
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = data.Nombre;
            cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = (object)data.Direccion ?? DBNull.Value;
            cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = (object)data.Edad ?? DBNull.Value;
            cmd.Parameters.Add("@Foto", SqlDbType.VarBinary).Value = (object)data.Foto ?? DBNull.Value;

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
            SqlConnection con = new SqlConnection(connectionString);
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