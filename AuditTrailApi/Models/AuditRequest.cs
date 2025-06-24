using AuditTrailApi.Models;

namespace AuditApi.Models
{
    public class AuditRequest
    {
        public object? Before { get; set; }
        public object? After { get; set; }
        public string UserId { get; set; }
        public string EntityName { get; set; }
        public AuditAction Action { get; set; }
    }
}
