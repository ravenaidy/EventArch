using System;

namespace LoginService.Models
{
    public class Login
    {
        public int LoginId { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}