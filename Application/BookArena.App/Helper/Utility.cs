using System.Collections.Generic;
using AutoMapper;

namespace BookArena.App.Helper
{
    public class Mapper<TSource, TDestination>
    {
        public static TDestination SingleMap(TSource model)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestination>(); });

            var mapper = config.CreateMapper();

            return mapper.Map<TSource, TDestination>(model);
        }

        public static List<TDestination> ListMap(List<TSource> model)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TSource, TDestination>(); });

            var mapper = config.CreateMapper();

            return mapper.Map<List<TSource>, List<TDestination>>(model);
        }
    }
}