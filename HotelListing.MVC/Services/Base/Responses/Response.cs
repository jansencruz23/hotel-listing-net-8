namespace HotelListing.MVC.Services.Base.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}
