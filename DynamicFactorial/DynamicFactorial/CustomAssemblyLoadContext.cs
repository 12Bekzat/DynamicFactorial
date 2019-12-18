using System.Reflection;
using System.Runtime.Loader;

namespace DynamicFactorial
{
    public partial class MainWindow
    {
        public class CustomAssemblyLoadContext : AssemblyLoadContext
        {
            public CustomAssemblyLoadContext() : base(isCollectible: true) { }

            protected override Assembly Load(AssemblyName assemblyName)
            {
                return null;
            }
        }
    }
}
