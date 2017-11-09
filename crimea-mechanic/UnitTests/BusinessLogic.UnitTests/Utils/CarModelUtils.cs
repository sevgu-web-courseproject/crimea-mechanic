using System;
using DataAccessLayer.Models;

namespace BusinessLogic.UnitTests.Utils
{
    public static class CarModelUtils
    {
        public static CarModel Create(long id, string name, bool isDeleted = false)
        {
            return new CarModel
            {
                Id = id,
                Name = name,
                IsDeleted = isDeleted,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
        } 
    }
}
