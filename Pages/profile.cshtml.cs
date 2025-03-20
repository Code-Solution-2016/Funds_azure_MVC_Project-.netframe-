using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections;
namespace appr_poe_part_2_owen_muhluri_ndimande_GIFT_OF_GIVERS.Pages
{
    public class profileModel : PageModel
    {
        //variables declarations
        public string Name = string.Empty;
        public string Email = string.Empty;
        public string PhoneNumber = string.Empty;
        public string Role = string.Empty;
        public string Gender = string.Empty;
        user_info Get_user_Info = new user_info();

        public void OnGet()
        {

            //on get method to get information
            string user_active = HttpContext.Session.GetString("user_email");
         string hold = Get_user_Info.get_user(user_active);


            //then get the user info
            string[] found = hold.Split(',');
            Email = found[0];
            Name = found[1];
            Gender = found[2];
            Role = found[3];
            PhoneNumber = found[4];






        }
    }
}
