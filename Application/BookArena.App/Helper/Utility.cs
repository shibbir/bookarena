using System.Collections.Generic;
using AutoMapper;

namespace BookArena.App.Helper
{
    public class Mapper<TSource, TDestination>
    {
        public static TDestination SingleMap(TSource model)
        {
            Mapper.CreateMap<TSource, TDestination>();
            var entity = Mapper.Map<TSource, TDestination>(model);

            return entity;
        }

        public static List<TDestination> ListMap(List<TSource> model)
        {
            Mapper.CreateMap<TSource, TDestination>();
            var entity = Mapper.Map<List<TSource>, List<TDestination>>(model);

            return entity;
        }
    }
}