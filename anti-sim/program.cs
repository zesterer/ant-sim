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
            Frontend.Application app = new Frontend.Application();

            app.Run();

            return 0;
        }
    }
}
