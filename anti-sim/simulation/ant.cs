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
            private Common.Vec2 velocity;
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

            void Move(Common.Vec2 vec)
            {
                this.velocity += vec;
            }

            public void Tick()
            {
                //Do stuff
            }

            public void PostTick()
            {
                this.position += this.velocity;
                this.time++;
            }
        }
    }
}