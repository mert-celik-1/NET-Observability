using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracing_ConsoleApp
{
    internal class ServiceHelper
    {
        internal async Task Work1()
        {
            using var activity = ActivitySourceProvider.Source.StartActivity();

            var serviceOne = new ServiceOne();

            Console.WriteLine($"Google length : {await serviceOne.MakeRequestToGoogle()}");
            Console.WriteLine("Work 1 tamamlandı");

        }
    }
}
