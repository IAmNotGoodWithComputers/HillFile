using System;
using Microsoft.AspNetCore.Identity;

namespace HillFile.DbAccess.Entities
{
    public class Store
    {
        public Guid Id { get; set; }
        public IdentityUser User { get; set; }
    }
}