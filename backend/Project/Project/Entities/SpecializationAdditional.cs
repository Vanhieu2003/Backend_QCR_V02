using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class SpecializationAdditional
    {
        public string SpecializationId { get; set; } = null!;
        public DateTime? Year { get; set; }
        public string? Periods { get; set; }
        public string? PeriodId { get; set; }
        public int? NumberOfStudent { get; set; }
        public int? NumberOfPeriod { get; set; }

        public virtual Specialization Specialization { get; set; } = null!;
    }
}
