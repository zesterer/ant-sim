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
		//The simulation context class represents one simulation instance
        class Context
        {
			private int time = 0; //The global simulation age (used to update ant timestamps)
			private World world; //Used for the new ant detection code (not finished, but almost complete)

            private List<Ant> ants = new List<Ant>(); //The list containing all ants in the simulation
			private List<Nest> nests = new List<Nest>(); //The list containing all nests in the simulation
			private List<Food> food = new List<Food>(); //The list containing all food in the simulation

			private Random generator = new Random(4321); //The simulation's random generator (used by entities)

            public Context() //The Context constructor
            {
				//Create the world, and set it's size
				this.world = new World();
				this.world.Setup(16, 16, 16);

                //Create a few test ants
				for (int i = 0; i < 500; i++)
				{
					Ant ant = new Ant(this);
					ant.PlaceRandomly();
					this.ants.Add(ant);
				}

				//Create a test nest
				Nest mainNest = new Nest(this);
				mainNest.Position = this.world.Size / 2;
				this.nests.Add(mainNest);

				//Create a few pieces of test food (no longer used)
				/*for (int i = 0; i < 30; i++)
				{
					Food food = new Food(this);
					food.PlaceRandomly();
					this.food.Add(food);
				}*/
            }

			public Random Generator //The public getter and setter for the random generator
			{
				get { return this.generator; }
				set { this.generator = value; }
			}

			public World World //The public getter and setter for the world
			{
				get { return this.world; }
				set { this.world = value; }
			}

			public int Time //The public getter and setter for the simulation time
			{
				get { return this.time; }
			}

            public void Tick() //The tick method. Run every frame.
            {
                //Reset the world
				this.world.Reset(); //Reset the world (destroy current ant groups, since they've likely changed since the last tick (not fully implemented)

				//Add the ants within the world to their appropriate groups (not fully implemented)
                foreach (Ant ant in this.ants)
                    this.world.AddAnt(ant);

				//Check the food to see if it's finished.
				int i = 0;
				while (i < this.food.Count) //Loop through food
				{
					if (this.food[i].FoodCargo <= 0) //Is the food empty?
					{
						//If so, delete the food
						this.food.RemoveAt(i);
						Console.WriteLine("Food consumed");
					}
					else //Otherwise, move onto the next piece of food
						i ++;
				}

                //Tick the ants
                foreach (Ant ant in this.ants)
					ant.Tick();

                //Finish the tick for the ants
                foreach (Ant ant in this.ants)
					ant.PostTick();

                //Tick the world and update the simulation time
                this.world.Tick();
				this.time ++;
            }

			//Fairly obvious
			public void createFoodAt(Common.Vec2 position)
			{
				Food food = new Food(this);
				food.Position = position;
				this.food.Add(food);
			}

			//Also fairly obvious
			public void createAntAt(Common.Vec2 position)
			{
				Ant ant = new Ant(this);
				ant.Position = position;
				this.ants.Add(ant);
			}

			//Another obvious method
			public void createNestAt(Common.Vec2 position)
			{
				Nest nest = new Nest(this);
				nest.Position = position;
				this.nests.Add(nest);
			}

			//Deletes all food entities
			public void clearFood()
			{
				this.food.Clear();
			}

			//Same as above, but for ants
			public void clearAnts()
			{
				this.ants.Clear();
			}

			//Same as above, but for nests
			public void clearNests()
			{
				this.nests.Clear();
			}

			//Find ants within a specific range (default range is optional)
			public List<Ant> getAntsAt(Common.Vec2 position, double range = 1.0)
			{
				List<Ant> ants = new List<Ant>(); //Create a list to later contain all ants within range

				foreach (Ant ant in this.ants) //Loop through all ants in the world
				{
					if (ant.Position.DistanceTo(position) <= range) //Is it closer than the range?
						ants.Add(ant); //Add it to the list if so
				}

				return ants; //Return that list
			}

			//See previous method for explanation as to how this works (it's almost identical)
			public List<Food> getFoodAt(Common.Vec2 position, double range = 1.0)
			{
				List<Food> food = new List<Food>();

				foreach (Food foodPiece in this.food)
				{
					if (foodPiece.Position.DistanceTo(position) <= range)
						food.Add(foodPiece);
				}

				return food;
			}

			//See previous method for explanation as to how this works (it's almost identical)
			public List<Nest> getNestsAt(Common.Vec2 position, double range = 1.0)
			{
				List<Nest> nests = new List<Nest>();

				foreach (Nest nest in this.nests)
				{
					if (nest.Position.DistanceTo(position) <= range)
						nests.Add(nest);
				}

				return nests;
			}

			public int AntCount //Public getter for the number of ants in the simulation
			{
				get { return this.ants.Count; }
			}

			public Ant getAnt(int index) //Find a specific ant in the list
			{
				return this.ants[index];
			}

			public int FoodCount //Almost the same as property two above
			{
				get { return this.food.Count; }
			}

			public Food getFood(int index) //Almost the same as method two above
			{
				return this.food[index];
			}

			public int NestCount //Almost the same as property two above
			{
				get { return this.nests.Count; }
			}

			public Nest getNest(int index) //Almost the same as method two above
			{
				return this.nests[index];
			}
        }
    }
}