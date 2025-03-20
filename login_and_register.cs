using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS
{
    public class login_and_register
    {
        //Connection instance
        Database_connection connection = new Database_connection();


        //get connection method
        private string secure_connection()
        {

            //then return the connection
            return connection.Database_String_connection();
        }

        //register method , to allow users to create accounts
        public string RegisterUser(string email, string phoneNumber, string name, string role, string gender, string password)
        {
            // Temporary variable for feedback messages
            string message = "not_done";

            // Queries to check email existence and insert new user
            string queryCheckEmail = "SELECT COUNT(*) FROM users WHERE email = @Email";
            string queryInsertUser = "INSERT INTO users (email, phone_number, name, role, gender, password) VALUES (@Email, @PhoneNumber, @Name, @Role, @Gender, @Password)";

            // Hash the password before storing
            string hashedPassword = HashPassword(password);

            try
            {
                // Using block to automatically handle connection disposal
                using (var connection = new SqlConnection(secure_connection()))
                {
                    connection.Open();

                    // Check if the email already exists
                    using (SqlCommand checkEmailCommand = new SqlCommand(queryCheckEmail, connection))
                    {
                        checkEmailCommand.Parameters.AddWithValue("@Email", email);
                        int emailExists = (int)checkEmailCommand.ExecuteScalar();  // Returns number of matching emails

                        if (emailExists > 0)
                        {
                            message = "Email already exists";
                            return message;
                        }
                    }

                    //  If the email does not exist, proceed with the insertion
                    using (SqlCommand insertCommand = new SqlCommand(queryInsertUser, connection))
                    {
                        // Add all parameters for the new user
                        insertCommand.Parameters.AddWithValue("@Email", email);
                        insertCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        insertCommand.Parameters.AddWithValue("@Name", name);
                        insertCommand.Parameters.AddWithValue("@Role", role);
                        insertCommand.Parameters.AddWithValue("@Gender", gender);
                        insertCommand.Parameters.AddWithValue("@Password", hashedPassword);  

                        // Execute the insertion command
                        insertCommand.ExecuteNonQuery();

                        // Set success message
                        message = "Registration successful";
                    }


                }
            }
            catch (Exception e)
            {
                // Log the error and set the failure message
                Console.WriteLine(e.Message);
                message = "An error occurred during registration.";
            }

            return message;
        }

        //then login method , accepting the two variables as parameters Email and password
        public string LoginUser(string email, string password)
        {
            string query = "SELECT name, gender,role,password,phone_number FROM users WHERE email = @Email";

            try
            {
                using (var connection = new SqlConnection(secure_connection()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHashedPassword = reader["password"].ToString();
                                string hashedInputPassword = HashPassword(password);

                                // Compare the hashed passwords
                                if (storedHashedPassword == hashedInputPassword)
                                {
                                    // Return email, name, and gender
                                    string name = reader["name"].ToString();
                                    string gender = reader["gender"].ToString();
                                    string role = reader["role"].ToString();
                                    string phone = reader["phone_number"].ToString();
                                    //return the user detials
                                    return $"{email},{name},{gender},{role},{phone}";
                                }
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                // Handle the exception 
                Console.WriteLine(e.Message);
            }

            // Return null if login fails
            return null;
        }


        //hashing password method to ensure comparism for password and when they register
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    // Convert to hex format
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }





    }
}
