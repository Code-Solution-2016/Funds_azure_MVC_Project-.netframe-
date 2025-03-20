//using
using System;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS
{
    public class Database_connection
    {

        //return method for string connection
        public string Database_String_connection()
        {
            //temp variable
            string connection = "Server=tcp:code-solutions.database.windows.net,1433;Initial Catalog=rosebank_databases;Persist Security Info=False;User ID=code-solution;Password=RosebankFree@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";

            //then return to connect
            return connection;
        }


    }
}
