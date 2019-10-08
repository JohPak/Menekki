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
                // print out list of items like this:
                // Id  10. prikka...........55 kpl, á 0.25 € (yht. 13.75 €)
                Console.WriteLine($"Id {c.CompId,3}. {c.Name.PadRight(20, '.')} {c.Pcs,3} kpl, á {c.Price.ToString("F"),6} € (yht. {(c.Pcs*c.Price).ToString("F"),7} €)");
            }
        }

        public void NewComponent()
        {
            //creates NEW component into the list
            ComponentList.Add(new SingleComponent());
        }

        public void DeleteComponent(int idUserWantsToDelete)
        {
            // i found out deleting instances is tricky thing to do, so i do this instead:
            // if user input matches component's id
            // create a text line of that component (as in komponentit.txt)
            // move all lines in temporary file, except the one to be removed
            // then save the temporary file back in komponentit.txt
            string lineToRemove = "";
            foreach (var c in ComponentList)
            {
                if (idUserWantsToDelete == c.CompId)
                {
                    lineToRemove = $"{c.CompId} {c.Name} {c.Pcs} {c.Price.ToString("F")}";
                    var tempFile = Path.GetTempFileName();
                    var linesToKeep = File.ReadLines($"komponentit.txt").Where(l => l != lineToRemove);
                    File.WriteAllLines(tempFile, linesToKeep);
                    File.Delete($"komponentit.txt");
                    File.Move(tempFile, $"komponentit.txt");
                    Console.WriteLine($"---> Id: {lineToRemove} on poistettu.\n");
                }
                //else if (idUserWantsToDelete != c.CompId && lineToRemove == "")
                //{
                //    //TÄSSÄ ON JOTAKI MÄTÄÄ
                //    Console.WriteLine("Antamasi id-numero ei täsmää.\n");
                //    break;
                //}
            }
            // Build ComponentList again to make sure to have data up to date
            ComponentList.Clear();
            ReadComponent(ComponentList);
            ListComponents();
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
