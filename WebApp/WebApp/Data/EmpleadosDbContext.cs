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
        List<Empleado> modelo = new List<Empleado>();
        SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
        SqlCommand cmd = new SqlCommand("SELECT [EmpleadoID], [Nombre], [Edad] FROM [Empleados]", con);

        try
        {
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                modelo.Add(new Empleado
                {
                    EmpleadoID = (Guid)dr["EmpleadoID"],
                    Nombre = (string)dr["Nombre"],
                    Edad = dr["Edad"].Equals(DBNull.Value) ? null : (int?)dr["Edad"]
                });
            }
            return modelo;
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
        Empleado modelo = new Empleado();
        SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
        SqlCommand cmd = new SqlCommand("SELECT [EmpleadoID], [Nombre], [Edad] FROM [Empleados] WHERE [EmpleadoID] = @id", con);
        cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

        try
        {
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                modelo.EmpleadoID = (Guid)dr["EmpleadoID"];
                modelo.Nombre = (string)dr["Nombre"];
                modelo.Edad = dr["Edad"].Equals(DBNull.Value) ? null : (int?)dr["Edad"];
            }
            return modelo;
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
    internal void Add(Empleado empleado)
    {
        List<Empleado> modelo = new List<Empleado>();
        SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
        SqlCommand cmd = new SqlCommand(@"INSERT INTO [Empleados] ([EmpleadoID], [Nombre], [Edad]) VALUES (@EmpleadoID, @Nombre, @Edad);", con);
        cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = empleado.EmpleadoID;
        cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = empleado.Nombre;
        cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = (object)empleado.Edad ?? DBNull.Value;

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
    internal void Update(Empleado empleado)
    {
        List<Empleado> modelo = new List<Empleado>();
        SqlConnection con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
        SqlCommand cmd = new SqlCommand(@"UPDATE [Empleados] SET [Nombre] = @Nombre, [Edad] = @Edad WHERE [EmpleadoID] = @EmpleadoID;", con);
        cmd.Parameters.Add("@EmpleadoID", SqlDbType.UniqueIdentifier).Value = empleado.EmpleadoID;
        cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = empleado.Nombre;
        cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = (object)empleado.Edad ?? DBNull.Value;

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

/*
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    class Persona
    {
        public string id { get; set; }
        public string nombre { get; set; }
    }

    public class HomeController : Controller
    {
        Uri uri = new Uri(ConfigurationManager.AppSettings["Uri"]);
        HttpClient client = new HttpClient()
        {
            DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["Token"]) }
        };
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // GET: Home/Get
        public ActionResult Get()
        {
            try
            {
                var response = GetHttpClient.GetRequest<Persona>(client, uri, "values");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
        // GET: Home/GetId
        public ActionResult GetId(string id)
        {
            try
            {
                var response = GetHttpClient.GetRequest<Persona>(client, uri, "values", id);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Home/Post
        public ActionResult Post()
        {
            var persona = new Persona { id = "0", nombre = "nombre" };
            try
            {
                var response = GetHttpClient.PostRequest<Persona>(client, uri, "values", persona);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Home/Put
        public ActionResult Put(string id)
        {
            var persona = new Persona { id = id, nombre = "nombre" + id };
            try
            {
                var response = GetHttpClient.PutRequest<Persona>(client, uri, "values", persona, id);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // GET: Home/Delete
        public ActionResult Delete(string id)
        {
            var persona = new Persona { id = id, nombre = "nombre" + id };
            try
            {
                var response = GetHttpClient.PostRequest<Persona>(client, uri, "values", persona);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}

==============================================
  
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebApp.Controllers
{
    public static class GetHttpClient
    {
        // PUT Request
        public static IList<T> GetRequest<T>(HttpClient httpClient, Uri uri, string requestUri)
        {
            IList<T> response = null;
            httpClient.BaseAddress = uri;
            var responseTask = httpClient.GetAsync(requestUri);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<T>>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
        // GET Request
        public static T GetRequest<T>(HttpClient httpClient, Uri uri, string requestUri, string value)
        {
            T response = Activator.CreateInstance<T>();
            httpClient.BaseAddress = uri;
            var responseTask = httpClient.GetAsync(requestUri + "/" + value);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
        // POST Request
        public static T PostRequest<T>(HttpClient httpClient, Uri uri, string requestUri, object data)
        {
            T response = Activator.CreateInstance<T>();
            httpClient.BaseAddress = uri;

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var responseTask = httpClient.PostAsync(requestUri, content);
            responseTask.Wait();
            var result = responseTask.Result;
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
        // PUT Request
        public static T PutRequest<T>(HttpClient httpClient, Uri uri, string requestUri, object data, string value)
        {
            T response = Activator.CreateInstance<T>();
            httpClient.BaseAddress = uri;

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var responseTask = httpClient.PutAsJsonAsync(requestUri + value, (T)data);
            responseTask.Wait();
            var result = responseTask.Result;
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
    }
}
     */
