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
            private Simulation.Context mainContext;
            private DisplayForm displayForm;
			private int frameSkip = 1;

            public Application()
            {
                Console.WriteLine("Created application");

                this.displayForm = new DisplayForm();
                this.displayForm.SetParent(this);
                this.displayForm.Show();
                System.Windows.Forms.Application.EnableVisualStyles();

                this.mainContext = new Simulation.Context();
            }

            public void Run()
            {
                while (this.displayForm.Visible)
                {
                    //Perform GUI events
                    System.Windows.Forms.Application.DoEvents();
                }
            }

			public int FrameSkip
			{
				get { return this.frameSkip; }
				set { this.frameSkip = Math.Max(0, value); }
			}

			public Simulation.Context Context
			{
				get { return this.mainContext; }
			}

            public void TickSimulation()
            {
                //Tick the simulation context
                //Console.WriteLine("Ticking...");

				for (int i = 0; i < this.frameSkip; i ++)
	                this.mainContext.Tick();
            }
        }
    }
}