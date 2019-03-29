using Scissors.Xaf.CacheWarmup.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Xaf.CacheWarmup.Generators.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var finder = new AttributeFinder();
            var assemblyPath = @"C:\F\github\how-to-precache-an-xaf-winforms-application\src\how_to_precache_an_xaf_winforms_application.Win\bin\Debug\how_to_precache_an_xaf_winforms_application.Win.exe";
            var foundType = finder.FindAttribute(assemblyPath);

            Console.WriteLine(foundType);

            if(foundType != null)
            {
                var cacheGenerator = new CacheWarmupGenerator();

                var cacheResult = cacheGenerator.WarmupCache(assemblyPath, foundType);
                if(cacheResult != null)
                {
                    
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
