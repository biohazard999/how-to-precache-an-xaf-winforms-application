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
            Console.WriteLine("Hello World");
            return true;
        }
    }
}
