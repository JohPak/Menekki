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

            //read devices from file
            Devices AllDevices = new Devices(AllComponents);

            while (loopMenu)
            {
                // MAIN MENU
                Console.WriteLine("**** MENEKKI v0.03 ****");
                Console.WriteLine("1. Komponentit");
                Console.WriteLine("2. Laitteet");
                Console.WriteLine("3. Poistu");
                Console.Write("> ");

                // user menu choice
                var choice = Console.ReadKey();
                Console.WriteLine();

                // check user menu choice
                switch (choice.Key)
                {

//COMPONENTS MENU
/*****************************************************/

                    case ConsoleKey.D1: // COMPONENTS
                    Console.Clear();
                    ConsoleKey subChoice;

                    // keeps looping submenu until user exits to main menu
                    do
                    {
                        //COMPONENT MENU
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
                                    AllComponents.Worth();
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
                                    Console.WriteLine("Muokkaa komponenttia\n");
                                    AllComponents.EditComponent();
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

//DEVICES MENU
/*****************************************************/

                    case ConsoleKey.D2:
                    Console.Clear();

                    do
                    {

                        // DEVICE MENU
                        Console.WriteLine("LAITTEET");
                        Console.WriteLine("1. Listaa");
                        Console.WriteLine("2. Lisää uusi");
                        Console.WriteLine("3. Poista");
                        Console.WriteLine("4. Rakenna laite");
                        Console.WriteLine("5. Päävalikkoon");

                        subChoice = Console.ReadKey().Key;
                        switch (subChoice)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                Console.WriteLine("Listataan laitteet");
                                Console.WriteLine();
                                AllDevices.ListDevices();
                                break;
                            case ConsoleKey.D2:
                                Console.Clear();
                                Console.WriteLine("Lisätään laite");
                                AllDevices.NewDevice();
                                Console.WriteLine("\n");
                                break;
                            case ConsoleKey.D3:
                                Console.WriteLine("Poista laite");
                                    AllDevices.DeleteDevice();
                                break;
                            case ConsoleKey.D4:
                                    Console.Clear();
                                Console.WriteLine("\nRakennetaan laite");
                                    AllDevices.BuildDevice();
                                break;
                            case ConsoleKey.D5:
                                Console.Clear();
                                break;
                                default:
                                Console.Clear();
                                break;
                        }
                        break;

                    } while (subChoice != ConsoleKey.D5);
                    break;

                    default: // QUIT PROGRAM
                    Console.WriteLine("Heippa!");
                    loopMenu = false;
                    // closes the console window on Windows
                    Environment.Exit(0);
            
                    break;


                }
            }
        }
    }
}

