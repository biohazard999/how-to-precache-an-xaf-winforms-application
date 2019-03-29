using AppDomainToolkit;
using System;
using System.IO;
using System.Linq;
using Fasterflect;

namespace Scissors.Xaf.CacheWarmup.Generators
{
    public class CacheWarmupGenerator
    {
        private const string GetDcAssemblyFilePath = "GetDcAssemblyFilePath";
        private const string GetModelAssemblyFilePath = "GetModelAssemblyFilePath";
        private const string GetModelCacheFileLocationPath = "GetModelCacheFileLocationPath";
        private const string GetModulesVersionInfoFilePath = "GetModulesVersionInfoFilePath";

        public CacheWarmupGeneratorResponse WarmupCache(string assemblyPath, string xafApplicationTypeName)
        {
            using (var context = AppDomainContext.Create(new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                PrivateBinPath = Path.GetDirectoryName(assemblyPath),
                ConfigurationFile = $"{assemblyPath}.config"
            }))
            {
                context.LoadAssemblyWithReferences(LoadMethod.LoadFile, assemblyPath);
                return RemoteFunc.Invoke(context.Domain, new CacheWarmupGeneratorRequest
                {
                    AssemblyPath = assemblyPath,
                    XafApplicationTypeName = xafApplicationTypeName,
                },
                (args) =>
                {
                    var assembly = AppDomain.CurrentDomain.GetAssemblies().First(b => b.Location == args.AssemblyPath);
                    var applicationType = assembly.GetType(args.XafApplicationTypeName);

                    if(applicationType != null)
                    {
                        using (var xafApplication = (System.IDisposable)applicationType.CreateInstance())
                        {
                            xafApplication.SetPropertyValue("SplashScreen", null);
                            xafApplication.SetPropertyValue("DatabaseUpdateMode", 0);

                            xafApplication.CallMethod("Setup");

                            var dcAssemblyFilePath = (string)xafApplication.CallMethod(GetDcAssemblyFilePath);
                            var modelAssemblyFilePath = (string)xafApplication.CallMethod(GetModelAssemblyFilePath);
                            var modelCacheFileLocationPath = (string)xafApplication.CallMethod(GetModelCacheFileLocationPath);
                            var modulesVersionInfoFilePath = (string)xafApplication.CallMethod(GetModulesVersionInfoFilePath);

                            return new CacheWarmupGeneratorResponse
                            {
                                DcAssemblyFilePath = dcAssemblyFilePath,
                                ModelAssemblyFilePath = modelAssemblyFilePath,
                                ModelCacheFilePath = modelCacheFileLocationPath,
                                ModulesVersionInfoFilePath = modulesVersionInfoFilePath
                            };
                        }
                    }
                    
                    return null;
                });
            }
        }

        [Serializable]
        public class CacheWarmupGeneratorRequest
        {
            public string XafApplicationTypeName { get; set; }
            public string AssemblyPath { get; set; }
        }

        [Serializable]
        public class CacheWarmupGeneratorResponse
        {
            public string DcAssemblyFilePath { get; set; }
            public string ModelAssemblyFilePath { get; set; }
            public string ModelCacheFilePath { get; set; }
            public string ModulesVersionInfoFilePath { get; set; }
        }
    }
}
