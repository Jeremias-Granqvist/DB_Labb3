using DB_Labb3.Model;
using MongoDB.Bson;
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
        Task<ToDo> AddToDoAsync(ToDo toDo);
        Task<ToDo> UpdateToDoByIdAsync(ToDo toDo);
        Task<ToDo> RemoveToDoByIdAsync(ObjectId id);


        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(Category category);
        Task<Category> RemoveCategoryByIdAsync(ObjectId id);


        Task<List<Note>> GetAllNotesAsync();
        Task<Note> AddNoteAsync(Note note);
        Task<Note> UpdateNoteByIdAsync(Note note);
        Task<Note> RemoveNoteByIdAsync(ObjectId id);
    }
}
