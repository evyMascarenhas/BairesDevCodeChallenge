using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Text;

namespace BairesDev.Models
{
    public class CsvPerson
    {

            [CsvColumn(FieldIndex = 1)]
            public int PersonId { get; set; }

            [CsvColumn(FieldIndex = 2)]
            public string Name { get; set; }

            [CsvColumn(FieldIndex = 3)]
            public string LastName { get; set; }

            [CsvColumn(FieldIndex = 4)]
            public string CurrentRole { get; set; }

            [CsvColumn(FieldIndex = 5)]
            public string Country { get; set; }

            [CsvColumn(FieldIndex = 6)]
            public string Industry { get; set; }

            [CsvColumn(FieldIndex = 7)]
            public int NumberOfRecommendations { get; set; }

            [CsvColumn(FieldIndex = 8)]
            public int NumberOfConnections { get; set; }

        

    }
}
