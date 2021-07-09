using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core.Entities
{
    public static class AuditableEntityFields
    {
        public static string CreateOn = "CreateOn";
        public static string UpdateOn = "UpdateOn";
        public static string CreateBy = "CreateBy";
        public static string UpdateBy = "UpdateBy";
    }
}
