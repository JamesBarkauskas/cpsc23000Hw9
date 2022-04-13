using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw9
{
    internal class PlayerWriter
    {
        public static void WritePlayersToScreen(List<Player> players)
        {
            foreach (Player player in players)
            {
                Console.WriteLine(player);
            }
        }
    }
}
