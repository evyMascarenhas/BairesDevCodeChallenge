using BairesDev.Business;
using BairesDev.Data;
using BairesDev.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BairesDev
{
    class Program
    {
        //Change here if you want more than 100 records
        const int limiter = 100;
        static void Main(string[] args)
        {
            DataGeneration.CreateRolesWithPriorities();
            DataGeneration.CreateCountries();
            DataGeneration.ImportFile();
            List<int> peopleIDsToContact = new List<int>();
            Console.WriteLine("Running Analysis....");
            //To calculate the potentian clientes it was considered if they had a role that we consider important
            //And the country. 
           
            peopleIDsToContact.AddRange(PotentialClients.ChoosePotentialClientsWithinCountries(true, limiter));
            if (peopleIDsToContact.Count < limiter)
            {
                //If we can't find enough clients with the previous criteria, we can consider clients 
                //that are located in other countries.
                peopleIDsToContact.AddRange (PotentialClients.ChoosePotentialClientsWithinCountries(false, limiter));
            }
            if (peopleIDsToContact.Count > 0)
            {
                DataGeneration.WriteResults(peopleIDsToContact);
                 string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Files\\";
                 Console.WriteLine("The top " + limiter.ToString()+" potential clients were exported to the file " + currentPath + "people.out");
            }
            else
            {
                Console.WriteLine("No potential clients found. Please review the roles that should be considered");
            }




        }




    }

}
