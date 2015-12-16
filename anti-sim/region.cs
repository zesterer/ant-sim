using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace anti_sim
{
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
