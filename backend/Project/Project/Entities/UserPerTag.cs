﻿using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class UserPerTag
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? TagId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
