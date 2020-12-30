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
        List<Button> mainButtons = new List<Button>();
        List<Button> allMapButtons = new List<Button>();
        Map currentMap;
        MapVisualizer mapVisualizer;
        List<List<int>> worldMap = new List<List<int>>();
        Button activeButton;

        public FormMapEditor()
        {
            InitializeComponent();
        }
        private void FormMapEditor_Shown(object sender, EventArgs e)
        {
            generateNewMap();
            pictureBoxMap.Image = mapVisualizer.currentMapImage;
        }

        private void btn_click(object sender, EventArgs e)
        {
            if(activeButton == null) 
                return; 
            Button button = (Button) sender;
            button.BackColor = activeButton.BackColor;
            button.ForeColor = activeButton.ForeColor;
            button.FlatAppearance.MouseOverBackColor = activeButton.BackColor;
            button.Text = activeButton.Text;
        }

        private void selectButton_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (!mainButtons.Contains(button))
            {
                mainButtons.Add(button);
            }

            foreach(Button b in mainButtons) //wofür die liste?
            {
                b.FlatAppearance.BorderColor = b.BackColor;
            }
            
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.LightGreen;
            activeButton = button;
        }

        private void saveMap(object sender, EventArgs e)
        {
            String mapName = txtMapName.Text;
            if (!checkMapName(mapName))
            {
                //Show error here
                return;
            }


            ushort currentButtonIndex = 0;
            foreach (Button b in allMapButtons)
            {
                if (currentButtonIndex % currentMap.height == 0)
                {
                    currentMap.worldMap.Add(new List<sbyte>());
                }
                currentMap.worldMap[currentMap.worldMap.Count - 1].Add(Convert.ToSByte(b.Text));
                currentButtonIndex++;
            }

            String mapJson = JsonConvert.SerializeObject(currentMap);
            string projectDirectory = Environment.CurrentDirectory;
            File.WriteAllText(projectDirectory + @"\Maps\" + mapName + ".json", mapJson);
        }

        private void btnChangeDim_Click(object sender, EventArgs e)
        {
            clearMap();
            generateNewMap();
            pictureBoxMap.Image = mapVisualizer.currentMapImage;
        }

        private void clearMap()
        {
            foreach(Button b in allMapButtons)
            {
                this.Controls.Remove(b);
            }
        }

        private void generateNewMap()
        {
            uint dimX = (uint)numericUpDownMapDimX.Value;
            uint dimY = (uint)numericUpDownMapDimY.Value;
            currentMap = new Map(dimX, dimY);
            mapVisualizer = new MapVisualizer(currentMap, getDrawAbleSize());
            //int startX = 300;
            //int posX = startX;
            //int posY = 100;
            //allMapButtons.Clear();

            //for (int i = 0; i < dimX; i++)
            //{
            //    for (int j = 0; j < dimY; j++)
            //    {
            //        Button button = new Button();
            //        button.Name = i.ToString() + "-" + j.ToString();
            //        button.Size = new Size(50, 50);
            //        button.Location = new Point(posX, posY);
            //        button.BackColor = Color.White;
            //        button.Click += btn_click;
            //        button.FlatStyle = FlatStyle.Flat;
            //        button.Text = "0";
            //        button.FlatAppearance.MouseOverBackColor = Color.White;
            //        button.ForeColor = Color.White;
            //        button.Parent = this;
            //        posX += 51;
            //        allMapButtons.Add(button);
            //    }
            //    posX = startX;
            //    posY += 51;
            //}
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

        private void pictureBoxMap_MouseDown(object sender, MouseEventArgs e)
        {
            long sectionX = pictureBoxMap.Width / currentMap.width;
            long sectionY = pictureBoxMap.Height / currentMap.height;
            int coordX = (int)(e.X / sectionX);
            int coordY = (int)(e.Y / sectionY);
            this.Text = (coordX).ToString() + " " + coordY.ToString();
        }

        private Size getDrawAbleSize()
        {
            int maxWidth = this.Width - pictureBoxMap.Left - 50;
            int maxHeight = this.Height - pictureBoxMap.Top - 50;
            return new Size(maxWidth, maxHeight);
        }


    }
}
