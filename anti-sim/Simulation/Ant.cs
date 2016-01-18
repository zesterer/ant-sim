using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Simulation
    {
		enum AntState
		{
			RANDOM_WALK = 0,
			COLLECT_FOOD = 1,
			RETURN_FOOD = 2,
		}

		struct AntKnowledge
		{
			public bool KnowsFood;
			public bool KnowsNest;
			public int FoodVisitTime;
			public int NestVisitTime;
			public Common.Vec2 FoodPosition;
			public Common.Vec2 NestPosition;

			public AntKnowledge()
			{
				this.KnowsFood = false;
				this.KnowsNest = false;
				this.FoodVisitTime = 0;
				this.NestVisitTime = 0;
				this.FoodPosition = new Common.Vec2(0, 0);
				this.NestPosition = new Common.Vec2(0, 0);
			}
		}

        class Ant : Entity
        {
            private Common.Vec2 randomWalkDirection;

			private int time = 0;

			private AntState state = AntState.RANDOM_WALK;
			private AntKnowledge knowledge = new AntKnowledge();
			private int foodCargo = 0;

			public Ant(Context parent) : base(parent)
            {
				Console.WriteLine("Created ant");
            }

			public Ant(Context parent, Common.Vec2 position) : base(parent)
			{
				this.position = position;
				Console.WriteLine("Created ant at ({0}, {1})", position.X, position.Y);
			}

			public int FoodCargo
			{
				get { return this.foodCargo; }
			}

			public AntState State
			{
				get { return this.state; }
			}

			public AntKnowledge Knowledge
			{
				get { return this.knowledge; }
			}

			public void ForgetNest()
			{
				this.knowledge.KnowsNest = false;
				this.knowledge.NestPosition = new Common.Vec2(-1, -1);
			}

			public void ForgetFood()
			{
				this.knowledge.KnowsFood = false;
				this.knowledge.FoodPosition = new Common.Vec2(-1, -1);
			}

			public void Tick()
			{
				List<Ant> antsHere = this.parent.getAntsAt(this.Position, 8.0);
				List<Food> foodHere = this.parent.getFoodAt(this.Position, 8.0);
				List<Nest> nestsHere = this.parent.getNestsAt(this.Position, 8.0);

				//Randomly offset motion for the purposes of variation
				if (this.parent.Generator.Next(0, 10) >= 4)
					this.Move(new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2)));

				//Forget the nest or food occasionally
				if (this.parent.Generator.Next(0, 2000) == 0)
					this.ForgetFood();
				if (this.parent.Generator.Next(0, 2000) == 0)
					this.ForgetNest();

				Common.Vec2 net = new Common.Vec2(0);
				int i = 0;
				foreach (Ant ant in antsHere)
				{
					if (ant.Knowledge.FoodPosition == this.Knowledge.FoodPosition && !ant.Knowledge.KnowsFood && this.Knowledge.KnowsFood)
					{
						//Conflicting information! Update if the other one is correct...
						if (ant.Knowledge.FoodVisitTime > this.Knowledge.FoodVisitTime)
						{
							this.knowledge.KnowsFood = ant.Knowledge.KnowsFood;
							this.knowledge.FoodVisitTime = ant.Knowledge.FoodVisitTime;
							Console.WriteLine("Found an ant with more up-to-date info!");

							if (this.knowledge.KnowsFood)
							{
								Console.WriteLine("And they say they've found food!");
							}
							else
							{
								Console.WriteLine("And they say NO FOOD!");
								this.state = AntState.RANDOM_WALK;
							}
						}
					}

					if (ant.Knowledge.NestPosition == this.Knowledge.NestPosition && !ant.Knowledge.KnowsNest && this.Knowledge.KnowsNest)
					{
						//Conflicting information! Update if the other one is correct...
						if (ant.Knowledge.NestVisitTime > this.Knowledge.NestVisitTime)
						{
							this.knowledge.KnowsNest = ant.Knowledge.KnowsNest;
							this.knowledge.NestVisitTime = ant.Knowledge.NestVisitTime;
							Console.WriteLine("Found an ant with more up-to-date info!");

							if (this.knowledge.KnowsNest)
							{
								Console.WriteLine("And they say they've found a nest!");
							}
							else
							{
								Console.WriteLine("And they say NO NEST!");
								this.state = AntState.RANDOM_WALK;
							}
						}
					}

					net += ant.Position;
					i ++;
				}

				//Move towards the average of ants
				if (this.parent.Generator.Next(0, 2) == 0 && this.state == AntState.RANDOM_WALK)
				{
					if ((net / i - this.Position).Magnitude < 2)
						this.Move((this.Position - net / i).Sign);
					//else
						//this.StepTowards(net / i);
				}

                //Do stuff
				switch (this.state)
				{
					case AntState.RANDOM_WALK:

					//Do some walking
					if (this.parent.Generator.Next(0, 30) == 0)
					this.randomWalkDirection = new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2));
					this.Move(this.randomWalkDirection);

					foreach (Ant ant in antsHere)
					{
						if (ant.Knowledge.KnowsFood && ant.Knowledge.FoodPosition != this.Knowledge.FoodPosition)
						{
							this.knowledge.FoodPosition = ant.Knowledge.FoodPosition;
							this.knowledge.KnowsFood = true;
							this.knowledge.FoodVisitTime = ant.Knowledge.FoodVisitTime;
							this.state = AntState.RANDOM_WALK;
						}

						if (ant.Knowledge.KnowsNest && ant.Knowledge.NestPosition != this.Knowledge.NestPosition)
						{
							this.knowledge.NestPosition = ant.Knowledge.NestPosition;
							this.knowledge.KnowsNest = true;
							this.knowledge.NestVisitTime = ant.Knowledge.NestVisitTime;
							this.state = AntState.RANDOM_WALK;
						}
					}

					foreach (Food food in foodHere)
					{
						this.knowledge.FoodPosition = food.Position;
						this.knowledge.KnowsFood = true;
						this.knowledge.FoodVisitTime = this.parent.Time;

						if (this.knowledge.KnowsNest)
							this.state = AntState.COLLECT_FOOD;
						break;
					}

					foreach (Nest nest in nestsHere)
					{
						this.knowledge.KnowsNest = true;
						this.knowledge.NestPosition = nest.Position;
						this.knowledge.KnowsNest = true;
						this.knowledge.NestVisitTime = this.parent.Time;

						if (this.knowledge.KnowsFood)
							this.state = AntState.RETURN_FOOD;
						break;
					}

					if (this.FoodCargo > 0 && this.Knowledge.KnowsNest && this.state == AntState.RANDOM_WALK)
						this.state = AntState.RETURN_FOOD;

					break;

					case AntState.COLLECT_FOOD:
					this.StepTowards(this.knowledge.FoodPosition);

					//Collect some food
					if (this.Position == this.Knowledge.FoodPosition)
					{
						if (foodHere.Count == 0)
						{
							this.knowledge.KnowsFood = false;
							this.knowledge.FoodVisitTime = this.parent.Time;

							if (this.FoodCargo > 0)
								this.state = AntState.RETURN_FOOD;
							else
								this.state = AntState.RANDOM_WALK;
							
							Console.WriteLine("NO FOOD!");
						}
						else
						{
							this.knowledge.KnowsFood = true;
							this.foodCargo += foodHere[0].TakeFood(10);
							this.state = AntState.RETURN_FOOD;
							this.knowledge.FoodVisitTime = this.parent.Time;
						}
					}

					if (!this.Knowledge.KnowsFood)
						this.state = AntState.RANDOM_WALK;
					
					break;

					case AntState.RETURN_FOOD:
					this.StepTowards(this.knowledge.NestPosition);

					//Return the food
					if (this.Position == this.Knowledge.NestPosition)
					{
						if (nestsHere.Count == 0)
						{
							this.knowledge.KnowsNest = false;
							this.knowledge.NestVisitTime = this.parent.Time;

							if (this.FoodCargo <= 0)
								this.state = AntState.COLLECT_FOOD;
							else
								this.state = AntState.RANDOM_WALK;
							
							Console.WriteLine("NO NEST!");
						}
						else
						{
							this.knowledge.KnowsNest = true;
							this.foodCargo -= nestsHere[0].GiveFood(this.FoodCargo);
							this.state = AntState.COLLECT_FOOD;
							this.knowledge.NestVisitTime = this.parent.Time;
						}
					}

					if (!this.Knowledge.KnowsNest)
						this.state = AntState.RANDOM_WALK;

					break;
				}
            }

			public void PostTick()
            {
				this.velocity.X = Math.Max(-1, Math.Min(1, this.velocity.X));
				this.velocity.Y = Math.Max(-1, Math.Min(1, this.velocity.Y));
				this.Position += this.velocity;
				this.velocity = new Common.Vec2(0, 0);

				this.position.X = Math.Max(0, Math.Min(this.parent.World.Size.X - 1, this.position.X));
				this.position.Y = Math.Max(0, Math.Min(this.parent.World.Size.Y - 1, this.position.Y));

                this.time++;
            }
        }
    }
}