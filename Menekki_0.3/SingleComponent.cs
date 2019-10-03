using System;
//to read/write files
using System.IO;
// to count file lines
using System.Linq;

namespace Menekki_0._3
{
    public class SingleComponent
    {
        /* THIS CLASS IS TO
            - CREATE NEW SINGLE COMPONENT
            - READ COMPONENTS FROM FILE
        */


        // VARIABLES
        // each component must have id, name, pcs and price
        static int _id = 0;
        private string _name;
        private int _pcs;
        private double _price;
      

        //CONSTRUCTOR
        public SingleComponent()
        {
            AddNewComponent();
        }



        //METHODS

        public void AddNewComponent()
        {
            Console.WriteLine("Anna komponentin nimi: ");
            _name = Console.ReadLine();
            Console.WriteLine("Anna kappalemäärä: ");
            _pcs = int.Parse(Console.ReadLine());
            Console.WriteLine("Anna kappalehinta: ");
            _price = double.Parse(Console.ReadLine());
            //component id grows with every component created
            _id++;

            //ToString("F") ensures 2 decimals are written
            string newLine = $"{_id}, {_name}, {_pcs}, {_price.ToString("F")}";

            /*
            //SAVE the component
            using (StreamWriter comp = File.AppendText($"komponentit.txt"))
            {
                comp.Write("\n" + newLine);
            }
            */
        }

        //READ COMPONENTS FROM FILE
        public string ReadFromFile()
        {
            //declare helper for file reading
            string line;
            //open the file to read components from
            StreamReader file = new StreamReader($"komponentit.txt");
            while ((line = file.ReadLine()) != null)
            {
                return line;
            }
            return line;
        }
    }
}
