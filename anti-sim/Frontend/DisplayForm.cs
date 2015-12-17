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
                    this.parent.TickSimulation();
            }
        }
    }
}
