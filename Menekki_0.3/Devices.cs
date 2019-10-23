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

        private void ReadDevices() // read devices from Laitteet.xml
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

        public void ListDevices() // print out device info list, including components
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

        public void NewDevice() // creates new device and asks components for it
        {
            // if list is empty, create new component with id-number 1
            // add _components with new device, so they could link together
            if (!DeviceList.Any())
            {
                DeviceList.Add(new SingleDevice(1, _components));
                // if new device creation returns with empty name, delete the device from the list
                // because that means user did not want to create it after all
                if (DeviceList[0].Name == "")
                {
                    DeviceList.RemoveAll(SingleComponent => SingleComponent.Name == "");
                }
                SaveDevices();
            }
            else
            {
                // check last id in componentlist and add +1 to it
                // add _components with new device, so they could link together
                int lastIndex = DeviceList.Count - 1;
                DeviceList.Add(new SingleDevice(DeviceList[lastIndex].Id + 1, _components));
                if (DeviceList[lastIndex+1].Name == "")
                {
                    // if new device creation returns with empty name, delete the device from the list
                    // because that means user did not want to create it after all
                    Console.Write("Perutaan laitteen luonti.");
                    DeviceList.RemoveAll(SingleComponent => SingleComponent.Name == "");
                }
                SaveDevices();
            }
        }

        public void DeleteDevice() // deletes device from the list
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

        public void SaveDevices() // saves devices to Laitteet.xml
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

        public void BuildDevice() // lets user get "shopping list" of components for specific device
        {
            try
            {

                int i = 0;
                foreach (var d in DeviceList)
                {
                    Console.WriteLine($"{DeviceList[i].Id}. {DeviceList[i].Name}");
                    i++;
                }

                Console.Write("\nAnna rakennettavan laitteen id: ");
                int id = int.Parse(Console.ReadLine());
                id--;
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
                Console.WriteLine("\nTulostetaan varastosaldot: ");
                Console.WriteLine($"-------------------------------------------------------");
                Console.WriteLine($"TARVE                |      |            | PUUTTUU");
                Console.WriteLine($"{pcs,3} laitteeseen      |  KPL | VARASTOSSA |   KPL");
                Console.WriteLine($"-------------------------------------------------------");


                foreach (var c in DeviceList[id].DeviceComps)
                {
                    Console.Write($"{_components.GetComponentNameByID(c.ComponentID),-20} |" +
                        $"{(c.ComponentAmount*pcs).ToString().PadLeft(4)}  |");
                    foreach (var d in _components.GetWholeComponentList())
                    {
                        if (_components.GetComponentNameByID(c.ComponentID) == d.Name)
                        {
                            Console.Write($"{(d.Pcs),4} kpl");
                            if ((c.ComponentAmount * pcs) - (d.Pcs) > 0)
                            {
                                // component amount neede to build needed amount of devices
                                Console.WriteLine($"    | {(c.ComponentAmount * pcs) - (d.Pcs),5} kpl");
                            }
                            else
                            {
                                Console.WriteLine($"    |");
                            }

                        }
                    }
                }
                Console.WriteLine("-------------------------------------------------------");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
