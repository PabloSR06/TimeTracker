﻿using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.User
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public IList<RoleModel> Roles { get; set; } = new List<RoleModel>();
    }
}