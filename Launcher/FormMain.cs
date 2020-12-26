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
        List<Map> maps = new List<Map>();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            listBoxMaps.Items.Clear();
            String[] files = Directory.GetFiles(projectDirectory + @"\Maps");

            foreach (var file in files)
            {
                listBoxMaps.Items.Add(JsonConvert.DeserializeObject<Map>(File.ReadAllText(file)));
            }
        }

        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            Process.Start(Directory.GetCurrentDirectory() + "/Raycasting.exe");
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            FormMapEditor mapEditor = new FormMapEditor();
            mapEditor.ShowDialog();
        }
    }
}
