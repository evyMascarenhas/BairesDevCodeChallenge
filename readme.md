# Customer selection program

This program was written as a code challenge for BairesDev.
BairesDev is looking to  expand their client’s portfolio by sending an email marketing campaign. They requested me to develop a piece of code that, given an input file “people.in” with LinkedIn public data, finds the top 100 people with the highest chance of becoming their clients. The expected output is a file called “people.out” that contains the ids of the people that they should contact.

The input file contains the following fields, separated by a pipe (|): PersonId, Name, LastName, CurrentRole, Country, Industry, NumberOfRecommendations, NumberOfConnections. It is possible that in some cases we don’t know the value of the field, so two consecutive pipes will appear (||).


# What was done

 I've created an algorithm that reads the file people.in, imports to a database and, combined with other info that I thought could help in the task, returns the top 100 possible contacts. 
 The criteria I've used was:
 
  - The country: Since BairesDev has 95% of their developers located in LATAM, it thought that it would be more interesting if our clients are located in the same timezones or with minimum difference. So I've inserted some countries from LATAM and North America to do the initial filter. If we can't find 100 customers with the selected roles within the countries, another search is made in the other countries.
  - The current role: I created a table named RolesWithPriorities to help finding new contacts considering their current roles. These roles are ordered by priorities so, if a programmer is more important than an intern, it will have priority 1 and intern, priority 2. If a president or a CTO is more important than a programmer, in the database a president will have priority 1, CTO can also have priority 1, then a programmer will have priority 2, and so on.
  
### How to run

The project was built in C# using .NetCore framework and SQL Server Express as database. So the prerequisite is to have Visual Studio (it can also be run using .Net Core CLI that allows us to run only using a text editor and .Net Core SDK) and SQL Server Express (2008 or earlier) installed.

* Before Running, we need to create the database. There are two options, so feel free to run the best for you:
   * In Visual Studio: Open the Package Manager Console from Tools → Library Package Manager → Package Manager Console and then run   [[update -database ]]
   * In .Net CLI: run   [[dotnet ef database update]]

### Ways my algorithm could be improved.
* If I had more time, I'd create a way to consider the roles in every language. The way I build, a software developer who has "desarollador de software" wouldn't be considered unless his role was inserted in the table.
* I'd create a graphical interface to customize the limit of results, and allow the user to easily customize the list of roles and priorities, for example.
* I'd include the number of connections in the filter. I believe a person with more connections could become more relevant as it helps in the marketing.
* I'd use stored procedures to run the queries as the performance is usually better when we use the database to do so.

### Which additional data do you think would be relevant to improve your algorithm?
* The size of the company the person works for;
* The quantity of post the person has done in Linkedin;
* The business segment of the company;

