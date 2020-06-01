using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.Interface
{
    public interface IToDoLogic
    {
        IEnumerable<ToDoDto> GettAll();
        ToDoDtoUpsert(ToDoDto createToDoDto);
        ToDoDto GetId(int? id);
        ToDoDto Delete(int? id);
        IEnumerable<SelectListItem> GetDropDown();
    }
}