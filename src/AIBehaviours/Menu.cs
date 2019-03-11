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

            demoComboBox.Items.AddRange(_menuItems);
            demoComboBox.SelectedIndex = 1;
        }

        private void OnDemoClose(object sender, FormClosedEventArgs args)
        {
            Show();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            var menuItem = (MenuItem) demoComboBox.SelectedItem;
            var demoForm = (Form) Activator.CreateInstance(menuItem.Value);

            demoForm.Show();
            demoForm.FormClosed += OnDemoClose;

            Hide();
        }
    }
}