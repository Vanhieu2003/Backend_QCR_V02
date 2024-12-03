using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Staff
    {
        public string Id { get; set; } = null!;
        public string? EmployeeCode { get; set; }
        public string? TeacherName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public string? Rfid { get; set; }
    }
}
