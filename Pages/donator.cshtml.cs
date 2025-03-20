
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS.Pages
{
    public class donatorModel : PageModel
    {

        Donations store = new Donations();
        public string message = "";
        public void OnPost()
        {
            string donorName = Request.Form["name"];
            string donorEmail = Request.Form["email"];
            string phoneNumber = Request.Form["phone"];
            string donationType = Request.Form["donation_type"];
            string dateDonated = DateTime.Now.ToString("yyyy-MM-dd");
            string timeDonated = DateTime.Now.ToString("HH:mm:ss");
            string gender = HttpContext.Session.GetString("user_gender") ?? "Unknown";
            string role = HttpContext.Session.GetString("user_role") ?? "Unknown";
            string donationDetails = Request.Form["description"];

            // Call the method to insert donation details
            string resultMessage = store.InsertDonation( donorName, donorEmail, phoneNumber, donationType,dateDonated, timeDonated,gender, role,donationDetails);

            if (resultMessage == "Donation successfully recorded.")
            {
                message = resultMessage;

            }
            else if (resultMessage != "Donation successfully recorded.")
            {
                message = "something went wrong try to donate later";
            }
        }
        public void OnGet()
        {
        }
    }
}
