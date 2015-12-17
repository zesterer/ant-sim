using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Simulation
    {
        class Ant
        {
            private Common.Vec2 position;
            private int time = 0;

            public Ant()
            {
                Console.WriteLine("Created ant");
            }

            public Common.Vec2 Position
            {
                get { return this.position; }
                set { this.position = value; }
            }

            public void Tick()
            {
                this.time++;
            }
        }
    }
}