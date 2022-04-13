using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;

namespace Hw9
{
    internal class Program
    {
        public static List<Player> GetPlayersFromDatabase(DbCommand cmd)
        {
            List<Player> players = new List<Player>();
            cmd.CommandText = "select * from Quarterbacks";
            Player player;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    player = new Player(Convert.ToInt32(reader["PlayerId"]), Convert.ToString(reader["Team"]), Convert.ToString(reader["Name"]), 
                        Convert.ToInt32(reader["Passing Yards"]), Convert.ToInt32(reader["Touchdowns"]), Convert.ToInt32(reader["Interceptions"]));
                    players.Add(player);
                }
            }
            return players;
        }
        public static int PrintMenu()
        {
            int choice;
            Console.WriteLine("What would you like to do: ");
            Console.WriteLine("1. Show data.");
            Console.WriteLine("2. Add a player.");
            Console.WriteLine("3. Delete a player.");
            Console.WriteLine("4. Quit.");
            choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        public static int GetNextId(List<Player> players)
        {
            int maxId = 0;
            foreach (Player player in players)
            {
                if (player.Id > maxId)
                {
                    maxId = player.Id;
                }
            }
            return maxId + 1;
        }
        static void Main(string[] args)
        {
            int choice;
            string provider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            List<Player> players;
            choice = PrintMenu();
            


            // connecting the application to the database
            DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
            using (DbConnection conn = factory.CreateConnection())
            {
                if (conn == null)
                {
                    Console.WriteLine("Could not connect to database");
                    Console.ReadLine();
                    return;     // exiting program
                }
                conn.ConnectionString = connectionString;
                conn.Open();

                // now create the command piece
                DbCommand cmd = conn.CreateCommand();  // how to issue commands to the database

                players = GetPlayersFromDatabase(cmd);
                do
                {
                    if (choice == 1)
                    {
                        PlayerWriter.WritePlayersToScreen(players);
                        Console.WriteLine("\nWhat would you like to do: ");
                        Console.WriteLine("1. Show data.");
                        Console.WriteLine("2. Add a player.");
                        Console.WriteLine("3. Delete a player.");
                        Console.WriteLine("4. Quit.");
                        choice = Convert.ToInt32(Console.ReadLine());
                    }
                    if (choice == 2)       // add a player to list
                    {
                        Console.Write("Enter team name: ");
                        string team = Console.ReadLine();
                        Console.Write("Enter player name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter passing yards: ");
                        int yards = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter touchdowns: ");
                        int tds = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter interceptions: ");
                        int ints = Convert.ToInt32(Console.ReadLine());

                        int newId = GetNextId(players);

                        string query = String.Format("insert into Quarterbacks values ({0}, '{1}', '{2}', {3}, {4}, {5})",
                            newId, team, name, yards, tds, ints);
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        Console.ReadLine();
                        Console.WriteLine("Updated Results: ");
                        players = GetPlayersFromDatabase(cmd);
                        PlayerWriter.WritePlayersToScreen(players);
                        Console.WriteLine("\nWhat would you like to do: ");
                        Console.WriteLine("1. Show data.");
                        Console.WriteLine("2. Add a player.");
                        Console.WriteLine("3. Delete a player.");
                        Console.WriteLine("4. Quit.");
                        choice = Convert.ToInt32(Console.ReadLine());
                    }
                    if (choice == 3)        // delete a player from list
                    {
                        Console.Write("Enter the player id for player you wish to remove: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        cmd.CommandText = String.Format("delete from Quarterbacks where PlayerId = {0}", id);
                        cmd.ExecuteNonQuery();
                        Console.ReadLine();
                        Console.WriteLine("Updated Results: ");
                        players = GetPlayersFromDatabase(cmd);
                        PlayerWriter.WritePlayersToScreen(players);
                        Console.WriteLine("\nWhat would you like to do: ");
                        Console.WriteLine("1. Show data.");
                        Console.WriteLine("2. Add a player.");
                        Console.WriteLine("3. Delete a player.");
                        Console.WriteLine("4. Quit."); choice = Convert.ToInt32(Console.ReadLine());
                    }
                    
                } while (choice != 4);
            }
        }
    }
}
