using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scissors.Xaf.CacheWarmup.Generators.MsBuild
{
    public class XafCacheWarmupTask : Task
    {
        [Required]
        public string ApplicationPath { get; set; }

        public override bool Execute()
        {
            Console.WriteLine($"ApplicationPath: {ApplicationPath}");

            var finder = new AttributeFinder();
            var assemblyPath = ApplicationPath;
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
