using System;
// to write/read files:
using System.IO;
// to use lists
using System.Collections.Generic;
// to use to check if list is empty
using System.Linq;


namespace Menekki_0._3
{
    public class Components
    {
        //THIS CLASS IS TO KEEP A LIST OF ALL COMPONENTS IN STOCK
        // and this is the list:
        List<SingleComponent> ComponentList = new List<SingleComponent>();


        //CONSTRUCTOR
        public Components()
        {
            // check if ComponentList is empty
            if (!ComponentList.Any())
            {
                // if empty, insert data from file
                ReadComponent(ComponentList);
            }
        }

        //METHODS
        //READ COMPONENTS FROM FILE
        public static void ReadComponent(List<SingleComponent> ComponentList)
        { 
            // helper for file reading
            string line;
            // open the file to read components from
            StreamReader file = new StreamReader($"komponentit.txt");
            // read all lines from file
            while ((line = file.ReadLine()) != null)
            {
                //make new SingleComponent of each line
                ComponentList.Add(new SingleComponent(line));
            }
            
            
        }
        public void ListComponents()
        {
            foreach (var c in ComponentList)
            {
                //c.Name = "jee";
                //string nimi = c.Name;

                Console.WriteLine($"ID {c.Id}, {c.Name}, {c.Pcs} kpl, á {c.Price.ToString("F")} € (yht. {(c.Pcs*c.Price).ToString("F")} €)");
            }
        }

        public void NewComponent()
        {
            //creates NEW component into the list
            ComponentList.Add(new SingleComponent());
        }

        public void Worth()
        {
            double sum = 0;
            foreach (var c in ComponentList)
            {
                sum += c.Price * c.Pcs;
            }
            Console.Write($"Varaston arvo {sum.ToString("F")} €");
        }
        
    }
}
