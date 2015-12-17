using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Simulation
    {
        class Application
        {
            private SimContext main_context;

            public Application()
            {
                Console.WriteLine("Created application");

                this.main_context = new SimContext();
            }

            public void Tick()
            {
                this.main_context.Tick();
            }
        }
    }
}