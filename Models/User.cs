using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace atc_backend_app.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string[] Roles { get; set; }
    }
    public class UserDto { 
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
