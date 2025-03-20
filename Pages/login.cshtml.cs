using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS.Pages
{
    public class loginModel : PageModel
    {
        //class login and register instance
        login_and_register logins = new login_and_register();

        //variable declaration
        public String ErrorMessage = "";
        public String email_error = "";
        public void OnPost()
        {
            // Retrieve email and password from the form
            string email = Request.Form["email"];
            string password = Request.Form["password"];


            if (email=="Admin@gmail.com" &&password=="password")
            {

                HttpContext.Session.SetString("user_email", "Admin@gmail.com");
                HttpContext.Session.SetString("user_name", "Admin");
                HttpContext.Session.SetString("user_gender", "both");
                HttpContext.Session.SetString("user_role", "Admin");
                HttpContext.Session.SetString("user_phone", "0661887078");
                Response.Redirect("/Tasks");

            }
            else {
                //if error happens user must not re-enter the email address
                email_error = email;

                // Initialize the error message
                ErrorMessage = string.Empty;

                // Check if email and password are provided
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    string result = logins.LoginUser(email, password);
                    if (result != null)
                    {
                        // Login successful, result contains "email,name,gender" but split by , coma
                        string[] userDetails = result.Split(',');
                        string userEmail = userDetails[0];
                        string userName = userDetails[1];
                        string userGender = userDetails[2];
                        string userRole = userDetails[3];
                        string userphone = userDetails[4];
                        //clear and assign the session
                        ErrorMessage = "";
                        HttpContext.Session.SetString("user_email", userEmail);
                        HttpContext.Session.SetString("user_name", userName);
                        HttpContext.Session.SetString("user_gender", userGender);
                        HttpContext.Session.SetString("user_role", userRole);
                        HttpContext.Session.SetString("user_phone", userphone);
                        //then open the home page
                        Response.Redirect("/home");
                    }
                    else
                    {
                        ErrorMessage = "Invalid email or password.";
                    }

                }

                else
                {
                    ErrorMessage = "Both Email and Password fields are required.";
                }
            }
        }

        public void OnGet()
        {
        }
    }
}
