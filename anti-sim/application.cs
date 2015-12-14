using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anti_sim
{
    class Application
    {
        private World testWorld;

        public Application()
        {
            Console.WriteLine("Created application");

            this.testWorld = new World();
            this.testWorld.setup(32, 32);
        }
    }
}
