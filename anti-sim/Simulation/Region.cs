using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AntSim
{
    namespace Simulation
    {
		//Also part of the never-finished new ant search algorithm. Not completed, but I have
		//left the code behind for you to see.
        class Region
        {
            private List<Ant> ants = new List<Ant>();

            public void Clear()
            {
                this.ants.Clear();
            }

            public bool AppendAnt(Ant ant)
            {
                if (!this.ants.Contains(ant))
                {
                    this.ants.Add(ant);
                    return true;
                }
                else
                    return false;
            }
        }
    }
}