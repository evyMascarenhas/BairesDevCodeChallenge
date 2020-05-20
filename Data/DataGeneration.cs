using AutoMapper;
using BairesDev.Models;
using LINQtoCSV;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BairesDev.Data
{
    public static class DataGeneration
    {
        private static string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "\\Files";
        public static void CreateRolesWithPriorities()
        {
            using (var db = new MainDataContext())
            {
                //delete all to prevent duplicates                
                db.Database.ExecuteSqlRaw("TRUNCATE TABLE [RolesWithPriorities]");
                db.SaveChanges();
                db.Add(new RolesWithPriorities("programmer", 1));
                db.Add(new RolesWithPriorities("manage", 2));
                db.Add(new RolesWithPriorities("Leader", 3));
                db.Add(new RolesWithPriorities("President", 4));
                db.Add(new RolesWithPriorities("Owner", 4));
                db.SaveChanges();
            }
        }
        public static void CreateCountries()
        {
            using (var db = new MainDataContext())
            {
                //delete all to prevent duplicates                
                db.Database.ExecuteSqlRaw("TRUNCATE TABLE [Countries]");
                db.SaveChanges();
                db.Add(new Country("Brazil"));
                db.Add(new Country("Argentina"));
                db.Add(new Country("Chile"));
                db.Add(new Country("Paraguay"));
                db.Add(new Country("Uruguay"));
                db.Add(new Country("Colombia"));
                db.Add(new Country("Venezuela"));
                db.Add(new Country("Ecuador"));
                db.Add(new Country("Costa Rica"));
                db.Add(new Country("Mexico"));
                db.Add(new Country("Peru"));
                db.Add(new Country("Panama"));
                db.Add(new Country("United States"));
                db.Add(new Country("Puerto Rico"));


                db.SaveChanges();
            }
        }

        public static void ImportFile()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CsvPerson, Person>();
            });

            IMapper mapper = config.CreateMapper();

            CsvFileDescription csvFileDescription = new CsvFileDescription
            {
                SeparatorChar = '|',
                FirstLineHasColumnNames = false,
                EnforceCsvColumnAttribute = true
            };

            CsvContext csvContext = new CsvContext();
            string path = Path.Combine(currentPath, @"people.in");
            StreamReader streamReader = new StreamReader(path);
            IEnumerable<CsvPerson> list = csvContext.Read<CsvPerson>(streamReader, csvFileDescription);

            using (var db = new MainDataContext())
            {
                List<Person> persons = mapper.Map<IEnumerable<CsvPerson>, List<Person>>(list);
                List<Person> query = (from c in persons
                                      where !(from p in db.People
                                              select p.PersonId)
                                              .Contains(c.PersonId)
                                      select c).ToList();
                if (query.Count > 0)
                {
                    List<Person> peopleToInsert = new List<Person>();
                    while (query.Count >0)
                    {
                        peopleToInsert = query.Take(100).ToList();
                        foreach (Person p in peopleToInsert)
                        {
                            db.Add(p);
                        }
                        db.SaveChanges();
                        query.RemoveRange(0, peopleToInsert.Count);
                    }

                }
                

            }
        }

        public static void WriteResults (List<int> IDs)
        {
            string path = Path.Combine(currentPath, @"people.out");
            // Write file using StreamWriter  
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i =0; i< IDs.Count; i++)
                writer.WriteLine(IDs[i].ToString());
            }
        }

    }

}
