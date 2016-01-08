using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntSim
{
    namespace Frontend
    {
        partial class DisplayForm : Form
        {
            private Application parent;
            private bool paused;
			private int lastDrawTime = 0;

             public DisplayForm(Application parent = null)
             {
				this.DoubleBuffered = true;

                this.SetParent(parent);
                this.InitializeComponent();
             }

             public void SetParent(Application parent)
             {
                 this.parent = parent;
             }

            private void simulationTimer_Tick(object sender, EventArgs e)
            {
                if (this.parent != null)
                {
                    if (!this.paused)
						this.parent.TickSimulation();
                }

				if (this.lastDrawTime != this.parent.Context.Time)
				{
					this.drawPanel.Refresh();
					this.lastDrawTime = this.parent.Context.Time;
				}
            }

            private void pauseButton_Click(object sender, EventArgs e)
            {
                this.paused ^= true;

                if (this.paused)
                {
					this.pauseButton.Text = "Resume";
                    /*System.Reflection.Assembly thisExe;
                    thisExe = System.Reflection.Assembly.GetExecutingAssembly();
                    string[] resources = thisExe.GetManifestResourceNames();
                    string list = "";

                    // Build the string of resources.
                    foreach (string resource in resources)
                        list += resource + "\r\n";

                    Console.WriteLine(list);

                    System.IO.Stream pauseIcon = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AntSim.Properties.Resources.pauseIcon.png");
                    this.pauseButton.Image = Image.FromStream(pauseIcon);*/
                }
				else
					this.pauseButton.Text = "Pause";
			}

			private void DrawAnt(Graphics graphics, Brush brush, Common.Vec2 position, int radius)
			{
				graphics.FillEllipse(brush, new RectangleF(position.X - radius, position.Y - radius, radius * 2 + 1, radius * 2 + 1));
			}

			private void drawPanel_Paint(object sender, PaintEventArgs e)
			{

				e.Graphics.Clear(Color.White);

				SolidBrush brush = new SolidBrush(Color.Black);

				for (int i = 0; i < this.parent.Context.AntCount; i ++)
				{
					Simulation.Ant ant = this.parent.Context.getAnt(i);
					this.DrawAnt(e.Graphics, brush, ant.Position, 0);
				}
			}
        }
    }
}
