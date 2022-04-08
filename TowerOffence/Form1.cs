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
        GameModel game;
        Timer timer;
        public Form1()
        {
            timer = new Timer();
            game = new GameModel();
            InitializeComponent();

            timer.Interval = 15;
            timer.Tick += (sender, eventArgs) =>
            {
                Invalidate();
            };

            Paint += (sender, eventArgs) =>
            {
                foreach (var im in game.availableMonsters)
                    eventArgs.Graphics.DrawImage(im.image, new PointF(0,0));
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
