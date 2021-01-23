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
            if (args.Length >= 1)
                mapPath = args[0];
            if (!File.Exists(mapPath))
            {
                Console.WriteLine("Couldnt find map: " + mapPath);
                Console.ReadKey();
                return;
            }
            var useWallTextures = args.Length >= 2 ? args[1] == "true" : false;
            var map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(mapPath));
            var player = new Player(new Vector2(map.playerStartPosition.Y, map.playerStartPosition.X), map.playerStartOrientation);
            using (Game game = new Game(player, map))
            {
                game.UseWallTextures = useWallTextures;
                game.Run(60f);
            }
        }
    }
}
