using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ISyncService
    {
        Task<HttpResponseMessage> AddSession(Session id);
        Task<HttpResponseMessage> DeleteSession(Guid id);
        Task<HttpResponseMessage> UseSession(Guid id);
    }
}
