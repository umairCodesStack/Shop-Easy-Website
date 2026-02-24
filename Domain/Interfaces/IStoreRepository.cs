using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStoreRepository
    {
        public Store AddStore(AddStoreDTO store);
        public GetStoreDTO GetStore(int id);
        public IEnumerable<GetStoreDTO> GetAllStores();
        public int UpdateStore(UpdateStoreDTO store);
        public int DeleteStore(int id);
        public int UpdateStoreApprovalStatus(int id, string approvalStatus);
        public GetStoreDTO GetStoreByOwnerId(int OwnerId);
        public bool UpdateStoreStatus(int id, string NewStatus);
    }
}
