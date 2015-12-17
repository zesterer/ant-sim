using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AntSim
{
    class Program
    {
        static int Main(string[] args)
        {
            Simulation.Application app = new Simulation.Application();

			while (true)
			{
				Console.WriteLine("Ticking...");

				app.Tick();

				Thread.Sleep(1000);
			}

            return 0;
        }
    }
}
