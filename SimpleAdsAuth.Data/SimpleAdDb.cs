using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SimpleAdsAuth.Data
{
    public class SimpleAdDb
    {
        private readonly string _connectionString;

        public SimpleAdDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddSimpleAd(SimpleAd ad)
        {
            using var context = new SimpleAdsDataContext(_connectionString);
            context.Ads.Add(ad);
            context.SaveChanges();
        }

        public List<SimpleAd> GetAds()
        {
            using var context = new SimpleAdsDataContext(_connectionString);
            return context.Ads.OrderByDescending(a => a.CreatedDate).Include(a => a.User).ToList();
        }

        public List<SimpleAd> GetAdsForUser(int userId)
        {
            using var context = new SimpleAdsDataContext(_connectionString);
            return context.Ads.Include(a => a.User).Where(a => a.UserId == userId).ToList();
        }

        public int GetUserIdForAd(int adId)
        {
            using var context = new SimpleAdsDataContext(_connectionString);
            return context.Ads.Where(a => a.Id == adId).Select(a => a.UserId).FirstOrDefault();
        }

        public void Delete(int id)
        {
            using var context = new SimpleAdsDataContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM Ads WHERE Id = {id}");
        }

        public bool IsEmailAvailable(string email)
        {
            using var context = new SimpleAdsDataContext(_connectionString);
            return context.Users.All(u => u.Email != email);
        }
    }
}