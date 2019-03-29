using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scissors.Xaf.CacheWarmup.Generators.MsBuild
{
    public class XafCacheWarmupTask : Task
    {
        public override bool Execute()
        {
            var finder = new AttributeFinder();
            var assemblyPath = @"C:\F\github\how-to-precache-an-xaf-winforms-application\src\how_to_precache_an_xaf_winforms_application.Win\bin\Debug\how_to_precache_an_xaf_winforms_application.Win.exe";
            var foundType = finder.FindAttribute(assemblyPath);

            Console.WriteLine(foundType);

            if (foundType != null)
            {
                var cacheGenerator = new CacheWarmupGenerator();

                var cacheResult = cacheGenerator.WarmupCache(assemblyPath, foundType);
                if (cacheResult != null)
                {
                    Console.WriteLine("Done");
                    return true;
                }
            }

            return false;
        }
    }
}
