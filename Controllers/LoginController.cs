﻿using atc_backend_app.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace atc_backend_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [Route("login")]
        public string Login(Login login)
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("UserRegistration").ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM UserManagement WHERE Username = '" + login.Username + "' AND Password = '" + login.Password + "'", conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }
    }
}