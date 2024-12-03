using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Student
    {
        public string Id { get; set; } = null!;
        public string? StudentCode { get; set; }
        public string? StudentName { get; set; }
        public string? Department { get; set; }
        public string? SchoolYear { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Rfid { get; set; }
    }
}
