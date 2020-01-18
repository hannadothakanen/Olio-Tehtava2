using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtävä2 // Hanna Hakanen 2019
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Application.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.Write("Paina Enter lopettaaksesi...");
                Console.ReadLine();
            }
        }
    }
}
