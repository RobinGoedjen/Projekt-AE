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
        String[] availableMaps;
        public FormMain()
        {
            InitializeComponent();
            laodMapsFromFolder();
        }

        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            if (availableMaps.Length == 0)
            {
                //Show error Message
                return;
            }
            if (listBoxMaps.SelectedIndex == -1)
            {
                listBoxMaps.SelectedIndex = 0;
            }
            Process.Start(Directory.GetCurrentDirectory() + "/Raycasting.exe", '"' + availableMaps[listBoxMaps.SelectedIndex] + '"');
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            new FormMapEditor().ShowDialog();
            laodMapsFromFolder();
        }

        private void laodMapsFromFolder()
        {
            listBoxMaps.Items.Clear();
            availableMaps = Directory.GetFiles(projectDirectory + @"\Maps");

            foreach (var file in availableMaps)
            {
                listBoxMaps.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }
    }
}
