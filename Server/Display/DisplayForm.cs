using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Server.Display
{
    public partial class DisplayForm : Form
    {
        public DisplayForm()
        {
            InitializeComponent();
            refreshTimer.Enabled = true;
        }

        public Universe Universe { get; set; }
        public Dictionary<int, Control> controls = new Dictionary<int, Control>();

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            foreach (var controlPair in controls)
            {
                controlPair.Value.Visible = false;
            }
            foreach (var entity in Universe.Entites)
            {
                Control control;
                if (controls.ContainsKey(entity.Id))
                {
                    control = controls[entity.Id];
                    control.Visible = true;
                }
                else
                {
                    control = CreateControl(entity);
                    controls[entity.Id] = control;
                }
                UpdatePosition(control, entity);
            }
        }

        private void UpdatePosition(Control control, Entity entity)
        {
            control.Location = ConvertPosition(entity.Position);
        }

        private Point ConvertPosition(Vector2 position)
        {
            return new Point((int)(position.X * 10 + 200), (int)(position.Y * 10 + 200));
        }

        private Control CreateControl(Entity entity)
        {
            switch (entity.GetClientType())
            {
                case EntityUpdateType.Star:
                    var star = new Button();
                    star.Parent = this;
                    star.Text = "x";
                    star.Width = 20;
                    star.Height = 20;
                    return star;
                case EntityUpdateType.Ship:
                    var ship = new Label();
                    ship.Parent = this;
                    ship.Text = "S";
                    return ship;
                case EntityUpdateType.Asteroid:
                    var asteroid = new Label();
                    asteroid.Parent = this;
                    asteroid.Text = "A";
                    return asteroid;
                default:
                    throw new Exception();
            }
        }
    }
}
