using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Frontend
    {
		//The application class. Contains EVERYTHING.
        class Application : System.Windows.Forms.ApplicationContext
        {
            private Simulation.Context mainContext;
            private DisplayForm displayForm;
			private int frameSkip = 1;

            public Application() //Constructor, where the GUI is created and the simulation context is created
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
                    System.Windows.Forms.Application.DoEvents(); //The GUI event loop
                }
            }

			public int FrameSkip //Public getter and setter for the number of frames executed per redraw
			{
				get { return this.frameSkip; }
				set { this.frameSkip = Math.Max(0, value); }
			}

			public Simulation.Context Context //Public getter for the simulation
			{
				get { return this.mainContext; }
			}

            public void TickSimulation() //Some code to actually tick the simulation
            {
                //Tick the simulation context
                //Console.WriteLine("Ticking...");

				for (int i = 0; i < this.frameSkip; i ++) //Repeat according to frameskip value
	                this.mainContext.Tick();
            }
        }
    }
}