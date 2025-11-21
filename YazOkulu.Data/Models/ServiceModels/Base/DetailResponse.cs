namespace YazOkulu.Data.Models.ServiceModels.Base
{
    public class DetailResponse<T>
    {
        public required T Detail { get; set; }
    }
}