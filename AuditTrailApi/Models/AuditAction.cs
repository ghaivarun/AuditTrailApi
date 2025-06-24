using System.Text.Json.Serialization;

namespace AuditTrailApi.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AuditAction
{
    Created,
    Updated,
    Deleted
}
