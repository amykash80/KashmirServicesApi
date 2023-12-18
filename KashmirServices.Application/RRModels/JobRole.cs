using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.RRModels
{
    public class JobRoleRequest
    {
        public Guid CategoryId { get; set; }


        public Guid EngineerId { get; set; }
    }

    public class JobRoleUpdateRequest : JobRoleRequest
    {
        public Guid Id { get; set; }
    }

    public class JobRolesResponse
    {
        public Guid CategoryId { get; set; }

        public string JobRole { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid ManagerId { get; set; }

        public string ManagerName { get; set; } = string.Empty;

        public string ManagerPhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }


    public class MyEngineersJobRoleResponse
    {
        public Guid CategoryId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string JobRole { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid ManagerId { get; set; }
    }

}
