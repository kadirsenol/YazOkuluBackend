namespace YazOkulu.Data.Models.ServiceModels.Base
{
    public class SearchResponse<T>
    {
        public SearchResponse() { SearchResult = []; }
        public List<T> SearchResult { get; set; }
        public int TotalItemCount { get; set; }
    }
}