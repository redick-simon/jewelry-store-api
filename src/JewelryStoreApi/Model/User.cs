using JewelryStoreApi.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace JewelryStoreApi
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public UserType UserType { get; set; }
    }
}
