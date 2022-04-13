using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw9
{
    // Model Class
    internal class Player
    {
        public int Id { get; set; }
        public string Team { get; set; }
        public string Name { get; set; }
        public int Yards { get; set; }
        public int Touchdowns { get; set; }
        public int Interceptions { get; set; }
        public Player()
        {
            Id = 0;
            Team = "A team";
            Name = "Quarterback";
            Yards = 0;
            Touchdowns = 0; 
            Interceptions = 0;
        }
        public Player(int id, string team, string name, int yards, int tds, int ints)
        {
            Id = id;
            Team = team;
            Name = name;
            Yards = yards;
            Touchdowns = tds;
            Interceptions = ints;
        }

        public override string ToString()
        {
            return String.Format("PlayerId = {0} Team = {1} Name = {2} Passing Yards = {3} Touchdowns = {4} Interceptions = {5}",
                Id, Team, Name, Yards, Touchdowns, Interceptions);
        }
    }
}
