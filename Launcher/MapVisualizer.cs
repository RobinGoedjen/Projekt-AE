using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;
using System.Drawing;

namespace MapEditor
{
    class MapVisualizer : IDisposable
    {
        private Map map;
        public readonly int scaleFactor;
        public Bitmap currentMapImage;
        private Graphics graphBase;

        public MapVisualizer(Size drawSize,Map map)
        {
            this.map = map;
            int scaleWdith = (int)(drawSize.Width / map.width);
            int scaleHeight = (int)(drawSize.Height / map.height);
            scaleFactor = scaleWdith < scaleHeight ? scaleWdith : scaleHeight;
            currentMapImage = drawColoredFromScratch();
            redrawNonTiles();
        }

        public Bitmap drawColoredFromScratch()
        {
            Bitmap bm = new Bitmap((int)map.width * scaleFactor, (int)map.height * scaleFactor);
            graphBase = Graphics.FromImage(bm);
            graphBase.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphBase.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, bm.Width, bm.Height));

            //Draw grid
            for (int i = 1; i < map.height; i++)
            {
                graphBase.DrawLine(Pens.Black, 0, i * scaleFactor, bm.Width, i * scaleFactor);
            }
            for (int i = 1; i < map.width; i++)
            {
                graphBase.DrawLine(Pens.Black, i * scaleFactor, 0, i * scaleFactor, bm.Height);
            }

            //Color Grid
            SolidBrush b = new SolidBrush(Color.Black);
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    sbyte tileID = map.worldMap[i][j];
                    b.Color = Map.getColorFromTileID(tileID);
                    colorCoordinate(new Point(j, i),b , true);
                }
            }

            return bm;
        }

        public bool colorCoordinate(Point coords, SolidBrush brush, bool force = false)
        {
            if (coords.X >= map.width || coords.Y >= map.height || coords.X < 0|| coords.Y < 0)
            {
                return false;
            }
            if (!force && brush.Color == Color.White && (coords.X == map.width - 1 || coords.X == 0 || coords.Y == 0 || coords.Y == map.height - 1))
                return false;
            graphBase.FillRectangle(brush, new Rectangle(coords.X*scaleFactor + 1, coords.Y*scaleFactor + 1, scaleFactor - 2, scaleFactor- 2));
            return true;
        }

        private void drawPlayer()
        {
            int startX = (int)(map.playerStartPosition.X * scaleFactor - scaleFactor * 0.2f);
            int startY = (int)(map.playerStartPosition.Y * scaleFactor - scaleFactor * 0.2f);
            graphBase.FillEllipse(new SolidBrush(Color.Black), new Rectangle(startX, startY, (int)(scaleFactor * 0.4f), (int)(scaleFactor * 0.4f)));
        }

        private void drawPlayerOrientation()
        {
            int startX = (int)(map.playerStartPosition.X * scaleFactor);
            int startY = (int)(map.playerStartPosition.Y * scaleFactor);
            double endX = scaleFactor * 0.5f;
            double endY = 0;
            double radiant = (-map.playerStartOrientation + 90) * Math.PI / 180;
            double s = Math.Sin(radiant);
            double c = Math.Cos(radiant);
            double newEndX = endX * c - endY * s;
            double newEndY = endX * s + endY * c;
            var p = new Pen(new SolidBrush(Color.Red), scaleFactor * 0.05f);
            graphBase.DrawLine(p, startX, startY, (int)(newEndX + startX), (int)(newEndY + startY));
        }

        private void drawSprites()
        {
            foreach (var sprite in map.sprites)
            {
                int startX = (int)(sprite.position.X * scaleFactor - scaleFactor * 0.2f);
                int startY = (int)(sprite.position.Y * scaleFactor - scaleFactor * 0.2f);
                graphBase.FillEllipse(new SolidBrush(getColorForSpirte(sprite.name)), new Rectangle(startX, startY, (int)(scaleFactor * 0.4f), (int)(scaleFactor * 0.4f)));
            }
        }

        public static Color getColorForSpirte(SpriteName name)
        {
            switch (name)
            {
                case SpriteName.Barrel:
                    return Color.SaddleBrown;
                case SpriteName.Pillar:
                    return Color.DarkSlateGray;
                case SpriteName.Portal:
                    return Color.DarkViolet;
                case SpriteName.Coin:
                    return Color.Gold;
                default:
                    return Color.Pink;
            }
        }

        public void redrawNonTiles()
        {
            drawPlayer();
            drawPlayerOrientation();
            drawSprites();
        }

        public void Dispose()
        {
            graphBase.Dispose();
            currentMapImage.Dispose();
        }
    }
}
