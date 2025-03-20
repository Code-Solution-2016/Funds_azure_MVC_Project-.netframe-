using System.Collections;
using System.Data.SqlClient;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS
{
    public class Incidents
    {


        //Connection instance
        Database_connection connection = new Database_connection();


        //get connection method
        private string secure_connection()
        {

            //then return the connection
            return connection.Database_String_connection();
        }


        public string InsertIncident(string title, string date, string province, string town, string village, string affectedPopulation, string description, string email)
        {

            //
            string message = "no";
            using (var connection = new SqlConnection(secure_connection()))
            {
                connection.Open();
                string insertQuery = @"
        INSERT INTO incidents (incident_title, incident_date, province, town, village, affected_population, incident_description, email)
        VALUES (@title, @date, @province, @town, @village, @affectedPopulation, @description, @Email);";

                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@province", province);
                    command.Parameters.AddWithValue("@town", town);
                    command.Parameters.AddWithValue("@village", village);
                    command.Parameters.AddWithValue("@affectedPopulation", affectedPopulation);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@Email", email); 

                    command.ExecuteNonQuery();

                    message = "done";
                }


            }

            return message;
        }



        public string SearchIncidentsByEmail(
    string email,
    ArrayList incidentIds,
    ArrayList incidentTitles,
    ArrayList incidentDates,
    ArrayList provinces,
    ArrayList towns,
    ArrayList villages,
    ArrayList affectedPopulations,
    ArrayList incidentDescriptions,
    ArrayList createdAt)
        {
            string query = @"
    SELECT id, incident_title, incident_date, province, town, village, 
           affected_population, incident_description, created_at
    FROM incidents WHERE email = @Email";

            // Clear arrays to store fresh data
            incidentIds.Clear();
            incidentTitles.Clear();
            incidentDates.Clear();
            provinces.Clear();
            towns.Clear();
            villages.Clear();
            affectedPopulations.Clear();
            incidentDescriptions.Clear();
            createdAt.Clear();

            using (SqlConnection connection = new SqlConnection(secure_connection()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email); // Using email parameter

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            incidentIds.Add(reader["id"].ToString()); // Retrieve incident ID
                            incidentTitles.Add(reader["incident_title"].ToString());
                            incidentDates.Add(reader["incident_date"].ToString());
                            provinces.Add(reader["province"].ToString());
                            towns.Add(reader["town"].ToString());
                            villages.Add(reader["village"].ToString());
                            affectedPopulations.Add(reader["affected_population"].ToString());
                            incidentDescriptions.Add(reader["incident_description"].ToString());
                            createdAt.Add(reader["created_at"].ToString()); // Retrieve creation timestamp
                        }

                        return "Incidents found for the specified email.";
                    }
                    else
                    {
                        return "No incidents found for this email.";
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
