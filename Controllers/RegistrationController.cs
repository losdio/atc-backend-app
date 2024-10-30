using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using atc_backend_app.Models;
using System.Data.SqlClient;
using System.Data;

namespace atc_backend_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [Route("registration")]
        public string Registration(Registration registration)
        {

            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("UserRegistration").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO UserManagement(Username, Name, Email, Password) VALUES('" + registration.Username + "','" + registration.Name + "','" + registration.Email + "','" + registration.Password + "')", conn);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return "Success";
            }
            else
            {
                return "";
            }
        }
        
    }
}
