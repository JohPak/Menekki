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
    public class Components
    {
        // This class keeps record of all components.
        // Components are stored in a list and saved into xml-file
        // The class also has various methods to manage the component list

        List<SingleComponent> ComponentList = new List<SingleComponent>();

        // path and name of xml-file. All components will be saved there.
        private static string pathAndFilename = "Komponentit.xml";

        //CONSTRUCTOR
        public Components()
        {
            // reads components into memory from xml-file at the start of program
            ReadComponents();
        }

        //METHODS

        private void ReadComponents() // reads components from xml-file 
        {
            try
            {
                if (File.Exists(pathAndFilename))
                {
                    XmlSerializer serializer = new XmlSerializer(ComponentList.GetType());
                    using StreamReader sr = new StreamReader(pathAndFilename);
                    ComponentList = (List<SingleComponent>)serializer.Deserialize(sr);
                   // Console.WriteLine("Luettu komponentit tiedostosta.");
                }
                else throw new FileNotFoundException($"Tiedostoa {pathAndFilename} ei löydy.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 

        public void ListComponents() // prints informative list of components 
        {
            if (ComponentList.Count() == 0)
            {
                Console.WriteLine("-- Komponenttilista on tyhjä.");
            }
            else
            {
                foreach (var c in ComponentList)
                {
                    Console.WriteLine($"Id {c.Id,3}. {c.Name.PadRight(20, '.')} {c.Pcs,3} kpl, á {c.Price.ToString("F"),6} € (yht. {(c.Pcs * c.Price).ToString("F"),7} €)");
                }
                //Worth();
            }
        } 

        public void NewComponent() // creates new component 
        {
            // if list is empty, create new component with id-number 1
            if (!ComponentList.Any())
            {
                ComponentList.Add(new SingleComponent(1));
                SaveComponents();
            }
            else
            {
                // check last id in componentlist and add +1 to it
                int lastIndex = ComponentList.Count - 1;
                ComponentList.Add(new SingleComponent(ComponentList[lastIndex].Id + 1));
                SaveComponents();
            }
        } 

        public void DeleteComponent() // deletes component 
        {
            Console.WriteLine();
            ListComponents();

            if (!ComponentList.Any())
            {
                Console.WriteLine("Ei poistettavia komponentteja.\n");
            }
            else
            {
                Console.WriteLine("\nAnna poistettavan komponentin ID:");
                string userInput = Console.ReadLine();
                int removableId;

                if (userInput != "")
                {
                    int.TryParse(userInput, out removableId);

                    // check if user input is between id's stored in ComponentList
                    if (removableId >= ComponentList[0].Id && removableId <= ComponentList[ComponentList.Count() - 1].Id)
                    {
                        Console.Clear();
                        Console.WriteLine($"--> {ComponentList.Find(SingleComponent => SingleComponent.Id == removableId).Name} on poistettu.");
                        Console.WriteLine();

                        ComponentList.RemoveAll(SingleComponent => SingleComponent.Id == removableId);

                        ListComponents();
                        SaveComponents();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Epäkelpo id.\n");
                    }
                }
            }
        }

        public void DeleteComponentById(int id, int amountToDelete) // delete components by amount
        {
            //method takes component id and amount to be subtracted from stock as a parameters

            // this line searches component by given id, and then subtracts given amount from it
            ComponentList.Find(SingleComponent => SingleComponent.Id == id).Pcs -= amountToDelete;
            SaveComponents();
        }

        public void EditComponent() // lets user edit component information 
        {
            if (!ComponentList.Any())
            {
                Console.WriteLine("Ei muokattavia komponentteja.\n");
            }
            else
            {
                ListComponents();

                Console.WriteLine("\nAnna muokattavan komponentin ID:");
                string userInput = Console.ReadLine();

                if (userInput != "")
                {
                    int editableId = int.Parse(userInput);
                    // check if user input is between smallest/biggest id-number stored in ComponentList
                    if (editableId >= ComponentList[0].Id && editableId <= ComponentList[ComponentList.Count() - 1].Id)
                    {
                        Console.WriteLine("Syötä uudet tiedot. Jätä tyhjäksi, jos et halua muuttaa. ");
                        string input;

                        // print out selected component's name
                        // no idea how this works but it serves me very well:
                        // ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Name
                        Console.Write($"{ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Name} --> ");
                        input = Console.ReadLine();

                        if (input != "")
                        {
                            // if input is not empty, save new name for the component
                            ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Name = input;
                        }

                        Console.Write($"{ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Pcs} kpl --> ");
                        input = Console.ReadLine();

                        // if input is not empty, save new pcs for the component
                        if (input != "")
                        {
                            ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Pcs = int.Parse(input);
                        }

                        Console.Write($"{ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Price} € --> ");
                        input = Console.ReadLine();

                        // if input is not emty, save new price for the component
                        if (input != "")
                        {
                            ComponentList.Find(SingleComponent => SingleComponent.Id == editableId).Price = double.Parse(input);
                        }
                        Console.WriteLine();
                        ListComponents();
                        SaveComponents();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Epäkelpo id.\n");
                    }
                }
            }
        } 

        public void Worth() // prints total price of all components in stock 
        {
            double sum = 0;
            foreach (var c in ComponentList)
            {
                sum += c.Price * c.Pcs;
            }
            Console.WriteLine($"\nVaraston arvo {sum.ToString("F")} €\n");
        } 

        public void SaveComponents() // save all components into a xml-file 
        {
            try
            {
                if (File.Exists(pathAndFilename))
                {
                    // Saving into xml-file
                    XmlSerializer writer = new XmlSerializer(ComponentList.GetType());

                    FileStream file = File.Create(pathAndFilename);

                    writer.Serialize(file, ComponentList);
                    file.Close();
                }
                else throw new FileNotFoundException($"Tiedostoa {pathAndFilename} ei löydy.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 


//***** LITTLE HELPER METHODS

        public SingleComponent GetComponentByIndex(int index)
        {
            return ComponentList[index];
        }

        public SingleComponent GetComponentById(int id)
        {
            return ComponentList.Find(SingleComponent => SingleComponent.Id == id);
        }

        public List<SingleComponent> GetWholeComponentList()
        {
            return ComponentList;
        }

        public int GetComponentListCount()
        {
            return ComponentList.Count;
        }

        public int GetCompListLastID() // get ID of last component in list
        {
            return ComponentList[(ComponentList.Count - 1)].Id;
        } 

        public int GetCompListFirstID() // get ID of first component in list
        {
            return ComponentList[0].Id;
        } 

        public string GetComponentNameByID(int id) 
        {
            string ComponentNameToReturn;
            try
            {
                ComponentNameToReturn = ComponentList.Find(SingleComponent => SingleComponent.Id == id).Name;
                return ComponentNameToReturn;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                // writes line of deleted component on red color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Komponentti on poistettu (id {id}) \n");
                Console.ResetColor();
            }
            return "";

            // ComponentnNameToReturn = ComponentList.Find(SingleComponent => SingleComponent.Id == id).Name;

        }

        public double GetComponentPriceById(int id)
        {
            try
            {
                double price = ComponentList.Find(SingleComponent => SingleComponent.Id == id).Price;
                return price;
            }
            catch (Exception ex)
            {
                // Console.ForegroundColor = ConsoleColor.Red;
                // Console.Write(0);
                // Console.ResetColor();

            }
            return 0;
        }
    }
}

