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
        Button activeButton;
        int mapDimension;
        uint[,] worldMap = new uint[,] { };

        public FormMapEditor()
        {
            InitializeComponent();
        }

        private void FormMapEditor_Load(object sender, EventArgs e)
        {
            int startX = 300;
            int posX = startX;
            int posY = 100;
            mapDimension = 9;

            for(int i = 0; i < mapDimension; i++)
            {
                for (int j = 0; j < mapDimension; j++)
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
        }

        private void btn_click(object sender, EventArgs e)
        {
            if(activeButton == null) { return; }
            Button button = (Button) sender;
            button.BackColor = activeButton.BackColor;
            button.ForeColor = activeButton.ForeColor;
            button.FlatAppearance.MouseOverBackColor = activeButton.BackColor;
            button.Text = activeButton.Text;
            Console.WriteLine(button.Name);
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
            worldMap[5, 5] = 1;
            for (int i = 0; i < mapDimension; i++)
            {
                for (int j = 0; j < mapDimension; j++)
                {
                    Control btn = this.Controls.Find(i.ToString() + "-" + j.ToString(), true)[0];
                }
            }
            Console.WriteLine(worldMap);

            //Map map = new Map("test.json");
            //map.worldMap = worldMap.ToArray();

            //String mapJson = JsonConvert.SerializeObject(map);

            //Console.WriteLine(Directory.GetCurrentDirectory() + @"\" + map.filename);

            //File.WriteAllText(projectDirectory + @"\Maps\" + map.filename, mapJson);
        }
    }
}
