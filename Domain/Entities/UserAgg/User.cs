using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.UserAgg
{
    public class User : BaseEntity, IDeletableEntity, IAuditableEntity
    {
        public bool IsDeleted { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
