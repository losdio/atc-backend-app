//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using atc_backend_app.Models;
//using System.Data.SqlClient;
//using System.Data;

//namespace atc_backend_app.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RegistrationController(IConfiguration configuration) : ControllerBase
//    {
//        private readonly IConfiguration _configuration = configuration;

//        [HttpPost]
//        [Route("register")]
//        public string Registration(User user)
//        {
//            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("UserRegistration").ToString());
//            SqlCommand cmd = new SqlCommand("INSERT INTO Users(Username, Email, PasswordHash) VALUES('" + user.Username + "','" /*+ user.Name + "','"*/ + user.Email + "','" + user.Password + "')", conn);
//            conn.Open();
//            int i = cmd.ExecuteNonQuery();
//            conn.Close();
//            if (i > 0)
//            {
//                return "Success";
//            }
//            else
//            {
//                return "";
//            }
//        }

//    }
//}
