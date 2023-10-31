using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Repository;

namespace WebAPI.Service
{
    public interface ITodoItemService
    {
        TodoItemDTO GetItem(string searchQuery);
    }
    public class TodoItemService: ITodoItemService
    {
        ITodoItemRepository todoItemRepository ;

        public TodoItemService(ITodoItemRepository _todoItemRepository)
        {
            todoItemRepository = _todoItemRepository;
        }
        public TodoItemDTO GetItem(string searchQuery)
        {
            TodoItemDTO todoItemDTO = todoItemRepository.GetItem(searchQuery) ;
            return todoItemDTO;
        }
    }
}
