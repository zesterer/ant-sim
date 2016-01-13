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
		class Nest : Entity
		{
			private int foodCargo = 0;

			public Nest (Context parent) : base(parent)
			{
			}

			public int FoodCargo
			{
				get { return this.foodCargo; }
				set { this.foodCargo = value; }
			}
		}
	}
}

