using System;

namespace AntSim
{
	namespace Simulation
	{
		//The food class. Inherits properties from Entity, and adds new properties such as the ability to carry food.
		class Food : Entity
		{
			private int foodCargo = 1000;

			public Food(Context parent) : base(parent)
			{
				//Constructor
			}

			//Public getter and setter for food
			public int FoodCargo
			{
				get { return this.foodCargo; }
				set { this.foodCargo = value; }
			}

			//Utility function to remove food
			public int TakeFood(int food)
			{
				this.FoodCargo -= food;
				return food;
			}
		}
	}
}

