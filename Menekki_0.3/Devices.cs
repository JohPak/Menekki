using System;
// to write/read files:
using System.IO;
// to use lists
using System.Collections.Generic;
// to use to check if list is empty
using System.Linq;
// to save xml-files
using System.Xml.Serialization;

namespace Menekki_0._3
{
    public class Devices
    {

        List<SingleDevice> DeviceList = new List<SingleDevice>();
        private static string pathAndFilename = "Laitteet.xml";

        private Components _components;

        //CONSTRUCTOR
        public Devices(Components comp)
        {
            _components = comp;
            ReadDevices();
        }

        //METHODS

        private void ReadDevices()
        {
            try
            {
                if (File.Exists(pathAndFilename))
                {
                    XmlSerializer serializer = new XmlSerializer(DeviceList.GetType());
                    using StreamReader sr = new StreamReader(pathAndFilename);
                    DeviceList = (List<SingleDevice>)serializer.Deserialize(sr);
                    // Console.WriteLine("Luettu komponentit tiedostosta.");
                }
                else throw new FileNotFoundException($"Tiedostoa {pathAndFilename} ei löydy.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // attach componentlist to each device
            // needed so devices-class could see inside ComponentList
            foreach (var d in DeviceList)
            {
                d.AttachComponentsToDevice(_components);
            }
        }

        public void ListDevices()
        {
            if (DeviceList.Count() == 0)
            {
                Console.WriteLine("-- Laitelista on tyhjä.\n");
            }
            else
            {
                // runs through the list of devices
                foreach (var d in DeviceList)
                {
                    // print each device id and name
                    Console.WriteLine($"Id {d.Id,3}. {d.Name}");
                    double deviceTotalPrice = 0;
                    // runs through each device's components
                    foreach (var c in d.DeviceComps)
                    {
                    // print each components name, pcs, á-price and total-price
                    Console.WriteLine($"\t- {_components.GetComponentNameByID(c.ComponentID).PadRight(20, '.')} {c.ComponentAmount,3} kpl, {_components.GetComponentPriceById(c.ComponentID).ToString("F"),6} € (yht. {(c.ComponentAmount * _components.GetComponentPriceById(c.ComponentID)).ToString("F"),7} €)");
                        
                    // sum up the total cost of one device
                    deviceTotalPrice = deviceTotalPrice + (c.ComponentAmount * _components.GetComponentPriceById(c.ComponentID));
                    }
                    Console.WriteLine("Yhteensä " + deviceTotalPrice.ToString("F") + " €");
                    Console.WriteLine("-----------------------------------------------");
                }
                Console.WriteLine();

            }
        }

        public void NewDevice()
        {
            // if list is empty, create new component with id-number 1
            if (!DeviceList.Any())
            {
                DeviceList.Add(new SingleDevice(1, _components));
                SaveDevices();
            }
            else
            {
                // check last id in componentlist and add +1 to it
                int lastIndex = DeviceList.Count - 1;
                DeviceList.Add(new SingleDevice(DeviceList[lastIndex].Id + 1, _components));
                SaveDevices();
            }
        }

        public void DeleteDevice()
        {
            Console.WriteLine("\n");
            ListDevices();

            if (!DeviceList.Any())
            {
                Console.WriteLine("Ei poistettavia laitteita.\n");
            }
            else
            {
                Console.WriteLine("Anna poistettavan laitteen ID:");
                string userInput = Console.ReadLine();
                int removableId;

                if (userInput != "")
                {
                    int.TryParse(userInput, out removableId);

                    // check if user input is between id's stored in ComponentList
                    if (removableId >= DeviceList[0].Id && removableId <= DeviceList[DeviceList.Count() - 1].Id)
                    {
                        Console.WriteLine($"--> {DeviceList.Find(SingleComponent => SingleComponent.Id == removableId).Name} on poistettu.");
                        Console.WriteLine();

                        DeviceList.RemoveAll(SingleComponent => SingleComponent.Id == removableId);

                        ListDevices();
                        SaveDevices();
                    }
                    else
                    {
                        Console.WriteLine("Epäkelpo id.\n");
                    }
                }
            }
        }

        public void SaveDevices()
        {
            try
            {
                if (File.Exists(pathAndFilename))
                {
                    // Saving into xml-file
                    XmlSerializer writer = new XmlSerializer(DeviceList.GetType());

                    System.IO.FileStream file = System.IO.File.Create(pathAndFilename);

                    writer.Serialize(file, DeviceList);
                    file.Close();
                }
                else throw new FileNotFoundException($"Tiedostoa {pathAndFilename} ei löydy.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void BuildDevice()
        {
            int i = 0;
            foreach (var d in DeviceList)
            {
                Console.WriteLine($"{DeviceList[i].Id}. {DeviceList[i].Name}");
                i++;
            }

            Console.Write("\nAnna rakennettavan laitteen id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(DeviceList[id].Name);

            foreach (var c in DeviceList[id].DeviceComps)
            {
                Console.WriteLine($"\t- {_components.GetComponentNameByID(c.ComponentID).PadRight(20, '.')} {c.ComponentAmount,3} kpl, {_components.GetComponentPriceById(c.ComponentID).ToString("F"),6} € (yht. {(c.ComponentAmount * _components.GetComponentPriceById(c.ComponentID)).ToString("F"),7} €)");
            }

            Console.Write("Montako rakennetaan? ");
            int pcs = int.Parse(Console.ReadLine());

            foreach (var c in DeviceList[id].DeviceComps)
            {
                Console.WriteLine($"\t- {_components.GetComponentNameByID(c.ComponentID).PadRight(20, '.')} {c.ComponentAmount,3} * {pcs,2} = {c.ComponentAmount*pcs,3} kpl, {_components.GetComponentPriceById(c.ComponentID).ToString("F"),6} € (yht. {(c.ComponentAmount * _components.GetComponentPriceById(c.ComponentID)).ToString("F"),7} €)");
                // TÄHÄN JATKOKSI TARKISTUKSET LÖYTYYKÖ KOMPONENTTEJA JA KUINKA PALJON
            }

        }
    }
}
