using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AIBehaviours.Demos;

namespace AIBehaviours
{
    public partial class Menu : Form
    {
        private readonly object[] _menuItems = new object[]
        {
            new MenuItem("Path following", typeof(PathFollowingDemo)),
            new MenuItem("Offset pursuit", typeof(OffsetPursuitDemo)),
            new MenuItem("Behaviour playground", typeof(PlayGround))
        };

        public Menu()
        {
            InitializeComponent();

            demoComboBox.Items.AddRange(_menuItems);
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