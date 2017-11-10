using System;
using DataAccessLayer.Models;

namespace BusinessLogic.UnitTests.Utils
{
    public static class CityUtils
    {
        public static City Create(long id, string name, bool isDeleted = false)
        {
            return new City
            {
                Updated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                IsDeleted = isDeleted,
                Name = name,
                Id = id
            };
        }
    }
}
