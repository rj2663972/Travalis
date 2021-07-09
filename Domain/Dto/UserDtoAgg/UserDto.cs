using Domain.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto.UserDtoAgg
{
    public class UserDto: BaseDto
    {
        public bool IsDeleted { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
