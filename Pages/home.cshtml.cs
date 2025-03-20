using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS.Pages
{

    public class homeModel : PageModel
    {

       
        public string message = "";

        public void OnGet()
        { 
            //// Check if session contains the "connected" key
            //if (HttpContext.Session.GetString("user_id") == null)
            //{
            //    // If not connected, perform the connection
            //    message = one.connect();
            //    // Store connection status in session
            //    HttpContext.Session.SetString("user_id", message);
            //}
            //else
            //{
            //    // If already connected, retrieve message from session
            //    message = HttpContext.Session.GetString("user_id");
            //}


        }




    }
}
