using System;
using System.IO;
using System.Collections.Generic;
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
using Newtonsoft.Json.Linq;

namespace MapEditor
{
    public partial class FormMain : Form
    {
        string projectDirectory = Environment.CurrentDirectory;
        string[] availableMaps;
        public FormMain()
        {
            InitializeComponent();
            loadMapsFromFolder();
        }

        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            if (availableMaps.Length == 0)
            {
                MessageBox.Show("There are no maps to load!", "Error");
                return;
            }
            if (listBoxMaps.SelectedIndex == -1)
            {
                listBoxMaps.SelectedIndex = 0;
            }
            Process.Start(Directory.GetCurrentDirectory() + "/Raycasting.exe", '"' + availableMaps[listBoxMaps.SelectedIndex] + '"' + (checkBoxUseWallTextures.Checked ? " true" :  ""));
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            new FormMapEditor().ShowDialog();
            loadMapsFromFolder();
        }

        private void loadMapsFromFolder()
        {
            listBoxMaps.Items.Clear();
            availableMaps = Directory.GetFiles(projectDirectory + @"\Maps");

            foreach (var file in availableMaps)
            {
                listBoxMaps.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void btnEditMap_Click(object sender, EventArgs e)
        {
            if (availableMaps.Length == 0)
            {
                MessageBox.Show("There are no maps to edit!", "Error");
                return;
            }
            if (listBoxMaps.SelectedIndex == -1)
            {
                listBoxMaps.SelectedIndex = 0;
            }
            var map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(availableMaps[listBoxMaps.SelectedIndex]));
            
            new FormMapEditor(map).ShowDialog();
            pictureBoxPreview.Visible = false;
            loadMapsFromFolder();
        }

        private void listBoxMaps_Click(object sender, EventArgs e)
        {
            if (listBoxMaps.SelectedIndex == -1)
                return;
            var map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(availableMaps[listBoxMaps.SelectedIndex]));
            Size size = new Size(this.Width - pictureBoxPreview.Left - 50, this.Height - pictureBoxPreview.Top - 50);
            var mapVisualizer = new MapVisualizer(size, map);
            pictureBoxPreview.Image = mapVisualizer.currentMapImage;
            pictureBoxPreview.Visible = true;
        }

        private void btnDeleteMap_Click(object sender, EventArgs e)
        {
            if (listBoxMaps.SelectedIndex == -1)
                return;
            var result = MessageBox.Show("Delete " + listBoxMaps.SelectedItem + "?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;
            if (!File.Exists(availableMaps[listBoxMaps.SelectedIndex]))
                return;
            File.Delete(availableMaps[listBoxMaps.SelectedIndex]);
            pictureBoxPreview.Visible = false;
            loadMapsFromFolder();
        }
    }
}
