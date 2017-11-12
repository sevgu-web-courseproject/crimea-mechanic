using System;
using Common.Enums;
using DataAccessLayer.Models;

namespace BusinessLogic.UnitTests.Utils
{
    public static class UserCarUtils
    {
        public static UserCar CreateWithUser(string userId)
        {
            return new UserCar
            {
                User = new UserProfile
                {
                    ApplicationUser = new ApplicationUser
                    {
                        Id = userId
                    }
                }
            };
        }

        public static UserCar Create(long id, string name, string modelName, string markName, string userId, bool isDeleted = false)
        {
            var userCar = CreateWithUser(userId);
            userCar.Id = id;
            userCar.Created = DateTime.UtcNow;
            userCar.Updated = DateTime.UtcNow;
            userCar.IsDeleted = isDeleted;
            userCar.Name = name;
            userCar.FuelType = FuelType.Diesel;
            userCar.EngineCapacity = 2.0f;
            userCar.Year = 2006;
            userCar.Vin = "test";
            userCar.Model = new CarModel
            {
                Name = modelName,
                Mark = new CarMark
                {
                    Name = markName
                }
            };
            return userCar;
        }
    }
}
