using System.Collections.Generic;
using AutoMapper;
using BookArena.Model;

namespace BookArena.Web.Helper
{
    public class Utility
    {
        public static Response AccessDeniedResponse()
        {
            return new Response
            {
                ResponseType = ResponseType.Error,
                Message = "Access Denied! You need to login first!",
            };
        }
    }

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