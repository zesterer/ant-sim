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
			private float displayScale = 2.0f;

             public DisplayForm(Application parent = null) //Constructor. Not too complex.
             {
				this.DoubleBuffered = true;

                this.SetParent(parent);
                this.InitializeComponent();
             }

             public void SetParent(Application parent) //A method to change the GUI's parent. Not used, but useful utility function
             {
                 this.parent = parent;
             }

            private void simulationTimer_Tick(object sender, EventArgs e) //Timer tick event
            {
                if (this.parent != null) //If we have a parent
                {
                    if (!this.paused)
						this.parent.TickSimulation();
                }

				if (this.lastDrawTime != this.parent.Context.Time || this.paused) //Redraw the simulation!
				{
					this.drawPanel.Refresh();
					this.lastDrawTime = this.parent.Context.Time;
				}
            }

            private void pauseButtonClick(object sender, EventArgs e) //Switch between paused and ticking
            {
                this.paused ^= true; //Use bitwise operators. Because why not? :D

                if (this.paused) //Obvious
                	this.pauseButton.Text = "Resume";
				else
					this.pauseButton.Text = "Pause";
			}

			private void clearAntsButtonClick(object sender, EventArgs e) //Button click events. Pretty obvious
			{
				this.parent.Context.clearAnts();
			}

			private void clearFoodButtonClick(object sender, EventArgs e) //Button click events. Pretty obvious
			{
				this.parent.Context.clearFood();
			}

			private void clearNestsButtonClick(object sender, EventArgs e) //Button click events. Pretty obvious
			{
				this.parent.Context.clearNests();
			}

			private void drawPanelClick(object sender, MouseEventArgs e) //The panel click event
			{
				if (e.Button == MouseButtons.Left) //Create food if we're clicking the left mouse button
				{
					this.parent.Context.createFoodAt(new Common.Vec2((int)(e.X / this.displayScale), (int)(e.Y / this.displayScale)));
				}
				else if (e.Button == MouseButtons.Right) //Create a nest if we're clicking the middle mouse button
				{
							this.parent.Context.createAntAt(new Common.Vec2((int)(e.X / this.displayScale), (int)(e.Y / this.displayScale)));
				}
				else if (e.Button == MouseButtons.Middle) //Create an ant if we're clicking the right mouse button
				{
					this.parent.Context.createNestAt(new Common.Vec2((int)(e.X / this.displayScale), (int)(e.Y / this.displayScale)));
				}
			}

			private void DrawObject(Graphics graphics, Brush brush, Common.Vec2 position, int radius) //Just a utility function to draw some undefined entity
			{
				graphics.FillEllipse(brush, new RectangleF(position.X - radius, position.Y - radius, radius * 2 + 1, radius * 2 + 1));
			}

			private void drawPanelPaint(object sender, PaintEventArgs e) //The draw refresh event (where everything is drawn)
			{
				//Clear the screen first
				e.Graphics.Clear(Color.White);

				//Define the brushes used for each type of entity
				SolidBrush antBrush = new SolidBrush(Color.Brown);
				SolidBrush foodBrush = new SolidBrush(Color.Green);
				SolidBrush nestBrush = new SolidBrush(Color.Black);

				for (int i = 0; i < this.parent.Context.NestCount; i ++) //Loops through all nests in the simulation
				{
					//Get the nest we're currently drawing
					Simulation.Nest nest = this.parent.Context.getNest(i);
					//Draw it!
					this.DrawObject(e.Graphics, nestBrush, nest.Position * this.displayScale, (int)(2.0f * this.displayScale));
				}

				for (int i = 0; i < this.parent.Context.FoodCount; i ++) //Loops through all food in the simulation
				{
					//Get the food we're currently drawing
					Simulation.Food food = this.parent.Context.getFood(i);
					//Draw it!
					this.DrawObject(e.Graphics, foodBrush, food.Position * this.displayScale, (int)(2.0f * this.displayScale));
				}

				for (int i = 0; i < this.parent.Context.AntCount; i ++) //Loops through all ants in the simulation
				{
					//Get the ant we're currently drawing
					Simulation.Ant ant = this.parent.Context.getAnt(i);

					if (ant.FoodCargo > 0) //If it's carrying food, we need to draw a little food circle first
						this.DrawObject(e.Graphics, foodBrush, ant.Position * this.displayScale, (int)(1.0f * this.displayScale));

					//Draw it!
					this.DrawObject(e.Graphics, antBrush, ant.Position * this.displayScale, (int)(0.5f * this.displayScale));
				}
			}
        }
    }
}
