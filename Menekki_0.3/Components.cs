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
        //THIS CLASS IS TO KEEP A LIST OF ALL COMPONENTS IN STOCK
        // and this is the list:
        List<SingleComponent> ComponentList = new List<SingleComponent>();
        public static string pathAndFilename = "Komponentit.xml";

        //CONSTRUCTOR
        public Components()
        {
            ReadComponents();
        }

        //METHODS
        //READ COMPONENTS FROM FILE
        private void ReadComponents()
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

        public void ListComponents()
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
                Worth();
            }
        }

        public void NewComponent()
        {
            // if list is empty, create new component with id-number 1
            if (!ComponentList.Any())
            {
                ComponentList.Add(new SingleComponent(1));
            }
            else
            {
                // check last id in componentlist and add +1 to it
                int lastIndex = ComponentList.Count - 1;
                ComponentList.Add(new SingleComponent(ComponentList[lastIndex].Id + 1));
                SaveComponents();
            }
        }

        public void DeleteComponent()
        {
            Console.WriteLine("\n");
            ListComponents();

            if (!ComponentList.Any())
            {
                Console.WriteLine("Ei poistettavia komponentteja.\n");
            }
            else
            {
                Console.WriteLine("Anna poistettavan komponentin ID:");
                int removableId = int.Parse(Console.ReadLine());

                // check if user input is between id's stored in ComponentList
                if (removableId >= ComponentList[0].Id && removableId <= ComponentList[ComponentList.Count() - 1].Id)
                {
                    Console.WriteLine($"--> {ComponentList.Find(SingleComponent => SingleComponent.Id == removableId).Name} on poistettu.");
                    Console.WriteLine();

                    ComponentList.RemoveAll(SingleComponent => SingleComponent.Id == removableId);

                    ListComponents();
                    SaveComponents();
                }
                else
                {
                    Console.WriteLine("Epäkelpo id.\n");
                }
            }
        }
        
        public void Worth()
        {
            double sum = 0;
            foreach (var c in ComponentList)
            {
                sum += c.Price * c.Pcs;
            }
            Console.WriteLine($"\nVaraston arvo {sum.ToString("F")} €");
        }

        public void SaveComponents()
        {
            try
            {
                if (File.Exists(pathAndFilename))
                {
                    // Saving into xml-file
                    XmlSerializer writer = new XmlSerializer(ComponentList.GetType());

                    System.IO.FileStream file = System.IO.File.Create(pathAndFilename);

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

    }

}

