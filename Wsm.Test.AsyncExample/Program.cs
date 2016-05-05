using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.Test.AsyncExample
{
    class Program
    {

        public static void Main(string[] args)
        {


            Test();


            Console.WriteLine("Enter input:"); // Prompt
            string line = Console.ReadLine(); // Get string from user

            System.Diagnostics.Debug.Write("rofl");

        }

    public static async void Test()
        {
            execute(1);
          
            execute(3);

            System.Diagnostics.Debug.Write("rofl");

        }


        public static async Task execute(int i)
        {
           await Task.Run(() =>
            {
                while (true) {
                    System.Diagnostics.Debug.Write(i);
                    System.Threading.Thread.Sleep(1000 * i);
                }
          

            });
        }


    }
}
