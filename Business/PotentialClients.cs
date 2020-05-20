using BairesDev.Data;
using BairesDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BairesDev.Business
{
    public static class PotentialClients
    {

        public static List<int> ChoosePotentialClientsWithinCountries(bool considerCountry, int limiter)
        {
            int count = 0;
            
            List<int> peopleIDsToContact = new List<int>();
            using (var db = new MainDataContext())
            {
                List<RolesWithPriorities> rolesWithPriorities = db.RolesWithPriorities.OrderBy(r => r.Priority).ToList();
                List<Person> people = new List<Person>();
                for (int i = 0; i < rolesWithPriorities.Count; i++)
                {
                   
                        if (considerCountry)
                            people = (from c in db.People
                                      where c.CurrentRole.Contains(rolesWithPriorities[i].Role)
                                      && (from country in db.Countries
                                          select country.Name)
                                         .Contains(c.Country)
                                      select c).ToList();
                        else
                        people = (from c in db.People
                                  where c.CurrentRole.Contains(rolesWithPriorities[i].Role)
                                  && !(from country in db.Countries
                                      select country.Name)
                                     .Contains(c.Country)
                                  select c).ToList();
                    if ((count + people.Count) <= limiter)
                        {
                            peopleIDsToContact.AddRange(people.Select(x => x.PersonId));
                            count += people.Count;
                        }
                        else
                        {

                            peopleIDsToContact.AddRange(people.Take(limiter - count).Select(x => x.PersonId));
                            count = peopleIDsToContact.Count;
                        }
                    if (count == limiter)
                        break;
                }

            }
            return peopleIDsToContact;
        }
    }
}
