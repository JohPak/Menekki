using System;
//to read/write files
using System.IO;
// to count file lines
using System.Linq;
// to save xml-files
using System.Xml;
using System.Xml.Serialization;

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
        private int _id = 0;
        public int Id { get { return _id; } set { _id = value; } }

        //private int _compId = 0;
        //public int CompId { get { return _compId; } set { _compId = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private int _pcs;
        public int Pcs { get { return _pcs; } set { _pcs = value; } }

        private double _price;
        public double Price { get { return _price; } set { _price = value; } }

        //malli propertyn käytöstä
        //c.Name = "jee";
        //string nimi = c.Name;


        //CONSTRUCTOR for reading from file
        public SingleComponent()
        {

        }

        //CONSTRUCTOR #2, for manual component adding
         public SingleComponent(int idForNewComponent)
        {
            Id = idForNewComponent;
            AddNewComponent();
        }


        //METHODS
        public void AddNewComponent()
        {
            bool isNumber = false; // validation helper

            do // ask NAME until input is not empty
            {
                Console.WriteLine("Anna komponentin nimi: ");
                _name = Console.ReadLine();
            } while (_name == "");

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
        }
    }
}
