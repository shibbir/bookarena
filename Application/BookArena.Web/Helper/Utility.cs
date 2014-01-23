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
}