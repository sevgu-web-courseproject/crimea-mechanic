using System;
using DataAccessLayer.Models;

namespace BusinessLogic.UnitTests.Utils
{
    public static class WorkTagsUtils
    {
        public static WorkTag Create(long id, string name, bool isDeleted = false)
        {
            return new WorkTag
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
