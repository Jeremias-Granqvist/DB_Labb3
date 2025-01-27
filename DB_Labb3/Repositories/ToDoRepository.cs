﻿using DB_Labb3.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ToDo> _toDoItems;
        private readonly IMongoCollection<Category> _categories;
        private readonly IMongoCollection<Note> _notes;

        public ToDoRepository(IMongoDatabase database)
        {
            _database = database;

            CheckIfDatabaseExists();

            _toDoItems = database.GetCollection<ToDo>("ToDos");
            _categories = database.GetCollection<Category>("Categories");
            _notes = database.GetCollection<Note>("Notes");

            PopulateDefaultData();

        }

        //create collections if they don't exist.
        private void CheckIfDatabaseExists()
        {
            var collectionNames = _database.ListCollectionNames().ToList();

            if (!collectionNames.Contains("ToDos"))
            {
                _database.CreateCollection("ToDos");
            }
            if (!collectionNames.Contains("Categories"))
            {
                _database.CreateCollection("Categories");
            }
            if (!collectionNames.Contains("Notes"))
            {
                _database.CreateCollection("Notes");
            }

        }

        //populate collections on first startup
        private void PopulateDefaultData()
        {
            var defaultCategories = new List<Category>();
            if (_categories.CountDocuments(Builders<Category>.Filter.Empty) == 0)
            {
                defaultCategories = new List<Category> {
                new Category { Name = " " },
                new Category { Name = "Shopping" }
                };

                _categories.InsertMany(defaultCategories);
            }
            if (_toDoItems.CountDocuments(Builders<ToDo>.Filter.Empty) == 0)
            {
                var defaultToDos = new List<ToDo>
                {
                    new ToDo {Title = "Buy Milk, Eggs, Bacon", IsCompleted = false, ToDoCategory = defaultCategories.FirstOrDefault()},
                    new ToDo {Title = "Pick up dry cleaning", IsCompleted = false, ToDoCategory = defaultCategories.FirstOrDefault()}
                };
                _toDoItems.InsertMany(defaultToDos);
            }
            if (_notes.CountDocuments(Builders<Note>.Filter.Empty) == 0)
            {
                var defaultNotes = new List<Note>
                {
                    new Note { Content = "Livingroom measurements 6x4m", NoteCategory = defaultCategories.FirstOrDefault() },
                    new Note {Content = "wall colour kitchen is #FFFF00", NoteCategory = defaultCategories.FirstOrDefault()}
                };
                _notes.InsertMany(defaultNotes);
            }
        }


        //ToDo collection
        public async Task<List<ToDo>> GetAllToDosAsync() 
        {
            return await _toDoItems.Find(item => true).ToListAsync();
        }
        public async Task<ToDo> AddToDoAsync(ToDo toDo)
        {
            await _toDoItems.InsertOneAsync(toDo);
            return toDo;
        }
        public async Task<ToDo> UpdateToDoByIdAsync(ToDo toDo)
        {
            var filter = Builders<ToDo>.Filter.Eq(item => item.Id, toDo.Id);
            var updateToDo = Builders<ToDo>.Update
                .Set(t => t.Title, toDo.Title)
                .Set(t => t.ToDoCategory, toDo.ToDoCategory);

            var editToDo = await _toDoItems.FindOneAndUpdateAsync<ToDo>(filter, updateToDo);
            return editToDo;
        }

        public async Task<ToDo> RemoveToDoByIdAsync(ObjectId id)
        {
            var filter = Builders<ToDo>.Filter.Eq(item => item.Id, id);
            var deletedToDo = await _toDoItems.FindOneAndDeleteAsync(filter);
            return deletedToDo;
        }

        //Category collection
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categories.Find(c => true).ToListAsync();
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _categories.InsertOneAsync(category);
            return category;
        }

        public async Task<Category> GetCategoryByIdAsync(Category category)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, category.Id);
            return await _categories.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Category> RemoveCategoryByIdAsync(ObjectId id)
        {
            var filter = Builders<Category>.Filter.Eq(item => item.Id, id);
            var deletedCategory = await _categories.FindOneAndDeleteAsync(filter);
            return deletedCategory;
        }

        //note collection
        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _notes.Find(note => true).ToListAsync();
        }
        public async Task<Note> AddNoteAsync(Note note)
        {
            await _notes.InsertOneAsync(note);
            return note;
        }
        public async Task<Note> RemoveNoteByIdAsync(ObjectId id)
        {
            var filter = Builders<Note>.Filter.Eq(item => item.Id, id);
            var deletedNote = await _notes.FindOneAndDeleteAsync(filter);
            return deletedNote;
        }

        public async Task<Note>UpdateNoteByIdAsync(Note note)
        {
            var filter = Builders<Note>.Filter.Eq(n => n.Id, note.Id);
            var updateNote = Builders<Note>.Update
                .Set(n => n.Content, note.Content)
                .Set(n => n.NoteCategory, note.NoteCategory);

            var editNote = await _notes.FindOneAndUpdateAsync<Note>(filter, updateNote);
            return editNote;
        }
    }
}