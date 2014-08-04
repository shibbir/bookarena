using System;
using System.Linq;

namespace BookArena.Core
{
    public class Pagination<T> where T : class
    {
        public static PagedResult<T> GetPagedData(IQueryable<T> query, int? page, int pageSize)
        {
            var skip = (page ?? 0)*pageSize;
            var take = pageSize;

            var totalCount = query.Count();
            var entities = query.Skip(skip).Take(take).ToList();

            return new PagedResult<T>
            {
                Entities = entities,
                HasNext = (skip + 10 < totalCount),
                HasPrevious = (skip > 0),
                TotalCount = totalCount,
                TotalPage = Math.Ceiling((double) totalCount/pageSize),
                CurrentPage = (page ?? 0)
            };
        }
    }
}