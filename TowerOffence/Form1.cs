using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerOffence
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Load += (sender, eventArgs) =>
            {
                base.OnLoad(eventArgs);
                DoubleBuffered = true;
                WindowState = FormWindowState.Maximized;
            };

            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show("Действительно закрыть?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };
        }


    }
}
