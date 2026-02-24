using Domain.Entities;
using Domain.Interfaces;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;
        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Store AddStore(AddStoreDTO store)
        {
            var newStore = new Store
            {
                Name = store.Name,
                Description = store.Description,
                LogoUrl = store.LogoUrl,
                BannerUrl = store.BannerUrl,
                PhoneNumber = store.PhoneNumber,
                Address = store.Address,
                OwnerId = store.OwnerId,
                ApprovalStatus = store.ApprovalStatus,
                IsActive = store.IsActive,
                CreatedAt = store.CreatedAt,
                AverageRating = store.AverageRating,

            };
            _context.Stores.Add(newStore);
            _context.SaveChanges();
            return newStore;
        }
        public GetStoreDTO GetStore(int id)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);
            if (store == null)
            {
                return null;
            }
            return new GetStoreDTO
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                LogoUrl = store.LogoUrl,
                BannerUrl = store.BannerUrl,
                PhoneNumber = store.PhoneNumber,
                Address = store.Address,
                IsActive = store.IsActive,
                CreatedAt = store.CreatedAt,
                OwnerId = store.OwnerId,
                ApprovalStatus = store.ApprovalStatus,
                AverageRating = store.AverageRating,
            };
        }
        public bool UpdateStoreStatus(int id, string NewStatus)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);
            if (store == null)
            {
                return false;
            }
            if (NewStatus == "Active")
            {
                store.IsActive = true;
            }
            else if (NewStatus == "Inactive")
            {
                store.IsActive = false;
            }
            else if (NewStatus == "Blocked")
            {
                store.IsBlocked = true;
                store.IsActive = false;
            }
            int res = _context.SaveChanges();
            return res > 0;
        }
        public GetStoreDTO GetStoreByOwnerId(int OwnerId)
        {
            var store = _context.Stores.FirstOrDefault(s => s.OwnerId == OwnerId);
            if (store == null)
            {
                return null;
            }
            return new GetStoreDTO
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                LogoUrl = store.LogoUrl,
                BannerUrl = store.BannerUrl,
                PhoneNumber = store.PhoneNumber,
                Address = store.Address,
                IsActive = store.IsActive,
                CreatedAt = store.CreatedAt,
                OwnerId = store.OwnerId,
                ApprovalStatus = store.ApprovalStatus,
                AverageRating = store.AverageRating,
            };
        }
        public IEnumerable<GetStoreDTO> GetAllStores()
        {
            var store = _context.Stores.Include(s => s.Owner).ToList();
            return store.Select(store => new GetStoreDTO
            {
                Id = store.Id,
                Name = store.Name,
                Description = store.Description,
                LogoUrl = store.LogoUrl,
                BannerUrl = store.BannerUrl,
                PhoneNumber = store.PhoneNumber,
                Address = store.Address,
                IsActive = store.IsActive,
                CreatedAt = store.CreatedAt,
                OwnerId = store.OwnerId,
                ApprovalStatus = store.ApprovalStatus,
                OwnerEmail = store.Owner.Email,
                OwnerName = store.Owner.Name,

            }).ToList();
        }
        // Services/StoreService.cs
        public int UpdateStore(UpdateStoreDTO store)
        {
            var existingStore = _context.Stores.FirstOrDefault(s => s.Id == store.Id);

            if (existingStore == null)
            {
                throw new Exception("Store not found");
            }

            if (store.Name != null)
            {
                existingStore.Name = store.Name;
            }

            if (store.Description != null)
            {
                existingStore.Description = store.Description;
            }

            if (store.LogoUrl != null)
            {
                existingStore.LogoUrl = store.LogoUrl;
            }

            if (store.BannerUrl != null)
            {
                existingStore.BannerUrl = store.BannerUrl;
            }

            if (store.PhoneNumber != null)
            {
                existingStore.PhoneNumber = store.PhoneNumber;
            }

            if (store.Address != null)
            {
                existingStore.Address = store.Address;
            }
            existingStore.IsActive = store.IsActive;


            return _context.SaveChanges();
        }
        public int DeleteStore(int id)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);
            if (store == null)
            {
                throw new Exception("Store not found");
            }

            var owner = _context.Users.FirstOrDefault(u => u.Id == store.OwnerId);


            _context.Stores.Remove(store);


            if (owner != null)
            {
                // Check if user owns any other stores (optional but recommended)
                bool ownsOtherStores = _context.Stores.Any(s => s.OwnerId == owner.Id && s.Id != store.Id);
                if (!ownsOtherStores)
                {
                    _context.Users.Remove(owner);
                }
            }

            return _context.SaveChanges();
        }
        public int UpdateStoreApprovalStatus(int id, string approvalStatus)
        {
            var store = _context.Stores.FirstOrDefault(s => s.Id == id);
            if (store == null)
            {
                throw new Exception("Store not found");
            }
            store.ApprovalStatus = approvalStatus;
            return _context.SaveChanges();
        }
    }
}
