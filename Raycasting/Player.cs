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

        public Player(Vector2 startPosition, Vector2 startDirection, Vector2 viewPlane)
        {
            position = startPosition;
            direction = startDirection;
            plane = viewPlane;
        }
    }
}
