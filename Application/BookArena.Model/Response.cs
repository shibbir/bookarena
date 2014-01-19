namespace BookArena.Model
{
    public class Response
    {
        public string ResponseType { get; set; }
        public string Message { get; set; }
    }

    public class ResponseType
    {
        public static string Success = "Success";
        public static string Error = "Error";
        public static string Warning = "Warning";
        public static string Info = "Info";
    }
}