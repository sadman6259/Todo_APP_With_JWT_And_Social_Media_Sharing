using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        public TodoItemsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // GET: api/TodoItems
        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                string query = @"select * from dbo.TodoItems";
                DataTable table = new DataTable();
                string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
                SqlDataReader myReader;
                List<TodoItemDTO> dtoList = new List<TodoItemDTO>() { };
                int dtoID = 0;
                var dtoName = "";
                var dtoPercentage = 0;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            dtoID = (int)myReader.GetInt32(0);
                            dtoName = myReader.GetString(1);
                            dtoPercentage = (int)myReader.GetInt32(2);
                            dtoList.Add(ItemToDTO(new TodoItem { Id = dtoID, TodoName = dtoName ,ProgressPercentage = dtoPercentage }));
                        }

                        myReader.Close();
                        myCon.Close();
                    }
                }
                DataTable newerTable = MyExtensions.ToDataTable(dtoList);
                return new JsonResult(newerTable);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        // GET: api/TodoItems/{TodoName}
        [HttpGet("{searchQuery}")]
        public JsonResult GetItem(string searchQuery)
        {
            string query = @"select * from dbo.TodoItems where TodoName = '" + searchQuery + @"'";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
            SqlDataReader myReader;
            List<TodoItemDTO> dtoList = new List<TodoItemDTO>() { };
            int dtoID = 0;
            var dtoName = "";
            var dtoComp = "";
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();

                    while (myReader.Read())
                    {
                        dtoID = (int)myReader.GetInt32(0);
                        dtoName = myReader.GetString(1);
                        dtoComp = myReader.GetString(2);
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }
            try
            {
                dtoList.Add(ItemToDTO(new TodoItem { Id = dtoID, TodoName = dtoName }));
                DataTable newerTable = MyExtensions.ToDataTable(dtoList);
                return new JsonResult(newerTable);
            }
            catch (Exception)
            {
                return new JsonResult("Bad Request");
            }
        }

        //POST api/TodoItems
        [HttpPost]
        public JsonResult Post(TodoItem todo)
        {
            // string query = @"insert into dbo.TodoItems values ('" + todo.TodoName + @"', 'false', '" + todo.TodoSecret + @"')";
            string query = @"insert into dbo.TodoItems values ('" + todo.TodoName + @"','" + todo.ProgressPercentage + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
            SqlDataReader myReader;
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception e)
            {

            }


            return new JsonResult("Added Successfully");
        }

        //PUT api/TodoItems/{TodoName}
        [Route("TodoItemsName/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(TodoItem todo, int id)
        {
            try
            {
                string query = "";
                if (todo == null)
                {
                    return new JsonResult("Bad Request");
                }
                else
                {
                    query = @"update dbo.TodoItems 
                set TodoName = '" + todo.TodoName + @"'
                where Id = " + id + @"";
                }

                DataTable table = new DataTable();
                string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Updated Successfully");
            }
            catch(Exception e)
            {
                throw e;
            }
          
        }

        //PUT api/TodoItems/{TodoName}
        [Route("TodoItemsprogress/{id}")]
        [HttpPut("{id}")]
        public JsonResult PutProgress(TodoItem todo, int id)
        {
            string query = "";
            if (todo == null)
            {
                return new JsonResult("Bad Request");
            }
            else
            {
                query = @"update dbo.TodoItems 
                set ProgressPercentage = '" + todo.ProgressPercentage + @"'
                where Id = " + id + @"";
            }

            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        //DELETE api/TodoItems/{TodoName}
        [HttpDelete("{id}")]
        public JsonResult Delete(int Id)
        {

            string query = @"delete from dbo.TodoItems where Id = '" + Id + @"'";
            DataTable table = new DataTable();
            string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [Route("todoitemsdeleteall")]
        [HttpDelete]
        public JsonResult DeleteAll()
        {
            try
            {
                string query = @"delete from dbo.TodoItems";
                DataTable table = new DataTable();
                string sqlDataSource = Configuration.GetConnectionString("IdentityConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Deleted Successfully");
            }
            catch(Exception e)
            {
                throw e;
            }
          
        }


        //Returns TodoItemDTO Object 
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
        new TodoItemDTO
        {
            Id = todoItem.Id,
            TodoName = todoItem.TodoName,
            ProgressPercentage = todoItem.ProgressPercentage
        };
    }
}
