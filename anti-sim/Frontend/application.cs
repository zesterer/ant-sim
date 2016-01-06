using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Frontend
    {
        class Application : System.Windows.Forms.ApplicationContext
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
                    //Perform GUI events
                    System.Windows.Forms.Application.DoEvents();
                }
            }

			public Simulation.Context Context
			{
				get { return this.main_context; }
			}

            public void TickSimulation()
            {
                //Tick the simulation context
                Console.WriteLine("Ticking...");

                this.main_context.Tick();
            }
        }
    }
}