using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLogic.UnitTests.Utils
{
    public static class CarMarksUtils
    {
        public static CarMark Create(long id, string name, bool isDeleted = false)
        {
            return new CarMark
            {
                Id = id,
                Name = name,
                IsDeleted = isDeleted,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Models = new List<CarModel>()
            };
        }

        public static CarMark CreateWithModels(long id, string name, bool isDeleted, List<string> modelsName)
        {
            var carMark = Create(id, name, isDeleted);
            var modelId = 1;
            foreach (var model in modelsName) {
                carMark.Models.Add(CarModelUtils.Create(modelId, model));
                modelId++;
            }
            return carMark;
        }
    }
}
