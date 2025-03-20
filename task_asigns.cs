using System.Collections;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS
{
    public class task_asigns
    {

        //Connection instance
        Database_connection connection = new Database_connection();


        //get connection method
        private string secure_connection()
        {

            //then return the connection
            return connection.Database_String_connection();
        }



        public string get_user(ArrayList id, ArrayList name)
        {
            // Define the query to get users with the role of 'Volunteer'
            string query = "SELECT id, name FROM users WHERE role = @role";

            try
            {
                using (var connection = new SqlConnection(secure_connection()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add the parameter value for role
                        command.Parameters.AddWithValue("@role", "Volunteer");

                        using (var reader = command.ExecuteReader())
                        {
                            // Loop through each record
                            while (reader.Read())
                            {
                                // Add the id and name to the respective ArrayLists
                                id.Add(reader["id"].ToString());
                                name.Add(reader["name"].ToString());
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

            // If users are found,then  return a success message
            return id.Count > 0 ? "Users retrieved successfully." : "No users found.";
        }




        public string get_incident(ArrayList id, ArrayList name)
        {
            // Define the query to get incident titles and IDs
            string query = "SELECT id, incident_title FROM incidents";

            try
            {
                using (var connection = new SqlConnection(secure_connection()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Execute the command
                        using (var reader = command.ExecuteReader())
                        {
                            // Loop through each record
                            while (reader.Read())
                            {
                                // Add the id and title[name] 
                                id.Add(reader["id"].ToString());
                                name.Add(reader["incident_title"].ToString()); 
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

            // If incidents are found, then return a success message
            return id.Count > 0 ? "Incidents retrieved successfully." : "No incidents found.";
        }



        //store the tasks
        public string InsertTask(string taskName, string description, int assignedTo, int incidentId, string status, DateTime dueDate)
        {
            // SQL command to insert a new task
            string query = @"
        INSERT INTO tasks (task_name, description, assigned_to, incident_id, status, due_date)
        VALUES (@taskName, @description, @assignedTo, @incidentId, @status, @dueDate)";

            try
            {
                using (var connection = new SqlConnection(secure_connection()))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adding parameter values to avoid SQL injection
                        command.Parameters.AddWithValue("@taskName", taskName);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@assignedTo", assignedTo);
                        command.Parameters.AddWithValue("@incidentId", incidentId);
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@dueDate", dueDate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                // Handle and log the exception
                return $"Error: {e.Message}";
            }

            return "done";
        }

        //    public string GetVolunteerTasks(int userId,
        //ArrayList taskIds, ArrayList taskNames, ArrayList taskDescriptions, ArrayList dueDates,
        //ArrayList incidentIds, ArrayList incidentTitles, ArrayList incidentDates, ArrayList provinces,
        //ArrayList towns, ArrayList villages, ArrayList affectedPopulations, ArrayList incidentDescriptions,
        //ArrayList emails, ArrayList createdAts, ArrayList assignedUserIds, ArrayList assignedUserNames)
        //    {
        //        // Clear the ArrayLists to store fresh data
        //        taskIds.Clear();
        //        taskNames.Clear();
        //        taskDescriptions.Clear();
        //        dueDates.Clear();
        //        incidentIds.Clear();
        //        incidentTitles.Clear();
        //        incidentDates.Clear();
        //        provinces.Clear();
        //        towns.Clear();
        //        villages.Clear();
        //        affectedPopulations.Clear();
        //        incidentDescriptions.Clear();
        //        emails.Clear();
        //        createdAts.Clear();
        //        assignedUserIds.Clear();
        //        assignedUserNames.Clear();

        //        // Step 1: Get tasks assigned to the user
        //        string taskQuery = @"
        //SELECT 
        //    id AS TaskID,
        //    taskName,
        //    description,
        //    dueDate,
        //    incidentId,  -- Include incidentId to get incident details later
        //    assignedTo
        //FROM 
        //    tasks 
        //WHERE 
        //    assignedTo = @UserID";

        //        using (SqlConnection connection = new SqlConnection(secure_connection()))
        //        {
        //            SqlCommand taskCommand = new SqlCommand(taskQuery, connection);
        //            taskCommand.Parameters.AddWithValue("@UserID", userId);

        //            try
        //            {
        //                connection.Open();
        //                SqlDataReader taskReader = taskCommand.ExecuteReader();

        //                if (taskReader.HasRows)
        //                {
        //                    // Step 2: For each task, get incident details using incidentId
        //                    while (taskReader.Read())
        //                    {
        //                        // Add task details to the respective ArrayLists
        //                        taskIds.Add(taskReader["TaskID"].ToString());
        //                        taskNames.Add(taskReader["taskName"].ToString());
        //                        taskDescriptions.Add(taskReader["description"].ToString());
        //                        dueDates.Add(Convert.ToDateTime(taskReader["dueDate"]).ToString("yyyy-MM-dd"));

        //                        // Get the incidentId for further query
        //                        int incidentId = Convert.ToInt32(taskReader["incidentId"]);

        //                        // Add user details
        //                        assignedUserIds.Add(taskReader["assignedTo"].ToString()); // Assuming assignedTo is the user ID
        //                        assignedUserNames.Add(""); // You can populate this later if needed

        //                        // Now retrieve incident details based on incidentId
        //                        string incidentQuery = @"
        //                SELECT 
        //                    id AS IncidentID,
        //                    incident_title,
        //                    incident_date,
        //                    province,
        //                    town,
        //                    village,
        //                    affected_population,
        //                    incident_description,
        //                    email,
        //                    created_at
        //                FROM 
        //                    incidents
        //                WHERE 
        //                    id = @IncidentID";

        //                        using (SqlCommand incidentCommand = new SqlCommand(incidentQuery, connection))
        //                        {
        //                            incidentCommand.Parameters.AddWithValue("@IncidentID", incidentId);
        //                            SqlDataReader incidentReader = incidentCommand.ExecuteReader();

        //                            if (incidentReader.Read())
        //                            {
        //                                // Add incident details to the respective ArrayLists
        //                                incidentIds.Add(incidentReader["IncidentID"].ToString());
        //                                incidentTitles.Add(incidentReader["incident_title"].ToString());
        //                                incidentDates.Add(Convert.ToDateTime(incidentReader["incident_date"]).ToString("yyyy-MM-dd"));
        //                                provinces.Add(incidentReader["province"].ToString());
        //                                towns.Add(incidentReader["town"].ToString());
        //                                villages.Add(incidentReader["village"].ToString());
        //                                affectedPopulations.Add(incidentReader["affected_population"].ToString());
        //                                incidentDescriptions.Add(incidentReader["incident_description"].ToString());
        //                                emails.Add(incidentReader["email"].ToString());
        //                                createdAts.Add(Convert.ToDateTime(incidentReader["created_at"]).ToString("yyyy-MM-dd"));
        //                            }

        //                            incidentReader.Close(); // Close the incident reader
        //                        }
        //                    }

        //                    return "Tasks retrieved successfully.";
        //                }
        //                else
        //                {
        //                    return "No tasks found for this volunteer.";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                // Log or output the exception for debugging
        //                return $"Error: {ex.Message}";
        //            }
        //        }
        //    }



        public string GetVolunteerTasks(int userId,
        ArrayList taskIds, ArrayList taskNames, ArrayList taskDescriptions, ArrayList dueDates,
        ArrayList incidentIds, ArrayList incidentTitles, ArrayList incidentDates,
        ArrayList provinces, ArrayList towns, ArrayList villages, ArrayList affectedPopulations,
        ArrayList incidentDescriptions, ArrayList emails, ArrayList createdAts,
        ArrayList assignedUserIds, ArrayList assignedUserNames)
        {
            // Clear the ArrayLists to store fresh data
            taskIds.Clear();
            taskNames.Clear();
            taskDescriptions.Clear();
            dueDates.Clear();
            incidentIds.Clear();
            incidentTitles.Clear();
            incidentDates.Clear();
            provinces.Clear();
            towns.Clear();
            villages.Clear();
            affectedPopulations.Clear();
            incidentDescriptions.Clear();
            emails.Clear();
            createdAts.Clear();
            assignedUserIds.Clear();
            assignedUserNames.Clear();

            // SQL query to get tasks assigned to the user and join with incidents
            string query = @"
    SELECT 
        t.id AS TaskID,
        t.task_name,
        t.description,
        t.assigned_to,
        t.incident_id,
        t.created_at AS task_created_at,
        t.due_date,
        t.status,
        i.id AS IncidentID,
        i.incident_title,
        i.incident_date,
        i.province,
        i.town,
        i.village,
        i.affected_population,
        i.incident_description,
        i.email,
        i.created_at AS incident_created_at
    FROM 
        tasks t
    JOIN 
        incidents i ON t.incident_id = i.id
    WHERE 
        t.assigned_to = @UserID";  // Filter by assigned_to

            using (SqlConnection connection = new SqlConnection(secure_connection()))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", userId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Add task details to the respective ArrayLists
                            taskIds.Add(reader["TaskID"].ToString());
                            taskNames.Add(reader["task_name"].ToString());
                            taskDescriptions.Add(reader["description"].ToString());
                            dueDates.Add(Convert.ToDateTime(reader["due_date"]).ToString("yyyy-MM-dd"));

                            // Add incident details
                            string incidentId = reader["IncidentID"].ToString();
                            incidentIds.Add(incidentId);
                            incidentTitles.Add(reader["incident_title"].ToString());
                            incidentDates.Add(Convert.ToDateTime(reader["incident_date"]).ToString("yyyy-MM-dd"));
                            provinces.Add(reader["province"].ToString());
                            towns.Add(reader["town"].ToString());
                            villages.Add(reader["village"].ToString());
                            affectedPopulations.Add(reader["affected_population"].ToString());
                            incidentDescriptions.Add(reader["incident_description"].ToString());
                            emails.Add(reader["email"].ToString());
                            createdAts.Add(Convert.ToDateTime(reader["incident_created_at"]).ToString("yyyy-MM-dd"));

                            // Add assigned user details
                            assignedUserIds.Add(reader["assigned_to"].ToString());
                            assignedUserNames.Add("Owen"); // Set the assigned user name to "Owen"
                        }

                        return "Tasks retrieved successfully.";
                    }
                    else
                    {
                        return "No tasks found for this volunteer.";
                    }
                }
                catch (Exception ex)
                {
                    // Log or output the exception for debugging
                    return $"Error: {ex.Message}";
                }
            }
        }


        //get the id
        public int get_id(string? email)
        {
            string query = "SELECT id FROM users WHERE email = @Email";
            int id = 0;

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
                                id = reader.GetInt32(0);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return -1;
            }

            return id;
        }



    }
}
