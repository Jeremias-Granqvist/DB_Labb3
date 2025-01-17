using DB_Labb3.Repositories;
using DB_Labb3.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Model
{
    public class ToDoManager : BaseClass
    {
        public ObservableCollection<ToDo> ToDoItems { get; private set; }
        private readonly IToDoRepository _toDoRepository;

        public ToDoManager(IToDoRepository toDoRepository)
        {
            ToDoItems = new ObservableCollection<ToDo>();
            _toDoRepository = toDoRepository;
            ToDoItems.CollectionChanged += (sender, args) =>
            {
                foreach (var item in ToDoItems)
                {
                    item.PropertyChanged += OnToDoPropertyChanged;
                }
            };
        }
    
        private void OnToDoPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var toDo = sender as ToDo;
            if (toDo != null && args.PropertyName == nameof(ToDo.IsCompleted) && toDo.IsCompleted)
            {
                
                ToDoItems.Remove(toDo);
                _toDoRepository.RemoveToDoByIdAsync(toDo.Id);
            }
        }
    }
}
