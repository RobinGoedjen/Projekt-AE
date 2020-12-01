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

namespace MapEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnPlayGame_Click(object sender, EventArgs e)
        {
            Process.Start(Directory.GetCurrentDirectory() + "/Raycasting.exe");
        }

        private void btnMapEditor_Click(object sender, EventArgs e)
        {
            Map map = new Map("test.json");

            String mapJson = JsonConvert.SerializeObject(map);

            Console.WriteLine(Directory.GetCurrentDirectory() + @"\" + map.filename);

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Maps\" + map.filename, mapJson);
         
            //new FormMapEditor().ShowDialog();
        }
    }
}
