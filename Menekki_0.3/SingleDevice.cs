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
        // list is made with it's own class, so it could store two variables per cell
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
            do // ask NAME until input is not empty
            {
                Console.WriteLine("Anna laitteelle nimi: ");
                _name = Console.ReadLine();
            } while (_name == "");

            Console.Clear();
            _components.ListComponents();

            Console.WriteLine($"\nAnna laitteelle \"{_name}\" siihen kuuluvat komponentit ja niiden määrät. ");
            int ID, PCS;
            string userInput;

            do
            {
                Console.Write("Anna komponentin id: ");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out ID);

                if (ID >= _components.GetCompListFirstID() && ID <= _components.GetCompListLastID())
                {
                    Console.WriteLine($"Lisätään komponentti: {_components.GetComponentNameByID(ID)}");
                    Console.Write("Anna kappalemäärä: ");
                    userInput = Console.ReadLine();
                    int.TryParse(userInput, out PCS);
                    DeviceComps.Add(new IdAndAmount(ID, PCS));
                }
                else if (ID < _components.GetCompListFirstID() || ID > _components.GetCompListLastID())
                {
                    Console.WriteLine("Epäkelpo id.\n");
                }

            } while (userInput != "");
            Console.WriteLine("Poistutaan.");
           
        }
    }
    // helper class to keep two values inside list (=DeviceComps)
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
