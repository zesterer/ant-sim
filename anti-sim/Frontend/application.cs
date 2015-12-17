using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Frontend
    {
        public class Application : System.Windows.Forms.ApplicationContext
        {
            private Simulation.Context main_context;
            private DisplayForm display_form;

            public Application()
            {
                Console.WriteLine("Created application");

                this.display_form = new DisplayForm();
                this.display_form.SetParent(this);
                this.display_form.Show();
                System.Windows.Forms.Application.EnableVisualStyles();

                this.main_context = new Simulation.Context();
            }

            public void Run()
            {
                while (this.display_form.Visible)
                {
                    //Tick the simulation context
                    Console.WriteLine("Ticking...");

                    //Perform GUI events
                    System.Windows.Forms.Application.DoEvents();
                }
            }

            public void TickSimulation()
            {
                this.main_context.Tick();
            }
        }
    }
}