using DB_Labb3.Viewmodel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Labb3.Model
{
    public class Category : BaseClass
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
