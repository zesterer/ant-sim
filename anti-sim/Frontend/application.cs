using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Frontend
    {
        class Application
        {
            private Simulation.SimContext main_context;

            public Application()
            {
                Console.WriteLine("Created application");

                this.main_context = new Simulation.SimContext();
            }

            public void Tick()
            {
                this.main_context.Tick();
            }
        }
    }
}