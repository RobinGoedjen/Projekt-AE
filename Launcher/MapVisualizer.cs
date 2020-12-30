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

        public MapVisualizer(Map map, Size drawSize)
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
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gr.FillRectangle(Brushes.LightGray, new Rectangle(0, 0, bm.Width, bm.Height));
                //Draw grid
                for (int i = 1; i < loadedMap.height; i++)
                {
                    gr.DrawLine(Pens.Black, 0, i * scaleFactor, bm.Width, i * scaleFactor);
                }
                for (int i = 1; i < loadedMap.width; i++)
                {
                    gr.DrawLine(Pens.Black, i * scaleFactor, 0, i * scaleFactor, bm.Height);
                }
            }
            return bm;
        }


    }
}
