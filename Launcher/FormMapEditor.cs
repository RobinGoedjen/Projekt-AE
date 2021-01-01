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

        public FormMapEditor()
        {
            InitializeComponent();
        }
        public FormMapEditor(Map map)
        {
            InitializeComponent();
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
            mapVisualizer = new MapVisualizer(getDrawAbleSize(), currentMap);
            pictureBoxMap.Image = mapVisualizer.currentMapImage;
        }


        private void selectButton_click(object sender, EventArgs e)
        {
            selectedTileID = Convert.ToSByte(((Button)sender).Text);
        }

        private void saveMap(object sender, EventArgs e)
        {
            String mapName = txtMapName.Text;
            if (!checkMapName(mapName))
            {
                //Show error here
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
            mapVisualizer = new MapVisualizer(getDrawAbleSize(), currentMap);
            pictureBoxMap.Image = mapVisualizer.currentMapImage;
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

        private void pictureBoxMap_Mouse(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            long sectionX = pictureBoxMap.Width / currentMap.width;
            long sectionY = pictureBoxMap.Height / currentMap.height;
            if (setPlayer)
            {
                currentMap.playerStartPosition.X = (float)e.X / sectionX;
                currentMap.playerStartPosition.Y = (float)e.Y / sectionY;
                mapVisualizer = new MapVisualizer(getDrawAbleSize(), currentMap);
                pictureBoxMap.Image = mapVisualizer.currentMapImage;
                setPlayer = false;
                return;
            }
            
            int coordX = (int)(e.X / sectionX);
            int coordY = (int)(e.Y / sectionY);
            if (mapVisualizer.colorCoordinate(new Point(coordX, coordY), new SolidBrush(Map.getColorFromTileID(selectedTileID))))
            {
                mapVisualizer.redrawNonTiles();
                pictureBoxMap.Image = mapVisualizer.currentMapImage;
                currentMap.worldMap[coordY][coordX] = selectedTileID;
            }
        }

        private void btnSetPlayerPosition_Click(object sender, EventArgs e)
        {
            setPlayer = true;
        }

        private void trackBarPlayerOrientation_ValueChanged(object sender, EventArgs e)
        {
            currentMap.playerStartOrientation = (ushort)trackBarPlayerOrientation.Value;
            mapVisualizer = new MapVisualizer(getDrawAbleSize(), currentMap);
            pictureBoxMap.Image = mapVisualizer.currentMapImage;
        }
    }
}
