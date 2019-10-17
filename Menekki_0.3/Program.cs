using System;
// to write/read files:
using System.IO;
// to use lists
using System.Collections.Generic;

namespace Menekki_0._3 //8.10.2019
{
    class Program
    {
        static void Main(string[] args)
        {
            // helper variable to loop menu
            bool loopMenu = true;

            //read components from file
            Components AllComponents = new Components();

            while (loopMenu)
            {
                // print menu
                Console.WriteLine("**** MENEKKI v0.03 ****");
                Console.WriteLine("1. Komponentit");
                Console.WriteLine("2. Laitteet");
                Console.WriteLine("3. Poistu");
                Console.Write("> ");

                //user menu choice
                var choice = Console.ReadKey();
                Console.WriteLine();

                // check user menu choice
                switch (choice.Key)
                {

//MENU ACTIONS START HERE 
/*****************************************************/

        case ConsoleKey.D1: // COMPONENTS
            Console.Clear();
            ConsoleKey subChoice;

            // keeps looping submenu until user exits to main menu
            do
            {
                //MAIN MENU
                Console.WriteLine("KOMPONENTIT");
                Console.WriteLine("1. Listaa");
                Console.WriteLine("2. Lisää uusi");
                Console.WriteLine("3. Poista");
                Console.WriteLine("4. Muokkaa");
                Console.WriteLine("5. Päävalikkoon");


                subChoice = Console.ReadKey().Key;

                switch (subChoice)
                {
                    case ConsoleKey.D1:
                            Console.Clear();
                            Console.WriteLine("Listataan komponentit");
                            Console.WriteLine();
                            AllComponents.ListComponents();
                            Console.WriteLine("\n");
                        break;
                    case ConsoleKey.D2:
                            Console.Clear();
                            Console.WriteLine("Lisätään komponentti");
                            AllComponents.NewComponent();
                            Console.WriteLine("\n");
                        break;
                    case ConsoleKey.D3:
                            Console.Clear();
                            Console.WriteLine("Poista komponentti");
                            AllComponents.DeleteComponent();
                        break;
                    case ConsoleKey.D4:
                            Console.Clear();
                            Console.WriteLine("Valitse muokattava komponentti (id) ");
                            Console.WriteLine("\n");
                            AllComponents.ListComponents();
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (subChoice != ConsoleKey.D5);

            break;


/*****************************************************/

        case ConsoleKey.D2: // PRODUCTS
            Console.Clear();
            Console.WriteLine("LAITTEET");
            Console.WriteLine("1. Listaa");
            Console.WriteLine("2. Lisää uusi");
            Console.WriteLine("3. Poista");
            Console.WriteLine("4. Päävalikkoon");

            /*subChoice = int.Parse(Console.ReadLine());
            switch (subChoice)
            {
                case 1:
                    Console.WriteLine("Listataan laitteet");
                    //ListDevice();
                    break;
                case 2:
                    Console.WriteLine("Lisätään uusi laite");
                    //AddDevice();
                    break;
                case 3:
                    Console.WriteLine("Poista laite");
                    //DelDevice();
                    break;
                case 4:
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    break;
            }*/
            break;



        default: // QUIT PROGRAM
            Console.WriteLine("Heippa!");
            loopMenu = false;
            // closes the console window on Windows
            Environment.Exit(-1);
            
            break;


                }
            }
        }
    }
}

