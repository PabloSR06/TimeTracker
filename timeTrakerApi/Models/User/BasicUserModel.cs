﻿using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.User
{
    public class BasicUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
