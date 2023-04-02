﻿using Domain.Entities;
using Domain.Models;
using Domain.Utilities;
using HostedSupervizer.Settings;
using System.Collections.Concurrent;
using System.Text.Json;

namespace HostedSupervizer.Services
{
    public class Supervizer : IHostedService
    {
        private readonly ConcurrentDictionary<Guid, SyncEntity> _storageQueue
            = new ConcurrentDictionary<Guid, SyncEntity>();

        private object m_Locker = new object();

        private Timer _timer;
        private readonly IAPIHostSettings _hostSettings;

        public Supervizer(IAPIHostSettings hostSettings)
        {
            _hostSettings = hostSettings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoSendWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public bool Synk(SyncEntity data)
        {
            SyncEntity syncEntity = null;

            var session = JsonSerializer.Deserialize<Session>(data.JsonData);

            if(session.UserId != new Guid("00000000-0000-0000-0000-000000000000"))
            {
                return _storageQueue.TryAdd(session.Id, data);
            }

            return _storageQueue.TryRemove(session.Id, out syncEntity);
        }

        private async void DoSendWork(object state)
        {
            lock (_storageQueue)
            {
                foreach (var element in _storageQueue)
                {
                    SyncEntity syncEntity = null;

                    var session = JsonSerializer.Deserialize<Session>(element.Value.JsonData);

                    if (session.UsedUtcDate == null && session.EndUtcDate > DateTime.UtcNow)
                    {
                        continue;
                    }

                    var isPresent = _storageQueue.TryRemove(element.Key, out syncEntity);

                    CloseSession(element);
                }
            }
        }

        private bool CloseSession(KeyValuePair<Guid,SyncEntity> element)
        {
            var receiver = _hostSettings.Host;

            var session = JsonSerializer.Deserialize<Session>(element.Value.JsonData);
            
            try
            {
                var url = $"{receiver}{element.Value.OperationUrl}";
                var result = HttpClientUtility.SendJsonAsync(JsonSerializer.Serialize(new { Id = session.Id }), url, element.Value.SyncType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }
    }
}