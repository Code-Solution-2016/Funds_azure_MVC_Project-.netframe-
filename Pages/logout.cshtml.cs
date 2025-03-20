using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS.Pages
{
    public class logoutModel : PageModel
    {
        public void OnGet()
        {


            //clear the session
            HttpContext.Session.Clear();
        }
    }
}
