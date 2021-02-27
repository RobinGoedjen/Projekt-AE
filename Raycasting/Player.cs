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
        public ushort collectedCoins = 0;
        public const float moveSpeed = 0.08f;
        public const float rotSpeed = 0.03f;

        public Player(Vector2 startPosition, ushort startDirectionDegree)
        {
            position = startPosition;
            double radiance = startDirectionDegree * Math.PI / 180d;
            direction = new Vector2((float)Math.Cos(radiance), (float)Math.Sin(radiance));
            direction.Normalize();
            plane = new Vector2(direction.Y, -direction.X) * 0.95f; //FOV
            
        }

        public void rotate(float rotSpeed)
        {
            float newDirX = (float)(direction.X * Math.Cos(rotSpeed) - direction.Y * Math.Sin(rotSpeed));
            float newDirY = (float)(direction.X * Math.Sin(rotSpeed) + direction.Y * Math.Cos(rotSpeed));
            direction = new Vector2(newDirX, newDirY);

            float newPlaneX = (float)(plane.X * Math.Cos(rotSpeed) - plane.Y * Math.Sin(rotSpeed));
            float newPlaneY = (float)(plane.X * Math.Sin(rotSpeed) + plane.Y * Math.Cos(rotSpeed));
            plane = new Vector2(newPlaneX, newPlaneY);
        }
    }
}
