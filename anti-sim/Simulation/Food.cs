using System;

namespace AntSim
{
	namespace Simulation
	{
		class Food : Entity
		{
			private int foodCargo = 1000;

			public Food(Context parent) : base(parent)
			{
				//Constructor
			}

			public int FoodCargo
			{
				get { return this.foodCargo; }
				set { this.foodCargo = value; }
			}

			public int TakeFood(int food)
			{
				this.FoodCargo -= food;
				return food;
			}
		}
	}
}

