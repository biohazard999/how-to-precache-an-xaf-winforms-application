using System;

namespace Scissors.Xaf.CacheWarmup.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class XafCacheWarmupAttribute : Attribute
    {
        public Type XafApplicationType { get; }

        public XafCacheWarmupAttribute(Type xafApplicationType)
        {
            XafApplicationType = xafApplicationType;
        }
    }
}
