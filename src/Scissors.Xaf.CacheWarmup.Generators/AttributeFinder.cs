using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainToolkit;
using Scissors.Xaf.CacheWarmup.Attributes;
using static System.Console;

namespace Scissors.Xaf.CacheWarmup.Generators
{
    public class AttributeFinder
    {
        public string FindAttribute(string assemblyPath)
        {
            using (var context = AppDomainContext.Create(new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.GetDirectoryName(assemblyPath),
            }))
            {
                context.LoadAssemblyWithReferences(LoadMethod.LoadFile, assemblyPath);
                return RemoteFunc.Invoke(context.Domain, new AttributeFinderRequest { AssemblyPath = assemblyPath, AttributeType = typeof(XafCacheWarmupAttribute) }, (args) =>
                {
                    WriteLine($"Try to find {nameof(XafCacheWarmupAttribute)} in {args.AssemblyPath}");
                    var assembly = AppDomain.CurrentDomain.GetAssemblies().First(b => b.Location == args.AssemblyPath);

                    var attribute = assembly.GetCustomAttributes(false).OfType<XafCacheWarmupAttribute>().FirstOrDefault();
                    if(attribute != null)
                    {
                        WriteLine($"Found {nameof(XafCacheWarmupAttribute)} with '{attribute.XafApplicationType.FullName}'");
                        return attribute.XafApplicationType.FullName;
                    }
                    return null;
                });
            }
        }

        [Serializable]
        public class AttributeFinderRequest
        {
            public string AssemblyPath { get; set; }
            public Type AttributeType { get; set; }
        }
    }
}
