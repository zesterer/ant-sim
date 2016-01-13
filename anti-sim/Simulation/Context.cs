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
        class Context
        {
			private int time = 0;
            private World world;
            private List<Ant> ants = new List<Ant>();
			private List<Nest> nests = new List<Nest>();
			private List<Food> food = new List<Food>();

			private Random generator = new Random(4321);

            public Context()
            {
				this.world = new World();
				this.world.Setup(16, 16, 16);

                //Create a few test ants
				for (int i = 0; i < 500; i++)
				{
					Ant ant = new Ant(this);
					ant.PlaceRandomly();
					this.ants.Add(ant);
				}

				//Create a few test nests
				for (int i = 0; i < 4; i++)
				{
					Nest nest = new Nest();
					nest.PlaceRandomly(this.world, this.generator);
					this.nests.Add(nest);
				}

				//Create a few pieces of test food
				for (int i = 0; i < 0; i++)
				{
					Food food = new Food(this);
					food.PlaceRandomly();
					this.food.Add(food);
				}
            }

			public Random Generator
			{
				get { return this.generator; }
				set { this.generator = value; }
			}

			public World World
			{
				get { return this.world; }
				set { this.world = value; }
			}

			public int Time
			{
				get { return this.time; }
			}

            public void Tick()
            {
                //Reset the world
                this.world.Reset();

                //Seed the ants within the world
                foreach (Ant ant in this.ants)
                    this.world.AddAnt(ant);

				//Check the food
				int i = 0;
				while (i < this.food.Count)
				{
					if (this.food[i].FoodCargo <= 0)
					{
						this.food.RemoveAt(i);
						Console.WriteLine("Food consumed");
					}
					else
						i ++;
				}

                //Tick the ants
                foreach (Ant ant in this.ants)
					ant.Tick();

                //Finish the tick for the ants
                foreach (Ant ant in this.ants)
					ant.PostTick();

                //Tick the world
                this.world.Tick();
				this.time ++;
            }

			public void createFoodAt(Common.Vec2 position)
			{
				Food food = new Food(this);
				food.Position = position;
				this.food.Add(food);
			}

			public void createAntAt(Common.Vec2 position)
			{
				Ant ant = new Ant(this);
				ant.Position = position;
				this.ants.Add(ant);
			}

			public List<Ant> getAntsAt(Common.Vec2 position, double range = 1.0)
			{
				List<Ant> ants = new List<Ant>();

				foreach (Ant ant in this.ants)
				{
					if (ant.Position.DistanceTo(position) <= range)
						ants.Add(ant);
				}

				return ants;
			}

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

			public int AntCount
			{
				get { return this.ants.Count; }
			}

			public Ant getAnt(int index)
			{
				return this.ants[index];
			}

			public int FoodCount
			{
				get { return this.food.Count; }
			}

			public Food getFood(int index)
			{
				return this.food[index];
			}

			public int NestCount
			{
				get { return this.nests.Count; }
			}

			public Nest getNest(int index)
			{
				return this.nests[index];
			}
        }
    }
}