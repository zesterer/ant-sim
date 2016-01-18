using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntSim
{
    namespace Simulation
    {
		//Define the states that an ant can be in
		enum AntState
		{
			RANDOM_WALK = 0, //Wandering around randomly
			COLLECT_FOOD = 1, //Moving towards food
			RETURN_FOOD = 2, //Moving towards nest
		}

		//This struct primitive defined the knowlege remembered by each ant
		struct AntKnowledge
		{
			public bool KnowsFood; //Is the position of the food verified?
			public bool KnowsNest; //Is the position of the nest verified?
			public int FoodVisitTime; //When was the food position last verified?
			public int NestVisitTime; //When was the nest position last verified?
			public Common.Vec2 FoodPosition; //What was the position of the verified food?
			public Common.Vec2 NestPosition; //What was the position of the verified nest?

			public AntKnowledge() //Just define a constructor that sets the default values of each
			{
				this.KnowsFood = false;
				this.KnowsNest = false;
				this.FoodVisitTime = 0;
				this.NestVisitTime = 0;
				this.FoodPosition = new Common.Vec2(0, 0);
				this.NestPosition = new Common.Vec2(0, 0);
			}
		}

		//The main ant class. It inherits properties from 'Entity'
        class Ant : Entity
        {
            private Common.Vec2 randomWalkDirection; //Used for the RANDOM_WALK state to determine current wandering direction

			private int time = 0; //The age of the ant. Currently unused (I think? I don't remember using it...) but may be useful in the future.

			private AntState state = AntState.RANDOM_WALK; //The state of the ant. Default is RANDOM_WALK, of course.
			private AntKnowledge knowledge = new AntKnowledge(); //The ant's knowledge
			private int foodCargo = 0; //The amount of food the ant is carrying

			public Ant(Context parent) : base(parent) //The ant constructor. Because 'Entity' has an argumented constructor, this must pass through the arguments with 'base'.
            {
				Console.WriteLine("Created ant"); //Useful for debugging
            }

			public Ant(Context parent, Common.Vec2 position) : base(parent) //A second constructor (in case we wish to place the ant in a specific location)
			{
				this.position = position; //Obvious
				Console.WriteLine("Created ant at ({0}, {1})", position.X, position.Y); //Tell the world where the ant was created. Useful for debugging.
			}

			public int FoodCargo //The public foodcargo property that allows information hiding. GET only.
			{
				get { return this.foodCargo; }
			}

			public AntState State //The public state property that allows informatiom hiding. GET only.
			{
				get { return this.state; }
			}

			public AntKnowledge Knowledge //The public knowledge property that allows informatiom hiding. GET only.
			{
				get { return this.knowledge; }
			}

			public void ForgetNest() //A method that tells the ant to forget the location of the nest
			{
				this.knowledge.KnowsNest = false; //It no longer knows the nest
				this.knowledge.NestPosition = new Common.Vec2(-1, -1); //Set it to an 'impossible' position
			}

			public void ForgetFood() //A method that tells the ant to forget the location of the food
			{
				this.knowledge.KnowsFood = false; //It no longer knows the food
				this.knowledge.FoodPosition = new Common.Vec2(-1, -1); //Set it to an 'impossible' position
			}

			public void Tick() //The method called every simulation tick that defines ant behaviour
			{
				//Get lists of nearby entities
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

				//This bit here totals up the net position of nearby ants (useful for flocking behaviours)
				Common.Vec2 net = new Common.Vec2(0);
				int i = 0;
				foreach (Ant ant in antsHere) //Loop through all nearby ants
				{
					if (ant.Knowledge.FoodPosition == this.Knowledge.FoodPosition && !ant.Knowledge.KnowsFood && this.Knowledge.KnowsFood) //If ant memory information conflicts!
					{
						//Conflicting information! Update if the other one has more recent info...
						if (ant.Knowledge.FoodVisitTime > this.Knowledge.FoodVisitTime)
						{
							//Update memory
							this.knowledge.KnowsFood = ant.Knowledge.KnowsFood;
							this.knowledge.FoodVisitTime = ant.Knowledge.FoodVisitTime;
							Console.WriteLine("Found an ant with more up-to-date info!");

							//Just write this information to the console
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

					//Same as previous code, but for nest position
					if (ant.Knowledge.NestPosition == this.Knowledge.NestPosition && !ant.Knowledge.KnowsNest && this.Knowledge.KnowsNest) //If ant memory information conflicts!
					{
						//Conflicting information! Update if the other one is correct...
						if (ant.Knowledge.NestVisitTime > this.Knowledge.NestVisitTime)
						{
							this.knowledge.KnowsNest = ant.Knowledge.KnowsNest;
							this.knowledge.NestVisitTime = ant.Knowledge.NestVisitTime;
							Console.WriteLine("Found an ant with more up-to-date info!");

							//Just write this information to the console
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

					//This bit totals up the number of nearby ants and their net position
					net += ant.Position;
					i ++;
				}

				//Move away from the average of nearby ant positions (uses net ant position collected previously)...
				if (this.parent.Generator.Next(0, 2) == 0 && this.state == AntState.RANDOM_WALK)
				{
					if ((net / i - this.Position).Magnitude < 2) //...but only if they are closer than a certain value
						this.Move((this.Position - net / i).Sign); //Use integer division (see Common.Vec2 primitive)
					//else
						//this.StepTowards(net / i); //Unimplemented for now
				}

                //Change behaviour depending on the ant's state
				switch (this.state)
				{
					case AntState.RANDOM_WALK: //If we're in the RANDOM_WALK state...

					//...do some walking
					if (this.parent.Generator.Next(0, 30) == 0) //Random dice roll, 1 in 30 chance...
						this.randomWalkDirection = new Common.Vec2(this.parent.Generator.Next(-1, 2), this.parent.Generator.Next(-1, 2)); //..start moving in another direction
					this.Move(this.randomWalkDirection); //Move in the direction specified above

					foreach (Ant ant in antsHere) //Loop through nearby ants
					{
						if (ant.Knowledge.KnowsFood && ant.Knowledge.FoodPosition != this.Knowledge.FoodPosition) //Do they have information about food?
						{
							//Update this ant's information
							this.knowledge.FoodPosition = ant.Knowledge.FoodPosition;
							this.knowledge.KnowsFood = true;
							this.knowledge.FoodVisitTime = ant.Knowledge.FoodVisitTime;
							this.state = AntState.RANDOM_WALK;
						}

						if (ant.Knowledge.KnowsNest && ant.Knowledge.NestPosition != this.Knowledge.NestPosition) //Do they have information about nests?
						{
							//Update this ant's information
							this.knowledge.NestPosition = ant.Knowledge.NestPosition;
							this.knowledge.KnowsNest = true;
							this.knowledge.NestVisitTime = ant.Knowledge.NestVisitTime;
							this.state = AntState.RANDOM_WALK;
						}
					}

					foreach (Food food in foodHere) //Loop through nearby food
					{
						//Update information, overwrite knowledge of old food (leads to more optimal food collection)
						this.knowledge.FoodPosition = food.Position;
						this.knowledge.KnowsFood = true;
						this.knowledge.FoodVisitTime = this.parent.Time;

						if (this.knowledge.KnowsNest)
							this.state = AntState.COLLECT_FOOD;
						break; //That's enough. Once we find one piece of food, we don't need to find any more
					}

					foreach (Nest nest in nestsHere) //Loops through nearby food
					{
						//Update information, overwrite knowledge of old nest (leads to more optimal food collection)
						this.knowledge.KnowsNest = true;
						this.knowledge.NestPosition = nest.Position;
						this.knowledge.KnowsNest = true;
						this.knowledge.NestVisitTime = this.parent.Time;

						if (this.knowledge.KnowsFood)
							this.state = AntState.RETURN_FOOD;
						break; //That's enough. Once we find one nest, we don't need to find any more
					}

					if (this.FoodCargo > 0 && this.Knowledge.KnowsNest && this.state == AntState.RANDOM_WALK) //If all the conditions for switching to a return food state are met, do so!
						this.state = AntState.RETURN_FOOD;

					break; //End of RANDOM_WALK case

					case AntState.COLLECT_FOOD: //If we're in the COLLECT_FOOD state...
					
					this.StepTowards(this.knowledge.FoodPosition); //Move towards food, as you'd expect

					//Collect some food
					if (this.Position == this.Knowledge.FoodPosition) //If we're in the right place for the food
					{
						if (foodHere.Count == 0) //If there's no food, realise that they were incorrect and update timestamp info
						{
							this.knowledge.KnowsFood = false;
							this.knowledge.FoodVisitTime = this.parent.Time;

							if (this.FoodCargo > 0)
								this.state = AntState.RETURN_FOOD;
							else
								this.state = AntState.RANDOM_WALK;
							
							Console.WriteLine("NO FOOD!");
						}
						else //Take some food from the food object
						{
							this.knowledge.KnowsFood = true;
							this.foodCargo += foodHere[0].TakeFood(10);
							this.state = AntState.RETURN_FOOD;
							this.knowledge.FoodVisitTime = this.parent.Time;
						}
					}

					//If we no longer know the food location, then return to a random walk state.
					if (!this.Knowledge.KnowsFood)
						this.state = AntState.RANDOM_WALK;
					
					break; //End of COLLECT_FOOD case

					case AntState.RETURN_FOOD: //If we're in the RETURN_FOOD state...
					
					this.StepTowards(this.knowledge.NestPosition); //Move towards a nest, as you'd expect

					//Return the food
					if (this.Position == this.Knowledge.NestPosition) //If we're in the right place for the nest
					{
						if (nestsHere.Count == 0) //If there's no nest, realise that they were incorrect and update timestamp info
						{
							this.knowledge.KnowsNest = false;
							this.knowledge.NestVisitTime = this.parent.Time;

							if (this.FoodCargo <= 0)
								this.state = AntState.COLLECT_FOOD;
							else
								this.state = AntState.RANDOM_WALK;
							
							Console.WriteLine("NO NEST!");
						}
						else //Return this food to the nest
						{
							this.knowledge.KnowsNest = true;
							this.foodCargo -= nestsHere[0].GiveFood(this.FoodCargo);
							this.state = AntState.COLLECT_FOOD;
							this.knowledge.NestVisitTime = this.parent.Time;
						}
					}

					//If we no longer know the nest location, then return to a random walk state.
					if (!this.Knowledge.KnowsNest)
						this.state = AntState.RANDOM_WALK;

					break; //End of RETURN_FOOD case
				}
            }

			//Things that are done after the tick event
			public void PostTick()
            {
				//The 'Move' method uses a velocity value that prevents the ant moving more than 1 pixel in either direction per tick
				this.velocity.X = Math.Max(-1, Math.Min(1, this.velocity.X)); //Restrict the velocity to the Moore neighborhood
				this.velocity.Y = Math.Max(-1, Math.Min(1, this.velocity.Y)); //   "      "      "     "  "    "        "
				this.Position += this.velocity; //Update the position based on the now-restricted velocitu
				this.velocity = new Common.Vec2(0, 0); //Reset the velocity ready for the nest tick

				this.position.X = Math.Max(0, Math.Min(this.parent.World.Size.X - 1, this.position.X)); //Restrict the X position to within the world size
				this.position.Y = Math.Max(0, Math.Min(this.parent.World.Size.Y - 1, this.position.Y)); //Restrict the Y position to within the world size

                this.time ++; //Update the age of the ant
            }
        }
    }
}