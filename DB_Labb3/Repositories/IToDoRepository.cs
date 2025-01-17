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
        Task<ToDo> AddToDoAsync(ToDo item);
        Task<ToDo> GetToDoByIdAsync(ObjectId id);
        Task<ToDo> RemoveToDoByIdAsync(ObjectId id);


        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(string id);
        Task<Category> RemoveCategoryByIdAsync(ObjectId id);


        Task<List<Note>> GetAllNotesAsync();
        Task<Note> AddNoteAsync(Note note);
        Task<Note> RemoveNoteByIdAsync(ObjectId id);
    }
}
