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
        public partial class DisplayForm : Form
        {
            private Application parent;

            private bool paused;

             public DisplayForm(Application parent = null)
             {
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
                    if (this.paused)
                        this.parent.TickSimulation();
                }
            }

            private void pauseButton_Click(object sender, EventArgs e)
            {
                this.paused ^= true;

                if (this.paused)
                {
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
			}

			private void drawPanel_Paint(object sender, PaintEventArgs e)
			{
				SolidBrush brush = new SolidBrush(Color.Blue);
				e.Graphics.FillRectangle(brush, new RectangleF(10, 10, 20, 20));
			}
        }
    }
}
