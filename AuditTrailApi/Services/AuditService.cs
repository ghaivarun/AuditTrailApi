using AuditApi.Models;
using AuditTrailApi.Models;
using System.Text.Json;

namespace AuditTrailApi.Services
{
    public class AuditTrailService
    {
        public AuditResponse GenerateAuditTrail(AuditRequest request)
        {
            var changes = new Dictionary<string, ChangeDetail>();

            var beforeDict = request.Before != null
                ? JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(request.Before.ToString()!)
                : null;

            var afterDict = request.After != null
                ? JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(request.After.ToString()!)
                : null;

            if (request.Action == AuditAction.Updated && beforeDict != null && afterDict != null)
            {
                foreach (var kvp in beforeDict)
                {
                    if (afterDict.ContainsKey(kvp.Key))
                    {
                        var beforeValue = kvp.Value.ToString() ?? "null";
                        var afterValue = afterDict[kvp.Key].ToString() ?? "null";

                        if (beforeValue != afterValue)
                        {
                            changes[kvp.Key] = new ChangeDetail
                            {
                                Before = beforeValue,
                                After = afterValue
                            };
                        }
                    }
                }
            }
            else if (request.Action == AuditAction.Created && afterDict != null)
            {
                foreach (var kvp in afterDict)
                {
                    changes[kvp.Key] = new ChangeDetail
                    {
                        Before = "null",
                        After = kvp.Value.ToString() ?? "null"
                    };
                }
            }
            else if (request.Action == AuditAction.Deleted && beforeDict != null)
            {
                foreach (var kvp in beforeDict)
                {
                    changes[kvp.Key] = new ChangeDetail
                    {
                        Before = kvp.Value.ToString() ?? "null",
                        After = "null"
                    };
                }
            }

            return new AuditResponse
            {
                EntityName = request.EntityName,
                UserId = request.UserId,
                Action = request.Action,
                Timestamp = DateTime.UtcNow,
                Changes = changes
            };
        }
    }
}
