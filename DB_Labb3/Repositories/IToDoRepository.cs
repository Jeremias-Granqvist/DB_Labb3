using DB_Labb3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Repositories
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAllToDosAsync();
        Task<ToDo> AddToDoAsync(ToDo item);
        Task<ToDo> GetToDoByIdAsync(string id);

        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(string id);

        Task<List<Note>> GetAllNotesAsync();
        Task<Note> AddNoteAsync(Note note);

    }
}
