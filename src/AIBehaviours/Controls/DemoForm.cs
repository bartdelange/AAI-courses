using System;
using System.Drawing;
using System.Windows.Forms;
using AICore;
using AICore.Worlds;
using MainMenuItem = System.Windows.Forms.MenuItem;

namespace AIBehaviours.Controls
{
    public abstract class DemoForm : Form
    {
        protected WorldControl WorldControl { get; }

        protected IWorld World
        {
            get => WorldControl.World;
            set => WorldControl.World = value;
        }

        protected Size WorldSize => new Size(
            WorldControl.Width,
            WorldControl.Height - 20
        );

        private MainMenuItem _toggleDebugMenuItem;

        protected DemoForm(Size size)
        {
            Size = size;

            WorldControl = new WorldControl();

            Controls.Add(WorldControl);

            CreateMenu();
        }

        private void CreateMenu()
        {
            _toggleDebugMenuItem = new MainMenuItem("Toggle debug", ToggleDebug)
            {
                Checked = Config.Debug
            };

            Menu = new MainMenu
            {
                MenuItems =
                {
                    new MainMenuItem("Options")
                    {
                        MenuItems = {_toggleDebugMenuItem}
                    }
                }
            };
        }

        private void ToggleDebug(object sender, EventArgs e)
        {
            Config.Debug = !Config.Debug;
            _toggleDebugMenuItem.Checked = Config.Debug;
        }
    }
}