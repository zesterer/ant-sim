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
		//The nest class. Inherits properties from Entity, and adds new properties such as the ability to carry food.
		class Nest : Entity
		{
			private int foodCargo = 0;

			public Nest (Context parent) : base(parent)
			{
			}

			//Public getter and setter for food
			public int FoodCargo
			{
				get { return this.foodCargo; }
				set { this.foodCargo = value; }
			}

			//Utility function to add food
			public int GiveFood(int food)
			{
				this.FoodCargo += food;
				return food;
			}
		}
	}
}

