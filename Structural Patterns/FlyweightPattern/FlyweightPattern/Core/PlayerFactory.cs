using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPattern.Core
{
    internal class PlayerFactory
    {
        private Dictionary<string, Player> players = new();

        public Player GetPlayer(string role)
        {

            if (!players.ContainsKey(role))
            {
                Player player;
                if (role == "Terrorist")
                    player = new Terrorist();
                else
                    player = new CounterTerrorist();

                players[role] = player;
                Console.WriteLine($"Creating new {role} player.");
            }

            return players[role];
        }
    }
}
