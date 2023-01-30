namespace WebAPI.Helpers
{
    public class UserParams : PaginationParams
    {
        //public string OrderBy { get; set; } = "lastActive";
        public string OrderBy { get; set; } = "userName";
    }
}