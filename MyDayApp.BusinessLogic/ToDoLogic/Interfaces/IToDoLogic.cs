using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.ToDoLogic.Interfaces
{
    public interface IToDoLogic
    {
        Task<ToDo> CreateToDo(ToDo model);
        Task<IEnumerable<ToDo>> GetAllToDoList(ToDo model);
        Task<IEnumerable<ToDo>> GetAllToDoListAsUser(ToDo model);
        Task<ToDo> DeleteToDo(ToDo model);
        Task<ToDo> EditToDo(ToDo model);
    }
}