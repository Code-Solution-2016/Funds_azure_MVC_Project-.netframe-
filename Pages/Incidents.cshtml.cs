using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS.Pages
{
    public class IncidentsModel : PageModel
    {

        Incidents get_info = new Incidents();
        public string message = "";
        public void OnPost()
        {
            // Retrieve form values using Request.Form
            var title = Request.Form["incidentTitle"].ToString();
            var date = Request.Form["incidentDate"].ToString();
            var province = Request.Form["province"].ToString();
            var town = Request.Form["town"].ToString();
            var village = Request.Form["village"].ToString();
            var affectedPopulation = Request.Form["affectedPopulation"].ToString();
            var description = Request.Form["incidentDescription"].ToString();
            var email = HttpContext.Session.GetString("user_email");

            // Create an instance of the Incidents class
            var get_info = new Incidents();

            // Call the InsertIncident method with the form data
           message =  get_info.InsertIncident(title, date, province, town, village, affectedPopulation, description, email);

            
        }
        public void OnGet()
        {



        }
    }
}
