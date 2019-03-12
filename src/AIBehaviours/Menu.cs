using System;
using System.Windows.Forms;
using AIBehaviours.Demos;

namespace AIBehaviours
{
    public partial class Menu : Form
    {
        private readonly object[] _menuItems = {
            new MenuItem("Path following", typeof(PathFollowingDemo)),
            new MenuItem("Offset pursuit", typeof(OffsetPursuitDemo)),
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
            var demoForm = (Form) Activator.CreateInstance(menuItem.Value, 1000, 800);

            demoForm.Width = 1000;
            demoForm.Height = 800;
            demoForm.Show();
            demoForm.FormClosed += OnDemoClose;

            Hide();
        }
    }
}