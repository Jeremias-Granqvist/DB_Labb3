using DB_Labb3.Repositories;
using DB_Labb3.Viewmodel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Model
{
    public class Note : BaseClass
    {

        public ObjectId Id { get; set; }
        public string Content { get; set; }
        public Category? NoteCategory { get; set; }

    }
}
