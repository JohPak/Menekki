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
        private static int _id = 0;
        public int Id { get { return _id; } set { _id = value; } }

        private int _compId = 0;
        public int CompId { get { return _compId; } set { _compId = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private int _pcs;
        public int Pcs { get { return _pcs; } set { _pcs = value; } }

        private double _price;
        public double Price { get { return _price; } set { _price = value; } }

        //malli propertyn käytöstä
        //c.Name = "jee";
        //string nimi = c.Name;


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
            bool isNumber = false; // validation helper

            do // ask NAME until input is not empty
            {
                Console.WriteLine("Anna komponentin nimi: (älä käytä välilyöntejä) ");
                _name = Console.ReadLine();
            } while (_name == "" || _name.Contains(" "));

            do // ask PCS until input is number and > 0
            {
                Console.WriteLine("Anna kappalemäärä: ");
                isNumber = int.TryParse(Console.ReadLine(), out _pcs);
            } while (!isNumber || _pcs <= 0);

            do // ask PRICE until input is number and > 0
            {
                Console.WriteLine("Anna kappalehinta €: (0.00) ");
                isNumber = double.TryParse(Console.ReadLine(), out _price);
            } while (!isNumber || _price <= 0);

            // check last saved id on the file and then add 1 to it.
            string[] getId = (File.ReadLines($"komponentit.txt").Last()).Split(' ');
            _id = int.Parse(getId[0]);
            _id++;
            CompId = _id;

            //ToString("F") ensures 2 decimals are written
            string newLine = $"{_id} {_name} {_pcs} {_price.ToString("F")}";
            
            //SAVE the component
            using (StreamWriter comp = File.AppendText($"komponentit.txt"))
            {
                //comp.Write("\n" + newLine);
                comp.Write(newLine + "\n");
            }
            
        }

        public void ParseComponent(string linetoparse)
        {
            string[] splitted = linetoparse.Split(' ');
                //example of the lines in file: 1 ruuvi 16 1.50
                _compId = int.Parse(splitted[0]);
                _name = splitted[1];
                _pcs = int.Parse(splitted[2]);
                _price = double.Parse(splitted[3]);
            
        }

        

        

    }
}
