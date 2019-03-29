using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainToolkit;
using Scissors.Xaf.CacheWarmup.Attributes;

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
                    var assembly = AppDomain.CurrentDomain.GetAssemblies().First(b => b.Location == args.AssemblyPath);

                    var attribute = assembly.GetCustomAttributes(false).OfType<XafCacheWarmupAttribute>().FirstOrDefault();
                    if(attribute != null)
                    {
                        return attribute.XafApplicationType.AssemblyQualifiedName;
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
