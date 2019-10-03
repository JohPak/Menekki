using System;
// to write/read files:
using System.IO;
// to use lists
using System.Collections.Generic;
// to use to check if list is empty
using System.Linq;


namespace Menekki_0._3
{
    public class Components
    {
        //THIS CLASS IS TO KEEP A LIST OF ALL COMPONENTS IN STOCK
        // and this is the list:
        List<object> ComponentList = new List<object>();


        //CONSTRUCTOR
        public Components()
        {
            // check if ComponentList is empty
            if (!ComponentList.Any())
            {
                // if empty, insert data from file
                Console.WriteLine("Luetaan listaan tiedoston sisältö. ");
                ComponentList.Add(ReadComponent());
            }
            else
            {
                // if not empty, append new component
                Console.WriteLine("Lisätään uusi komponentti:");
                ComponentList.Add(NewComponent());
            }
        }

        //METHODS
        static object ReadComponent()
        {
            //SHOULD read components from a file NOT WORKING YET
            SingleComponent ReadComp = new SingleComponent();
            return ReadComp;
        }

        static object NewComponent()
        {
            //creates NEW component
            SingleComponent NewComp = new SingleComponent();
            return NewComp;
        }
        
    }
}
