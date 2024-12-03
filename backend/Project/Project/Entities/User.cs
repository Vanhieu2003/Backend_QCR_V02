using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class User
    {
        public User()
        {
            FormTemplateProcessApprovers = new HashSet<FormTemplateProcessApprover>();
            FormTemplateWatchers = new HashSet<FormTemplateWatcher>();
            RequestFormActivityLogs = new HashSet<RequestFormActivityLog>();
            RequestFormCommunicates = new HashSet<RequestFormCommunicate>();
            RequestFormProcessApprovers = new HashSet<RequestFormProcessApprover>();
            RequestFormWatchers = new HashSet<RequestFormWatcher>();
            RequestForms = new HashSet<RequestForm>();
            UserDepartments = new HashSet<UserDepartment>();
            UserGroups = new HashSet<UserGroup>();
            UserRequestBases = new HashSet<UserRequestBasis>();
        }

        public string Id { get; set; } = null!;
        public string? EmployeeCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsVerified { get; set; }
        public string? ExternalId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<FormTemplateProcessApprover> FormTemplateProcessApprovers { get; set; }
        public virtual ICollection<FormTemplateWatcher> FormTemplateWatchers { get; set; }
        public virtual ICollection<RequestFormActivityLog> RequestFormActivityLogs { get; set; }
        public virtual ICollection<RequestFormCommunicate> RequestFormCommunicates { get; set; }
        public virtual ICollection<RequestFormProcessApprover> RequestFormProcessApprovers { get; set; }
        public virtual ICollection<RequestFormWatcher> RequestFormWatchers { get; set; }
        public virtual ICollection<RequestForm> RequestForms { get; set; }
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserRequestBasis> UserRequestBases { get; set; }
    }
}
