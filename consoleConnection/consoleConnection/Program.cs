using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace consoleConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = dbTestDatabase; Integrated Security = True");
            var firstName = "";
            var lastName = "";
            connection.Open();
            
            while (true)
            {
                Console.WriteLine("Enter first then last name of client to be saved in database.");

                //all this code is in a while loop so we keep asking user for a name until they enter a valid one
                while (true)
                {
                    var fullName = Console.ReadLine();
                    var names = fullName.Split(' ');
                     
                    //test that they entered only two names seperated by a string, if they entered nothing, no space, or too many names we give them an error
                    if (names.Count() == 2 && !String.IsNullOrWhiteSpace(names[0]) && !String.IsNullOrWhiteSpace(names[1]))
                    {
                        firstName = names[0];
                        lastName = names[1];
                        break;
                    }
                    //if we made it here they screwed up their entry so we remind them
                    Console.WriteLine("Enter first name then last name seperated by a space.");
                }
                //if we have made it to here they have entered a valid first and last name so we can proceed
                String query = "INSERT INTO tblClients (strFirstName, strLastName) VALUES (@firstName,@lastName)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);

                command.ExecuteNonQuery();

                Console.WriteLine("Entry Successful.");
                Console.WriteLine("Enter another client? y/n");
                
                var result = Console.ReadLine();
                if(result != "y" && result != "yes" && result != "Yes")
                {
                    break;
                }

            }

            connection.Close();
        }
    }
}
