using System.Configuration;
using System.Data;
using System.ServiceProcess;
using System.Windows;
using DB_Labb3.Repositories;
using DB_Labb3.Viewmodel;
using MongoDB.Driver;

namespace DB_Labb3
{
    public partial class App : Application
    {
        public static IMongoClient MongoClient { get; private set; }
        public static IMongoDatabase MongoDatabase { get; private set; }
        public static IToDoRepository ToDoRepository { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var connectionString = "mongodb://localhost:27017/";
            var databaseName = "JeremiasGranqvist";
            try
            {
                MongoClient = new MongoClient(connectionString);
                MongoDatabase = MongoClient.GetDatabase(databaseName);
            }
            catch (MongoException ex)
            {
                MessageBox.Show($"Could not connect to MongoDB. Error: {ex.Message}", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            ToDoRepository = new ToDoRepository(MongoDatabase);

        }
    }

}
