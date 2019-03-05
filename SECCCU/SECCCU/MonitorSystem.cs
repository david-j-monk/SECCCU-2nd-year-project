using System.Diagnostics;

namespace SECCCU
{
    public class MonitorSystem
    {
        public Database Database { get; set; }
        public MonitorSystem()
        {
            Database = new Database();
            if (Database.CreateConnection())
            {
                Debug.WriteLineIf(Database.InitializeDatabase(), "Database Initialized");
            }
        }

        public string[] IsUserCurrentlySignedIn(string cardNumber)
        {
            return Database.DidUserSwipeInCurrentLecture(cardNumber);
        }


    }
}