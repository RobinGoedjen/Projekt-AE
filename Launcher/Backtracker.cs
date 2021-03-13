using MapLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class Backtracker
    {
        Map map;
        uint mapDimX;
        uint mapDimY;
        Random rand = new Random();
        List<PointF> deadEnds = new List<PointF>();

        public Backtracker(uint mapDimX, uint mapDimY)
        {
            this.mapDimX = mapDimX;
            this.mapDimY = mapDimY;
        }

        public Map getRandomMap()
        {
            map = new Map(mapDimX, mapDimY);

            for (int i = 0; i < mapDimY; i++)
            {
                map.worldMap.Add(new List<WallKind>());
                for (int j = 0; j < mapDimX; j++)
                {
                    map.worldMap[i].Add(WallKind.RedWall);
                }
            }
            recursiveBacktracker(1, 1);

            placeSprites();

            return map;
        }

        private bool recursiveBacktracker(int x, int y)
        {
            if (pointIsValid(x, y))
            {
                map.worldMap[x][y] = 0;
                Point[] neighbours = getValidNeighbours(x, y);

                if (neighbours == null || neighbours.Length == 0)
                {
                    return false;
                }

                Point point = neighbours[rand.Next(neighbours.Length)];

                if (x < point.X) { map.worldMap[x + 1][y] = 0; }
                if (x > point.X) { map.worldMap[x - 1][y] = 0; }
                if (y < point.Y) { map.worldMap[x][y + 1] = 0; }
                if (y > point.Y) { map.worldMap[x][y - 1] = 0; }

                if (!recursiveBacktracker(point.X, point.Y))
                {
                    return recursiveBacktracker(x, y);
                }
            }
            return false;
        }

        private bool pointIsValid(int x, int y)
        {
            if (x > 0 && y > 0 && x < mapDimX && y < mapDimY)
            {
                return true;
            }
            return false;
        }

        private Point[] getValidNeighbours(int x, int y)
        {
            List<Point> resultList = new List<Point>();

            if (pointIsValid(x + 2, y) && map.worldMap[x + 2][y] != 0)
            {
                resultList.Add(new Point(x + 2, y));
            }
            if (pointIsValid(x, y + 2) && map.worldMap[x][y + 2] != 0)
            {
                resultList.Add(new Point(x, y + 2));
            }
            if (pointIsValid(x - 2, y) && map.worldMap[x - 2][y] != 0)
            {
                resultList.Add(new Point(x - 2, y));
            }
            if (pointIsValid(x, y - 2) && map.worldMap[x][y - 2] != 0)
            {
                resultList.Add(new Point(x, y - 2));
            }
            return resultList.ToArray();
        }

        private void placeSprites()
        {
            for (int i = 0; i < mapDimY; i++)
            {
                for (int j = 0; j < mapDimX; j++)
                {
                    if(i == 1 && j == 1) { continue; }
                    if (map.worldMap[i][j] == WallKind.None)
                    { 
                        if (isDeadEnd(i,j)) { deadEnds.Add(new PointF(j + 0.5f, i + 0.5f)); }     
                        if (rand.Next(10) == 1) 
                        {
                            spriteData coin = new spriteData();
                            coin.name = SpriteName.Coin;
                            coin.position = new PointF(j + 0.5f, i + 0.5f);
                            map.sprites.Add(coin);
                        }
                    }
                }
            }

            spriteData portal = new spriteData();
            portal.name = SpriteName.Portal;
            portal.position = deadEnds.First();
            map.sprites.Add(portal);
        }

        private bool isDeadEnd(int x, int y)
        {
            int walls = 0;

            if (map.worldMap[x + 1][y] != 0)
            {
                walls++;
            }
            if (map.worldMap[x][y + 1] != 0)
            {
                walls++;
            }
            if (map.worldMap[x - 1][y] != 0)
            {
                walls++;
            }
            if (map.worldMap[x][y - 1] != 0)
            {
                walls++;
            }

            return walls == 3;
        }
    }
}
