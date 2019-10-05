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
            - PARSE COMPONENTS saved into a file and then make them as object
        */


        // VARIABLES
        // each component must have id, name, pcs and price
        private static int _id = 0; //for
        public int Id { get { return _id; } set { _id = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private int _pcs;
        public int Pcs { get { return _pcs; } set { _pcs = value; } }

        private double _price;
        public double Price { get { return _price; } set { _price = value; } }



        //CONSTRUCTOR
        public SingleComponent()
        {
            AddNewComponent();
        }

        //CONSTRUCTOR #2, for components saved in a file
        public SingleComponent(string lineFromFile)
        {
            //Parse the read line from the file
            ParseComponent(lineFromFile);
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
            string newLine = $"{_id} {_name} {_pcs} {_price.ToString("F")}";
            
            //SAVE the component
            using (StreamWriter comp = File.AppendText($"komponentit.txt"))
            {
                comp.Write("\n" + newLine);
            }
            
        }

        public void ParseComponent(string linetoparse)
        {
            string[] splitted = linetoparse.Split(' ');
                //example of the lines in file: 1 ruuvi 16 1.50
                _id = int.Parse(splitted[0]);
                _name = splitted[1];
                _pcs = int.Parse(splitted[2]);
                _price = double.Parse(splitted[3]);
            
        }

        

    }
}
