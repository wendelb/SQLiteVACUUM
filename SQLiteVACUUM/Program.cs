using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace SQLiteVACUUM
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(string arg in args)
            {
                if (File.Exists(arg)) {
                    System.Console.Write("Cleaning: ");
                    System.Console.WriteLine(arg);
                    cleanDatabaseFile(arg);
                }
                else
                {
                    PrintError("Error: " + arg + " does not exist!");
                }
            }

#if DEBUG
            // In Debug-Mode, pause at this point.
            System.Console.ReadLine();
#endif
        }

        private static void PrintError(string message)
        {
            TextWriter writer = System.Console.Error;
            writer.WriteLine(message);
        }

        private static void cleanDatabaseFile(string FileName)
        {
            using (SQLiteConnection connection = new SQLiteConnection())
            {
                // Set the data source path
                connection.ConnectionString = "Data Source=" + FileName;
                connection.Open();

                // Create a command for execution
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    // Clear the database using the VACUUM command
                    // See: https://sqlite.org/lang_vacuum.html
                    command.CommandText = "VACUUM";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
