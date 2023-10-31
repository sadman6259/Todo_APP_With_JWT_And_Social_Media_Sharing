using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.Repository
{
    public interface ITodoItemRepository
    {
        TodoItemDTO GetItem(string searchQuery);
    }
    
    public class TodoItemRepository : ITodoItemRepository
    {
        public TodoItemRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public TodoItemDTO GetItem(string searchQuery)
        {
            TodoItemDTO todoItem = new TodoItemDTO();

            try
            {
                string query = @"select top(1) * from dbo.TodoItems where TodoName like '%" + searchQuery + @"%'";
                DataTable table = new DataTable();
                string sqlDataSource = Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
                SqlDataReader myReader;
                int dtoID = 0;
                var dtoName = "";
                var dtoComp = 0;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();

                        while (myReader.Read())
                        {
                            dtoID = (int)myReader.GetInt64(0);
                            dtoName = myReader.GetString(1);
                            dtoComp = (int)myReader.GetInt32(2);
                        }

                        myReader.Close();
                        myCon.Close();
                    }
                }
                if (!String.IsNullOrEmpty(dtoName))
                {
                    todoItem.Id = dtoID;
                    todoItem.TodoName = dtoName;
                    todoItem.ProgressPercentage = dtoComp;
                }
            }
            catch(Exception e)
            {

            }
            return todoItem;
        }
    }
}
