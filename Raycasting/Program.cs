using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player(new Vector2(22, 12), new Vector2(-1, 0), new Vector2(0, 0.66f));
            var map = new Map("");
            using (Game game = new Game(player, map))
            {
                game.Run(60f);
            }
        }
    }
}
