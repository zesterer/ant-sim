using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace anti_sim
{
    class SimContext
    {
        private World world;
        private ArrayList ants = new ArrayList();

        public SimContext()
        {
            world = new World();
            world.setup(64, 64);

            //Create a few test ants
            for (int i = 0; i < 10; i++)
                ants.Add(new Ant());
        }
    }
}
