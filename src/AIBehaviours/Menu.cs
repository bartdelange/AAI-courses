#region

using System;
using System.Windows.Forms;

#endregion

namespace AIBehaviours
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void pathFinding_Click(object sender, EventArgs e)
        {
            var pF = new PathFollowingDemo {menu = this};
            pF.Show();
            Hide();
        }

        private void playGround_Click(object sender, EventArgs e)
        {
            var pG = new PlayGround {menu = this};
            pG.Show();
            Hide();
        }
    }
}