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
        List<List<int>> worldMap = new List<List<int>>();
        Button activeButton;
        int mapDimensionY;
        int mapDimensionX;
        bool mapGenerated = false;

        public FormMapEditor()
        {
            InitializeComponent();
        }

        private void FormMapEditor_Load(object sender, EventArgs e)
        {
            
            mapDimensionY = (int) mapDimX.Value;
            mapDimensionX = (int) mapDimY.Value;
            generateMap(mapDimensionY, mapDimensionX);
        }

        private void btn_click(object sender, EventArgs e)
        {
            if(activeButton == null) { return; }
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

            foreach(Button b in mainButtons.ToArray())
            {
                b.FlatAppearance.BorderColor = b.BackColor;
            }
            
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.LightGreen;
            activeButton = button;
        }

        private void saveMap(object sender, EventArgs e)
        {
            for (int i = 0; i < mapDimensionY; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < mapDimensionX; j++)
                {
                    Control btn = this.Controls.Find(i.ToString() + "-" + j.ToString(), true)[0];
                    row.Add(Convert.ToInt32(btn.Text));
                }
                worldMap.Add(row);
            }

            String mapName = txtMapName.Text;
            if(checkMapName(mapName))
            {
                Map map = new Map(mapName + ".json");

                map.worldMap = worldMap;

                String mapJson = JsonConvert.SerializeObject(map);

                string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

                File.WriteAllText(projectDirectory + @"\Maps\" + map.filename, mapJson);
            }
        }

        private void btnChangeDim_Click(object sender, EventArgs e)
        {
            if (mapGenerated)
            {
                clearMap();
            }
            mapDimensionY = (int)mapDimY.Value;
            mapDimensionX = (int)mapDimX.Value;

            generateMap(mapDimensionY, mapDimensionX);
        }

        private void clearMap()
        {
            for (int i = 0; i < mapDimensionY; i++)
            {
                for (int j = 0; j < mapDimensionX; j++)
                {
                    Control btn = this.Controls.Find(i.ToString() + "-" + j.ToString(), true)[0];
                    this.Controls.Remove(btn);
                }
            }
        }

        private void generateMap(int dimX, int dimY)
        {
            int startX = 300;
            int posX = startX;
            int posY = 100;

            for (int i = 0; i < dimX; i++)
            {
                for (int j = 0; j < dimY; j++)
                {
                    Button button = new Button();
                    button.Name = i.ToString() + "-" + j.ToString();
                    button.Size = new Size(50, 50);
                    button.Location = new Point(posX, posY);
                    button.BackColor = Color.White;
                    button.Click += new EventHandler(btn_click);
                    button.FlatStyle = FlatStyle.Flat;
                    button.Text = "0";
                    button.FlatAppearance.MouseOverBackColor = Color.White;
                    button.ForeColor = Color.White;
                    this.Controls.Add(button);
                    posX += 51;
                }
                posX = startX;
                posY += 51;
            }
            mapGenerated = true;
        }

        private bool checkMapName(string name)
        {
            if(String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
            {
                // todo add label for error message
                return false;
            }
            if (name.Contains('.'))
            {
                return false;
            }
            return true;
        }
    }
}
