using System;
using System.Drawing;
using System.Windows.Forms;
using AIBehaviours.Demos;

namespace AIBehaviours
{
    public partial class Menu : Form
    {
        private readonly object[] _menuItems = {
            new MenuItem("Soccer game", new Size(1000, 800), typeof(WeirdSoccerGameDemo)),
            new MenuItem("Path following", new Size(800, 600), typeof(PathFollowingDemo)),
            new MenuItem("Offset pursuit", new Size(800, 600), typeof(OffsetPursuitDemo)),
            new MenuItem("Wall avoidance", new Size(800, 600), typeof(WallAvoidanceDemo)),
            new MenuItem("Obstacle avoidance", new Size(800, 600), typeof(ObstacleAvoidanceDemo)),
        };

        public Menu()
        {
            InitializeComponent();

            AcceptButton = submitButton;

            demoComboBox.Items.AddRange(_menuItems);
            demoComboBox.SelectedIndex = 0;
        }

        private void OnDemoClose(object sender, FormClosedEventArgs args)
        {
            Show();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem) demoComboBox.SelectedItem;
            var demoForm = (Form) Activator.CreateInstance(menuItem.Value, menuItem.Size);

            demoForm.Show();
            demoForm.FormClosed += OnDemoClose;

            Hide();
        }
    }
}