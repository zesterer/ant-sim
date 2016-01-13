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
			private float displayScale = 1.0f;

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

            private void pauseButtonClick(object sender, EventArgs e)
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

			private void drawPanelClick(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.parent.Context.createFoodAt(new Common.Vec2((int)(e.X / this.displayScale), (int)(e.Y / this.displayScale)));
				}
				else if (e.Button == MouseButtons.Right)
				{
							this.parent.Context.createAntAt(new Common.Vec2((int)(e.X / this.displayScale), (int)(e.Y / this.displayScale)));
				}
			}

			private void DrawObject(Graphics graphics, Brush brush, Common.Vec2 position, int radius)
			{
				graphics.FillEllipse(brush, new RectangleF(position.X - radius, position.Y - radius, radius * 2 + 1, radius * 2 + 1));
			}

			private void drawPanelPaint(object sender, PaintEventArgs e)
			{

				e.Graphics.Clear(Color.White);

				SolidBrush antBrush = new SolidBrush(Color.Brown);
				SolidBrush foodBrush = new SolidBrush(Color.Green);
				SolidBrush nestBrush = new SolidBrush(Color.Blue);

				for (int i = 0; i < this.parent.Context.AntCount; i ++)
				{
					Simulation.Ant ant = this.parent.Context.getAnt(i);
					this.DrawObject(e.Graphics, antBrush, ant.Position * this.displayScale, (int)(1 * this.displayScale));
				}

				for (int i = 0; i < this.parent.Context.FoodCount; i ++)
				{
					Simulation.Food food = this.parent.Context.getFood(i);
							this.DrawObject(e.Graphics, foodBrush, food.Position * this.displayScale, (int)(2 * this.displayScale));
				}

				for (int i = 0; i < this.parent.Context.NestCount; i ++)
				{
					Simulation.Nest nest = this.parent.Context.getNest(i);
									this.DrawObject(e.Graphics, nestBrush, nest.Position * this.displayScale, (int)(3 * this.displayScale));
				}
			}
        }
    }
}
