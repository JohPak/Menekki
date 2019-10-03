using System;
// to write/read files:
using System.IO;
// to use lists
using System.Collections.Generic;

namespace Menekki_0._3 //3.10.2019
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //read components from file
            Components AllComponents = new Components();

            Console.WriteLine("Lisätäänkö komponentti? (k) ");
            string  vastaus = Console.ReadLine();

            if (vastaus == "k")
            {
                SingleComponent komponentti = new SingleComponent();
            }


            
        }
    }
}
