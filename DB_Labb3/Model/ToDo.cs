using DB_Labb3.Viewmodel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Model
{

    public class ToDo : BaseClass
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public Category? ToDoCategory { get; set; }
    }
}
