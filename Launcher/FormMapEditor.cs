using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using MapLibrary;

namespace MapEditor
{
    public partial class FormMapEditor : Form
    {
        Map currentMap = null;
        MapVisualizer mapVisualizer;
        sbyte selectedTileID = 0;
        bool setPlayer = false;
        bool beginDraw = true;
        Random rand = new Random();

        public FormMapEditor()
        {
            InitializeComponent();
            listBoxSprites.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxSprites.DrawItem += listBox_DrawItem;
            foreach (SpriteName name in (SpriteName[])Enum.GetValues(typeof(SpriteName)))
            {
                if (Map.canBeDrawnByUser(name))
                    listBoxSprites.Items.Add(name);
            }
            listBoxSprites.SelectedIndex = 0;
        }
        public FormMapEditor(Map map) : this()
        {
            currentMap = map;
            txtMapName.Text = map.name;
            numericUpDownMapDimX.Value = map.width;
            numericUpDownMapDimY.Value = map.height;
            trackBarPlayerOrientation.Value = map.playerStartOrientation;
        }
        private void FormMapEditor_Shown(object sender, EventArgs e)
        {
            if (currentMap == null)
                generateNewMap();
            drawFromScratch();
        }


        private void selectButton_click(object sender, EventArgs e)
        {
            selectedTileID = Convert.ToSByte(((Button)sender).Text);
            radioButtonSpriteIgnore.Checked = true;
        }

        private void saveMap(object sender, EventArgs e)
        {
            String mapName = txtMapName.Text;
            if (!checkMapName(mapName))
            {
                //Show error here TODO
                return;
            }
            currentMap.name = mapName;
            String mapJson = JsonConvert.SerializeObject(currentMap);
            string projectDirectory = Environment.CurrentDirectory;
            File.WriteAllText(projectDirectory + @"\Maps\" + mapName + ".json", mapJson);
        }

        private void btnChangeDim_Click(object sender, EventArgs e)
        {
            generateNewMap();
            drawFromScratch();
        }

        private void generateNewMap()
        {
            uint dimX = (uint)numericUpDownMapDimX.Value;
            uint dimY = (uint)numericUpDownMapDimY.Value;
            currentMap = new Map(dimX, dimY);
            for (int i = 0; i < dimY; i++)
            {
                currentMap.worldMap.Add(new List<sbyte>());
                for (int j = 0; j < dimX; j++)
                {
                    if (i == dimY-1 || i == 0 || j == 0 || j == dimX-1)
                    {
                        currentMap.worldMap[i].Add(4);
                        continue;
                    }
                    currentMap.worldMap[i].Add(0);
                }
            }
        }

        private bool checkMapName(string name)
        {
            if(String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            if (name.Contains('.'))
            {
                return false;
            }
            return true;
        }

        private Size getDrawAbleSize()
        {
            int maxWidth = this.Width - pictureBoxMap.Left - 50;
            int maxHeight = this.Height - pictureBoxMap.Top - 50;
            return new Size(maxWidth, maxHeight);
        }

        private void drawFromScratch()
        {
            if (mapVisualizer != null)
                mapVisualizer.Dispose();
            mapVisualizer = new MapVisualizer(getDrawAbleSize(), currentMap);
            pictureBoxMap.Image = mapVisualizer.currentMapImage;
        }

        private void pictureBoxMap_Mouse(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            long sectionX = pictureBoxMap.Width / currentMap.width;
            long sectionY = pictureBoxMap.Height / currentMap.height;
            float coordX = (float)e.X / sectionX;
            float coordY = (float)e.Y / sectionY;
            if (setPlayer)
            {
                currentMap.playerStartPosition.X = coordX;
                currentMap.playerStartPosition.Y = coordY;
                drawFromScratch();
                setPlayer = false;
                return;
            }

            if (radioButtonSpriteDraw.Checked)
            {
                var newSpriteCoords = new PointF(coordX, coordY);
                if (currentMap.sprites.Count > 0)
                {
                    var lastSprite = currentMap.sprites[currentMap.sprites.Count - 1];
                    var distance = lastSprite.getDistanceToPoint(newSpriteCoords);
                    if (!beginDraw && distance < 0.5)
                        return;
                }
                beginDraw = false;
                spriteData newSprite;
                newSprite.name = (SpriteName)listBoxSprites.SelectedItem;
                newSprite.position = newSpriteCoords;
                currentMap.sprites.Add(newSprite);
                drawFromScratch();
                return;
            }

            if (radioButtonSpriteDelete.Checked)
            {
                var newSpriteCoords = new PointF(coordX, coordY);
                var delete = new List<spriteData>();
                foreach (var sprite in currentMap.sprites.Where<spriteData>(x => x.getDistanceToPoint(newSpriteCoords) < 0.2f))
                {
                    delete.Add(sprite);
                }
                foreach (var sprite in delete)
                {
                    currentMap.sprites.Remove(sprite);
                }
                if (delete.Count > 0)
                    drawFromScratch();
                return;
            }
            
            if (mapVisualizer.colorCoordinate(new Point((int)coordX, (int)coordY), new SolidBrush(Map.getColorFromTileID(selectedTileID))))
            {
                mapVisualizer.redrawNonTiles();
                pictureBoxMap.Image = mapVisualizer.currentMapImage;
                currentMap.worldMap[(int)coordY][(int)coordX] = selectedTileID;
            }
        }

        private void btnSetPlayerPosition_Click(object sender, EventArgs e)
        {
            setPlayer = true;
        }

        private void trackBarPlayerOrientation_ValueChanged(object sender, EventArgs e)
        {
            currentMap.playerStartOrientation = (ushort)trackBarPlayerOrientation.Value;
            drawFromScratch();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            uint dimX = (uint)numericUpDownMapDimX.Value;
            uint dimY = (uint)numericUpDownMapDimY.Value;
            currentMap = new Map(dimX, dimY);
            for (int i = 0; i < dimY; i++)
            {
                currentMap.worldMap.Add(new List<sbyte>());
                for (int j = 0; j < dimX; j++)
                {
                    currentMap.worldMap[i].Add(1);
                }
            }
            recursiveBacktracker(1, 1);
            drawFromScratch();
        }

        private bool recursiveBacktracker(int x, int y)
        {
            if (pointIsValid(x, y))
            {
                currentMap.worldMap[x][y] = 0;
                Point[] neighbours = getValidNeighbours(x, y);

                if (neighbours == null || neighbours.Length == 0)
                {
                    return false;
                }

                Point point = neighbours[rand.Next(neighbours.Length)];

                if (x < point.X) { currentMap.worldMap[x + 1][y] = 0; }
                if (x > point.X) { currentMap.worldMap[x - 1][y] = 0; }
                if (y < point.Y) { currentMap.worldMap[x][y + 1] = 0; }
                if (y > point.Y) { currentMap.worldMap[x][y - 1] = 0; }

                if (!recursiveBacktracker(point.X, point.Y))
                {
                    return recursiveBacktracker(x, y);
                }

                recursiveBacktracker(point.X, point.Y);
            }
            return false;
        }

        private bool pointIsValid(int x, int y)
        {
            if (x > 0 && y > 0 && x < (int)numericUpDownMapDimX.Value && y < (int)numericUpDownMapDimY.Value)
            {
                return true;
            }
            return false;
        }

        private Point[] getValidNeighbours(int x, int y)
        {
            List<Point> resultList = new List<Point>();

            if (pointIsValid(x + 2, y) && currentMap.worldMap[x + 2][y] != 0)
            {
                resultList.Add(new Point(x + 2, y));
            }
            if (pointIsValid(x, y + 2) && currentMap.worldMap[x][y + 2] != 0)
            {
                resultList.Add(new Point(x, y + 2));
            }
            if (pointIsValid(x - 2, y) && currentMap.worldMap[x - 2][y] != 0)
            {
                resultList.Add(new Point(x - 2, y));
            }
            if (pointIsValid(x, y - 2) && currentMap.worldMap[x][y - 2] != 0)
            {
                resultList.Add(new Point(x, y - 2));
            }
            return resultList.ToArray();
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(MapVisualizer.getColorForSpirte((SpriteName)listBoxSprites.Items[e.Index])), e.Bounds);
            ListBox lb = (ListBox)sender;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                g.DrawString(lb.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.White), new PointF(e.Bounds.X, e.Bounds.Y));
            else
                g.DrawString(lb.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }

        private void pictureBoxMap_MouseUp(object sender, MouseEventArgs e)
        {
            beginDraw = true;
        }
    }
}
