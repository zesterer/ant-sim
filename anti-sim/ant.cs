using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anti_sim
{
    class Ant
    {
		private Vec2 position;
		private int lifetime = 0;

        public Ant()
        {
            Console.WriteLine("Created ant");
        }

		public Vec2 Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		void tick()
		{
			this.lifetime ++;
		}
    }
}
