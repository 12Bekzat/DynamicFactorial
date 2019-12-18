using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DynamicFactorial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToCountFactorials(object sender, RoutedEventArgs e)
        {
            LoadAssembly(numbers.Text);
            // очистка
            GC.Collect();
            GC.WaitForPendingFinalizers();

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                Console.WriteLine(asm.GetName().Name);

            Console.Read();
        }

        private static void LoadAssembly(string nums)
        {

            var context = new CustomAssemblyLoadContext();
            context.Unloading += ContextUnloading;

            Assembly assembly = context.LoadFromAssemblyPath(@"C:\Users\толеутайб\source\repos\DynamicFactorial\DynamicFactorial\DynamicFactorial\bin\Debug\netcoreapp3.0\ToCountFactorial\ToCountFactorial\bin\Debug\netcoreapp3.0\ToCountFactorial.dll");

            var type = assembly.GetType("ToCountFactorial.Program");

            var greetMethod = type.GetMethod("ToFactorial");

            string numFactorials = String.Empty;
            string[] tokens = nums.Split(',');
            for (int i = 0; i < tokens.Length; i++)
            {
                if (int.TryParse(tokens[i], out var number))
                {
                    var instance = Activator.CreateInstance(type);
                    int result = (int)greetMethod.Invoke(instance, new object[] { number });
                    numFactorials += result.ToString();
                    numFactorials += '\n';
                }
            }
            MessageBox.Show(numFactorials);

            context.Unload();
        }
        private static void ContextUnloading(AssemblyLoadContext obj)
        {
            Console.WriteLine("Библиотека ToCountFactorial выгружена \n");
        }
    }
}
