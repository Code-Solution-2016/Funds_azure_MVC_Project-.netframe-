using System.Collections;
using System.Data.SqlClient;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS
{
    public class Donations
    {

        //Connection instance
        Database_connection connection = new Database_connection();


        //get connection method
        private string secure_connection()
        {

            //then return the connection
            return connection.Database_String_connection();
        }

        public string InsertDonation(string donorName, string donorEmail, string phoneNumber,
                                string donationType, string dateDonated, string timeDonated,
                                string gender, string role, string donationDetails)
        {
            string to_ = "no"; // Shared_to value set to "no"

            // Define the query to insert a new donation record
            string query = @"INSERT INTO donations (donor_name, donor_email, phone_number, 
                     donation_type, date_donated, time_donated, gender, role, 
                     donation_details, shared_to)
                     VALUES (@DonorName, @DonorEmail, @PhoneNumber, 
                     @DonationType, @DateDonated, @TimeDonated, @Gender, @Role, 
                     @DonationDetails, @SharedTo)";

            // Using SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(secure_connection()))
            {
                SqlCommand command = new SqlCommand(query, connection);

                // Adding parameters to prevent SQL injection
                command.Parameters.AddWithValue("@DonorName", donorName);
                command.Parameters.AddWithValue("@DonorEmail", donorEmail);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@DonationType", donationType);
                command.Parameters.AddWithValue("@DateDonated", dateDonated);
                command.Parameters.AddWithValue("@TimeDonated", timeDonated);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@DonationDetails", donationDetails);
                command.Parameters.AddWithValue("@SharedTo", to_); // Match the parameter name

                try
                {
                    // Open the connection and execute the query
                    connection.Open();
                    command.ExecuteNonQuery();
                    return "Donation successfully recorded.";
                }
                catch (Exception ex)
                {
                    // Return an error message if something goes wrong
                    return $"Error: {ex.Message}";
                }
            }
        }


        public string SearchDonationsByEmail(
    string email,
    ArrayList donorNames,
    ArrayList donorEmails,
    ArrayList phoneNumbers,
    ArrayList donationTypes,
    ArrayList datesDonated,
    ArrayList timesDonated,
    ArrayList genders,
    ArrayList roles,
    ArrayList donationDetails,
    ArrayList sharedTo)
        {
            string query = @"SELECT donor_name, donor_email, phone_number, donation_type, 
                    date_donated, time_donated, gender, role, donation_details, shared_to
                    FROM donations WHERE donor_email = @DonorEmail";

            // Clear arrays to store fresh data
            donorNames.Clear();
            donorEmails.Clear();
            phoneNumbers.Clear();
            donationTypes.Clear();
            datesDonated.Clear();
            timesDonated.Clear();
            genders.Clear();
            roles.Clear();
            donationDetails.Clear();
            sharedTo.Clear();

            using (SqlConnection connection = new SqlConnection(secure_connection()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DonorEmail", email);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            donorNames.Add(reader["donor_name"].ToString());
                            donorEmails.Add(reader["donor_email"].ToString());
                            phoneNumbers.Add(reader["phone_number"].ToString());
                            donationTypes.Add(reader["donation_type"].ToString());
                            datesDonated.Add(reader["date_donated"].ToString());
                            timesDonated.Add(reader["time_donated"].ToString());
                            genders.Add(reader["gender"].ToString());
                            roles.Add(reader["role"].ToString());
                            donationDetails.Add(reader["donation_details"].ToString());
                            sharedTo.Add(reader["shared_to"].ToString());
                        }

                        return "Donations found for the specified email.";
                    }
                    else
                    {
                        return "No donations found for this email.";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }


    }
}
