using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;
using System.Drawing;

namespace MapEditor
{
    class MapVisualizer
    {
        private Map loadedMap;
        public readonly int scaleFactor;
        public Bitmap currentMapImage { get; }
        private Graphics currGr;

        public MapVisualizer(Size drawSize,Map map)
        {
            loadedMap = map;
            int scaleWdith = (int)(drawSize.Width / map.width);
            int scaleHeight = (int)(drawSize.Height / map.height);
            scaleFactor = scaleWdith < scaleHeight ? scaleWdith : scaleHeight;
            currentMapImage = drawMapFromScratch();
        }

        public Bitmap drawMapFromScratch()
        {
            Bitmap bm = new Bitmap((int)loadedMap.width * scaleFactor, (int)loadedMap.height * scaleFactor);
            currGr = Graphics.FromImage(bm);
            currGr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            currGr.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, bm.Width, bm.Height));

            //Draw grid
            for (int i = 1; i < loadedMap.height; i++)
            {
                currGr.DrawLine(Pens.Black, 0, i * scaleFactor, bm.Width, i * scaleFactor);
            }
            for (int i = 1; i < loadedMap.width; i++)
            {
                currGr.DrawLine(Pens.Black, i * scaleFactor, 0, i * scaleFactor, bm.Height);
            }

            //Color Grid
            SolidBrush b = new SolidBrush(Color.Black);
            for (int i = 0; i < loadedMap.height; i++)
            {
                for (int j = 0; j < loadedMap.width; j++)
                {
                    sbyte tileID = loadedMap.worldMap[i][j];
                    b.Color = Map.getColorFromTileID(tileID);
                    colorCoordinate(new Point(j, i),b , true);
                }
            }

            return bm;
        }

        public bool colorCoordinate(Point coords, SolidBrush brush, bool force = false)
        {
            if (coords.X >= loadedMap.width || coords.Y >= loadedMap.height || coords.X < 0|| coords.Y < 0)
            {
                return false;
            }
            if (!force && brush.Color == Color.White && (coords.X == loadedMap.width - 1 || coords.X == 0 || coords.Y == 0 || coords.Y == loadedMap.height - 1))
                return false;
            currGr.FillRectangle(brush, new Rectangle(coords.X*scaleFactor + 1, coords.Y*scaleFactor + 1, scaleFactor - 2, scaleFactor- 2));
            return true;
        }

    }
}
