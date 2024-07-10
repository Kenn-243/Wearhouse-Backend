namespace Wearhouse.Models.Result
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }
        public string RequestMethod { get; set; }
        public T Data { get; set; }
    }
}
