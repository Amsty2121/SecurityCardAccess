using Application.Configurations;
using Application.IServices;
using Domain.Entities;
using Domain.Models;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Services
{
    public class SyncService : ISyncService
    {
        private readonly string _origin;
        private readonly SyncServiceSettings _syncService;

        public SyncService(IHttpContextAccessor httpContextAccessor, SyncServiceSettings syncService)
        {
            _origin = httpContextAccessor.HttpContext.Request.Host.ToString();
            _syncService = syncService;
        }

        public async Task<HttpResponseMessage> AddSession(Session session)
        {
            var json = JsonSerializer.Serialize(new SyncEntity
            {
                JsonData = JsonSerializer.Serialize(session),
                SyncType = "GET",
                Id = session.Id,
                LastChangeAt = DateTime.UtcNow,
                Origin = _origin + "/api",
                OperationUrl = "/session/close"
            });

            return await HttpClientUtility.SendJsonAsync(json, _syncService.Host, "POST");
        }

        public async Task<HttpResponseMessage> DeleteSession(Guid id)
        {
            var json = JsonSerializer.Serialize(new SyncEntity
            {
                JsonData = JsonSerializer.Serialize(new { Id = id }),
                SyncType = "GET",
                Id = id,
                LastChangeAt = DateTime.UtcNow,
                Origin = _origin + "/api",
                OperationUrl = "/session/close"
            });
            return await HttpClientUtility.SendJsonAsync(json, _syncService.Host, "POST");
        }
        public async Task<HttpResponseMessage> UseSession(Guid id)
        {
            var json = JsonSerializer.Serialize(new SyncEntity
            {
                JsonData = JsonSerializer.Serialize(new { Id = id }),
                SyncType = "GET",
                Id = id,
                LastChangeAt = DateTime.UtcNow,
                Origin = _origin + "/api",
                OperationUrl = "/session/close"
            });

            return await HttpClientUtility.SendJsonAsync(json, _syncService.Host, "POST");
        }
    }
}
