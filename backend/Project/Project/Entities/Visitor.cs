using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Visitor
    {
        public string Id { get; set; } = null!;
        public string? VisitorCode { get; set; }
        public string? VisitorName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Rfid { get; set; }
    }
}
