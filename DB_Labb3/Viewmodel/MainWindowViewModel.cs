using DB_Labb3.Command;
using DB_Labb3.DialogWindows;
using DB_Labb3.Interfaces;
using DB_Labb3.Model;
using DB_Labb3.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DB_Labb3.Viewmodel
{
    public class MainWindowViewModel : BaseClass, ICloseWindows
    {

        private readonly IToDoRepository _toDoRepository;
        public ToDoManager ToDoManager { get; private set; }


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
            ToDoManager = new ToDoManager(toDoRepository);
            _notes = new ObservableCollection<Note>();
            _categories = new ObservableCollection<Category>();
            LoadDataAsync();


            SaveCategoryCommand = new DelegateCommand(SaveCategoryPress);
            SaveToDoNoteCommand = new DelegateCommand(SaveToDoNotePress);
            EditNoteCommand = new DelegateCommand(EditNotePress);
            SaveNoteChangesCommand = new DelegateCommand(SaveNoteChanges);
            EditToDoCommand = new DelegateCommand(EditToDoPress);
            RemoveNoteCommand = new DelegateCommand(DeleteNotePress);
            RemoveToDoCommand = new DelegateCommand(DeleteToDoPress);
            CancelChangesCommand = new DelegateCommand(CancelChangesPress);
        }

        //delegatecommands
        public ICommand SaveCategoryCommand { get; }
        public ICommand SaveToDoNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand EditToDoCommand { get; }
        public ICommand RemoveNoteCommand { get; }
        public ICommand RemoveToDoCommand { get; }
        public ICommand SaveNoteChangesCommand { get; }
        public ICommand CancelChangesCommand { get; }


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
            ToDoManager.ToDoItems.Clear();
            foreach (var item in items)
            {
                ToDoManager.ToDoItems.Add(item);
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
            ToDoManager.ToDoItems.Add(addedToDo);
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

        //button presses
        private void SaveToDoNotePress(object obj)
        {
            if (ToDoIsChecked == false && NoteIsChecked == false)
            {
                string message = "Please choose if this is a Note or a ToDo";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.OK);
            }
            if (ToDoIsChecked == true && NoteIsChecked == false)
            {
                var newToDo = new ToDo { Title = Description, IsCompleted = false, ToDoCategory = SelectedCategory };
                _toDoRepository.AddToDoAsync(newToDo);
                LoadToDoAsync();
            }
            if (NoteIsChecked == true && ToDoIsChecked == false)
            {
                var newNote = new Note() { Content = Description, NoteCategory = SelectedCategory };
                _toDoRepository.AddNoteAsync(newNote);
                LoadNotesAsync();
            }
        }
        private void SaveCategoryPress(object obj)
        {
            var newCategory = new Category { Name = NewCategory };
            _toDoRepository.AddCategoryAsync(newCategory);
            LoadCategoriesAsync();
            NewCategory = string.Empty;
        }

        public void EditNotePress(object obj)
        {
            var editNoteWindow = new EditNoteDialogWindow(this);
            if (editNoteWindow.DataContext is ICloseWindows vm)
            {
                vm.Close = new Action(editNoteWindow.Close);
            }
            editNoteWindow.Show();
        }

        public void SaveNoteChanges(object obj)
        {
            _toDoRepository.GetNoteByIdAsync(SelectedNote);
            LoadNotesAsync();
            Close?.Invoke();
        }



        public void EditToDoPress(object obj)
        {

        }

        private void DeleteNotePress(object obj)
        {
            if (SelectedNote != null)
            {
                _toDoRepository.RemoveNoteByIdAsync(SelectedNote.Id);
                LoadNotesAsync();
            }
        }

        private void DeleteToDoPress(object obj)
        {
            if (SelectedToDo != null)
            {
                _toDoRepository.RemoveToDoByIdAsync(SelectedToDo.Id);
                LoadToDoAsync();
            }
        }




        private void CancelChangesPress(object obj)
        {
            Close?.Invoke();
        }

        // properties
        private Note _selectedNote;
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                RaisePropertyChanged();
            }
        }

        private ToDo _selectedToDo;
        public ToDo SelectedToDo
        {
            get { return _selectedToDo; }
            set
            {
                _selectedToDo = value;
                RaisePropertyChanged();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged();
            }
        }

        private string _newCategory;
        public string NewCategory
        {
            get { return _newCategory; }
            set
            {
                _newCategory = value;
                RaisePropertyChanged();
            }
        }

        private bool _ToDoIsChecked;
        public bool ToDoIsChecked
        {
            get { return _ToDoIsChecked; }
            set
            {
                _ToDoIsChecked = value;
                RaisePropertyChanged();
            }
        }

        private bool _noteIsChecked;
        public bool NoteIsChecked
        {
            get { return _noteIsChecked; }
            set
            {
                _noteIsChecked = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }


        private bool _completedToDo;
        public bool CompletedToDo
        {
            get { return _completedToDo; }
            set
            {
                _completedToDo = value;
                RaisePropertyChanged();
            }
        }

        //edit note properties

        private string _editNoteContent;

        public string EditNoteContent
        {
            get { return _editNoteContent; }
            set
            {
                _editNoteContent = value;
                RaisePropertyChanged();
            }
        }





        public Action Close { get; set; }
    }
}
