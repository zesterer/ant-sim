using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace anti_sim
{
    class Program
    {
        static int Main(string[] args)
        {
            Application app = new Application();

			while (Console.ReadKey().Key != ConsoleKey.Q)
			{
				Console.WriteLine("Ticking...");

				app.Tick();

				Thread.Sleep(1000);
			}

            return 0;
        }
    }
}
