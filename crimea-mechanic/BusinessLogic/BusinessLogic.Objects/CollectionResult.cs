using System.Collections.Generic;

namespace BusinessLogic.Objects
{
    public class CollectionResult<T> where T : class 
    {
        public int ItemsCount { get; set; }
        public IEnumerable<T> Items { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
