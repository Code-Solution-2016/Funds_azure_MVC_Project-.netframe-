using System.Data.SqlClient;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS
{
    public class user_info
    {

        //Connection instance
        Database_connection connection = new Database_connection();


        //get connection method
        private string secure_connection()
        {

            //then return the connection
            return connection.Database_String_connection();
        }


        public string get_user(string email)
        {
            // Define the query with a parameter placeholder
            string query = "SELECT name, gender, role, phone_number FROM users WHERE email = @Email";

            try
            {
                using (var connection = new SqlConnection(secure_connection()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter value for email
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = command.ExecuteReader())
                        {
                            // Check if a record exists
                            if (reader.Read())
                            {
                                // Extract the data from the reader
                                string name = reader["name"].ToString();
                                string gender = reader["gender"].ToString();
                                string role = reader["role"].ToString();
                                string phone = reader["phone_number"].ToString();

                                // Return the user details as a string
                                return $"{email},{name},{gender},{role},{phone}";
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                // Handle and log the exception 
                return $"Error: {e.Message}";
            }

            // If no user is found, return null or a message
            return null;
        }

    }
}
