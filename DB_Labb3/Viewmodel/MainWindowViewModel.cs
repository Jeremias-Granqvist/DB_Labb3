using DB_Labb3.Model;
using DB_Labb3.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DB_Labb3.Repositories;
using System.Collections.ObjectModel;

namespace DB_Labb3.Viewmodel
{
    public class MainWindowViewModel : BaseClass
    {

        private readonly IToDoRepository _toDoRepository;
        
        private ObservableCollection<ToDo> _toDoItems;
        public ObservableCollection<ToDo> ToDoItems
        {
            get { return _toDoItems; }
            set { _toDoItems = value; }
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }



        public MainWindowViewModel(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
            _toDoItems = new ObservableCollection<ToDo>();
            _notes = new ObservableCollection<Note>();
            _categories = new ObservableCollection<Category>();
            LoadDataAsync();

            SaveCategoryCommand = new DelegateCommand(SaveCategoryPress);
            SaveToDoNoteCommand = new DelegateCommand(SaveToDoNotePress);
        }

        //delegatecommands
        public ICommand SaveCategoryCommand { get; }
        public ICommand SaveToDoNoteCommand { get; }

        //load data
        public async Task LoadDataAsync()
        {
            await LoadToDoAsync();
            await LoadCategoriesAsync();
            await LoadNotesAsync();
        }
        public async Task LoadToDoAsync()
        {
            var items = await _toDoRepository.GetAllToDosAsync();
            ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoItems.Add(item);
            }
        }
        public async Task LoadCategoriesAsync()
        {
            var categories = await _toDoRepository.GetAllCategoriesAsync();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }
        public async Task LoadNotesAsync()
        {
            var notes = await _toDoRepository.GetAllNotesAsync();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        //Add to lists
        public async Task AddToDoAsync(ToDo newToDo)
        {
            var addedToDo = await _toDoRepository.AddToDoAsync(newToDo);
            ToDoItems.Add(addedToDo);
        }
        public async Task AddCategoryAsync(Category newCategory)
        {
            var addedCategory = await _toDoRepository.AddCategoryAsync(newCategory);
            Categories.Add(addedCategory);
        }
        public async Task AddNoteAsync(Note newNote)
        {
            var addedNote = await _toDoRepository.AddNoteAsync(newNote);
            Notes.Add(addedNote);
        }

        //save buttons
        private void SaveToDoNotePress(object obj)
        {
            
            // lägga in if-sats mellan radioknappar för att se om det är note eller todo
            //skapa item av det.
            // kör antingen addnoteasync eller addtodoasync
        }
        private void SaveCategoryPress(object obj)
        {
            //skapa kategori, och kör addcategoryasync
        }


        private Category _newCategory;

        public Category NewCategory
        {
            get { return _newCategory; }
            set 
            { 
                _newCategory = value;
                RaisePropertyChanged();
            }
        }






    }
}
