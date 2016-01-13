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
			public bool VisitedFood;
			public bool KnowsNest;
			public bool VisitedNest;
			public Common.Vec2 FoodPosition;
			public Common.Vec2 NestPosition;

			public AntKnowledge(bool knowsFood, bool visitedFood, bool knowsNest, bool visitedNest, Common.Vec2 foodPosition, Common.Vec2 nestPosition)
			{
				this.KnowsFood = knowsFood;
				this.VisitedFood = visitedFood;
				this.KnowsNest = knowsNest;
				this.VisitedNest = visitedNest;
				this.FoodPosition = foodPosition;
				this.NestPosition = nestPosition;
			}
		}

        class Ant : Entity
        {
            private Common.Vec2 velocity;
			private Common.Vec2 randomWalkDirection;

			private int time = 0;

			private AntState state = AntState.RANDOM_WALK;
			private AntKnowledge knowledge = new AntKnowledge(false, false, false, false, new Common.Vec2(0, 0), new Common.Vec2(0, 0));
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

			public AntState State
			{
				get { return this.state; }
			}

			public AntKnowledge Knowledge
			{
				get { return this.knowledge; }
			}

            void Move(Common.Vec2 vec)
            {
                this.velocity += vec;
            }

			//I was going to hack together an implementation of Bresenham's algorithm, but
			//if this project required proper movement planning, I'd have used a continuous
			//world with communication distance thresholds instead.
			public void StepTowards(Common.Vec2 target)
			{
				Common.Vec2 relative = target - this.Position;
				int dimensionalMax = Math.Max(Math.Abs(relative.X), Math.Abs(relative.Y));

				if (dimensionalMax > 0)
					this.Move(new Common.Vec2(relative.X, relative.Y) / dimensionalMax);
			}

			public void Tick()
            {
				if (this.parent.Generator == null)
					this.parent.Generator = new Random(142857);

				//Select the correct state
				if (this.state == AntState.COLLECT_FOOD)
				{
					if (!this.Knowledge.KnowsFood)
						this.state = AntState.RANDOM_WALK;
				}
				else if (this.state == AntState.RETURN_FOOD)
				{
					if (!this.Knowledge.KnowsNest)
						this.state = AntState.RANDOM_WALK;
				}

				List<Ant> antsHere = this.parent.getAntsAt(this.Position);
				List<Food> foodHere = this.parent.getFoodAt(this.Position);
				List<Nest> nestsHere = this.parent.getNestsAt(this.Position);

                //Do stuff
				switch (this.state)
				{
					case AntState.RANDOM_WALK:

						//Do some walking
					if (this.parent.Generator.Next(0, 30) == 0)
						this.randomWalkDirection = new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2));
						this.Move(this.randomWalkDirection);
					this.Move(new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2)));

						foreach (Ant ant in antsHere)
						{
							if (ant.Knowledge.KnowsFood && !this.Knowledge.KnowsFood)
							{
								this.knowledge.FoodPosition = ant.Knowledge.FoodPosition;
								this.knowledge.KnowsFood = true;
								this.state = AntState.RETURN_FOOD;
							}

							if (ant.Knowledge.KnowsNest && !this.Knowledge.KnowsNest)
							{
								this.knowledge.NestPosition = ant.Knowledge.NestPosition;
								this.knowledge.KnowsNest = true;
								this.state = AntState.COLLECT_FOOD;
							}
						}

						foreach (Food food in foodHere)
						{
							this.knowledge.FoodPosition = food.Position;
							this.knowledge.KnowsFood = true;
							this.state = AntState.RETURN_FOOD;
							break;
						}

						foreach (Nest nest in nestsHere)
						{
							this.knowledge.KnowsNest = true;
							this.knowledge.NestPosition = nest.Position;
							this.knowledge.KnowsNest = true;
							this.state = AntState.COLLECT_FOOD;
							break;
						}

						break;

					case AntState.COLLECT_FOOD:
						this.StepTowards(this.knowledge.FoodPosition);
					this.Move(new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2)));

						//Collect some food
						if (this.Position == this.Knowledge.FoodPosition)
						{
							if (foodHere.Count == 0)
								this.knowledge.KnowsFood = false;
							else
							{
								this.knowledge.KnowsFood = true;
								foodHere[0].FoodCargo -= 100;
								this.foodCargo += 100;
								this.state = AntState.RETURN_FOOD;
							}
						}
					
						break;

					case AntState.RETURN_FOOD:
						this.StepTowards(this.knowledge.NestPosition);
					this.Move(new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2)));

						//Return the food
						if (this.Position == this.Knowledge.NestPosition)
						{
							if (nestsHere.Count == 0)
								this.knowledge.KnowsNest = false;
							else
							{
								nestsHere[0].FoodCargo += this.foodCargo;
								this.foodCargo = 0;
								this.state = AntState.COLLECT_FOOD;
							}
						}

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