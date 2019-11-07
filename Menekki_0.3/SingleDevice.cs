using System;
// to use lists
using System.Collections.Generic;

namespace Menekki_0._3
{
    public class SingleDevice
    {
        // VARIABLES
        // each device must have id, name, components
        private int _id = 0;
        public int Id { get { return _id; } set { _id = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        // this holds all components a device is made of (component's id and pcs)
        // list is made with it's own class (written end of this file), so it could store two variables per cell
        public List<IdAndAmount> DeviceComps = new List<IdAndAmount>(); 

        // _components are being brought as a parameter from devices-class
        // needed for linking components to devices 
        private Components _components;

        //CONSTRUCTOR #1 for reading from file
        public SingleDevice()
        {

        }

        //CONSTRUCTOR #2, for manual device adding
        public SingleDevice(int idForNewDevice, Components comp)
        {
            Id = idForNewDevice;
            _components = comp;
            AddNewDevice();
        }

        //METHODS
        // attaches ComponentList to each device, so it could see what components are stored in Components-class' instance
        public void AttachComponentsToDevice(Components comps)
        {
            _components = comps;
        }


        public void AddNewDevice()
        {
            
            Console.WriteLine("Anna laitteelle nimi: ");
            _name = Console.ReadLine();
            if (_name == "")
            {
                // if user gives empty name, return and delete constructor-created (empty) device from the list
                return;
            }

            Console.Clear();
            Console.WriteLine("KOMPONENTTIEN LISÄYS LAITTEELLE\n");
            _components.ListComponents();

            Console.WriteLine($"\nAnna laitteelle \"{_name}\" siihen kuuluvat komponentit ja niiden määrät. ");
            int ID, PCS;
            string userInput;

            do
            {
                // ask user components till input is empty
                Console.Write("Anna komponentin id: ");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out ID);

                // test if user inputted id-number is between the list's id-range
                if (ID >= _components.GetCompListFirstID() && ID <= _components.GetCompListLastID())
                {
                    Console.WriteLine($"Lisätään komponentti: {_components.GetComponentNameByID(ID)}");
                    Console.Write("Anna kappalemäärä: ");
                    userInput = Console.ReadLine();
                    int.TryParse(userInput, out PCS);

                    // use helper class to store both (id & pcs) values into next available list cell
                    DeviceComps.Add(new IdAndAmount(ID, PCS));
                }
                // test if user inputted id-number outside the list's id-range
                else if (ID < _components.GetCompListFirstID() || ID > _components.GetCompListLastID())
                {
                    if (userInput == "")
                    {
                        Console.WriteLine("Poistutaan");
                    }
                    Console.WriteLine("Epäkelpo id.\n");
                }

            } while (userInput != "");
           
        }
    }
    // helper class to store valuepairs inside list (=DeviceComps)
    // I was suppose to use Dictionary, but had some problems with serializing it, so I made new class instead.
    public class IdAndAmount
    {
        private int _componentID;
        public int ComponentID { get { return _componentID; } set { _componentID = value; } }

        private int _componentAmount;
        public int ComponentAmount { get { return _componentAmount; } set { _componentAmount = value; } }

        //CONSTRUCTOR
        public IdAndAmount()
        {
            ComponentID = 0;
            ComponentAmount = 0;
        }

        public IdAndAmount(int compID, int compAmount)
        {
            ComponentID = compID;
            ComponentAmount = compAmount;
        }
    }
}
