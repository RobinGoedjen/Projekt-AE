using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;
using Newtonsoft.Json;
using System.IO;

namespace Raycasting
{
    class Program
    {
        static void Main(string[] args)
        {
            String mapPath = Directory.GetCurrentDirectory() + @"\Fallback.json"; //DIESE DATEI MUSS BEIM STARTEN OHNE LAUNCHER EXISTIEREN
            if (args.Length == 1)
                mapPath = args[0];
            if (!File.Exists(mapPath))
            {
                Console.WriteLine("Couldnt find map: " + mapPath);
                Console.ReadKey();
                return;
            }
            //22,12 old start pos 66 old FOV
            var player = new Player(new Vector2(5, 3), new Vector2(-1, 0), new Vector2(0, 0.95f));
            var map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(mapPath));
            using (Game game = new Game(player, map))
            {
                game.Run(60f);
            }
        }
    }
}
