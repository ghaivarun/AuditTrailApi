using AuditTrailApi.Models;

namespace AuditApi.Models
{
    public class AuditResponse
    {
        public string EntityName { get; set; }
        public string UserId { get; set; }
        public AuditAction Action { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, ChangeDetail> Changes { get; set; }
    }

    public class ChangeDetail
    {
        public string Before { get; set; }
        public string After { get; set; }
    }
}
