using AuditApi.Models;
using AuditTrailApi.Models;
using AuditTrailApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuditApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly ILogger<AuditController> _logger;
        private readonly AuditTrailService _auditService;
        public AuditController(ILogger<AuditController> logger, AuditTrailService auditService)
        {
            _logger = logger;
            _auditService = auditService;
        }

        [HttpPost("log")]
        public IActionResult Log([FromBody] AuditRequest request)
        {
            if (request.Action == AuditAction.Updated)
            {
                if (request.Before == null || request.After == null)
                    return BadRequest("Both 'Before' and 'After' must be provided for update action.");
            }
            else if (request.Action == AuditAction.Created)
            {
                if (request.After == null)
                    return BadRequest("'After' must be provided for create action.");
            }
            else if (request.Action == AuditAction.Deleted)
            {
                if (request.Before == null)
                    return BadRequest("'Before' must be provided for delete action.");
            }
            else
            {
                return BadRequest("Invalid audit action.");
            }

            var result = _auditService.GenerateAuditTrail(request);
            return Ok(result);
        }
    }
}
