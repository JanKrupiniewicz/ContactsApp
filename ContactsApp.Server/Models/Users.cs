﻿using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Server.Models
{
    /// <summary>
    /// Reprezentuje użytkownika w systemie.
    /// </summary>
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public List<Contacts> Contacts { get; set; }
    }
}
