using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application
{
    public class StoreService
    {
        private readonly IStoreRepository _repo;
        public StoreService(IStoreRepository repo)
        {
            _repo = repo;
        }
        public Store AddStore(AddStoreDTO store)
        {
            return _repo.AddStore(store);
        }
        public GetStoreDTO GetStore(int id)
        {
            return _repo.GetStore(id);
        }
        public IEnumerable<GetStoreDTO> GetAllStores()
        {
            return _repo.GetAllStores();
        }
        public int UpdateStore(UpdateStoreDTO store)
        {
            return _repo.UpdateStore(store);
        }
        public int DeleteStore(int id)
        {
            return _repo.DeleteStore(id);
        }
        public int UpdateStoreApprovalStatus(int id, string approvalStatus)
        {
            return _repo.UpdateStoreApprovalStatus(id, approvalStatus);
        }
        public GetStoreDTO GetStoreByOwnerId(int ownerId)
        {
            return _repo.GetStoreByOwnerId(ownerId);
        }
        public bool UpdateStoreStatus(int id, string NewStatus)
        {
            return _repo.UpdateStoreStatus(id, NewStatus);
        }
    }
}
