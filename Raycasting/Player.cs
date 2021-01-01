using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    class Player
    {
        public Vector2 position { get; set; }
        public Vector2 direction { get; set; }
        public Vector2 plane { get; set; }

        public Player(Vector2 startPosition, ushort startDirectionDegree)
        {
            position = startPosition;
            double radiance = startDirectionDegree * Math.PI / 180d;
            direction = new Vector2((float)Math.Cos(radiance), (float)Math.Sin(radiance));
            direction.Normalize();
            plane = new Vector2(direction.Y, -direction.X) * 0.95f;
            
        }
    }
}
