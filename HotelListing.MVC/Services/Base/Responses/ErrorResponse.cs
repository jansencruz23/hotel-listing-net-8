namespace HotelListing.MVC.Services.Base.Responses
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public List<string> Errors { get; set; }
    }
}
