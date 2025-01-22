using DB_Labb3.Viewmodel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Model
{

    public class ToDo : BaseClass
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set 
            {
                if (_isCompleted != value)
                {
                _isCompleted = value;
                RaisePropertyChanged();
                }
            }
        }


        public ToDo(string Title, bool isCompleted)
        {
            if (this.ToDoCategory == null)
            {

            }
        }
        public DateTime DueDate { get; set; }
        public Category? ToDoCategory { get; set; }
    }
}
