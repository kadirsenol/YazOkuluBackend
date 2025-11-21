using YazOkulu.Core.Enums;
namespace YazOkulu.Data.Models.ServiceModels.Base
{
    public class SearchRequest
    {
        public int? CompanyID { get; set; }
        public int LanguageID { get; set; }
        public int Page { get; set; } = 1;
        public string SearchText { get; set; } = string.Empty;
        public PageSizeEnum PageSize { get; set; } = PageSizeEnum.Unlimited;
        #region OrderBy
        public string? OrderBy { get; set; } = string.Empty;
        protected virtual HashSet<string> AllowedOrderFields { get; }
        public bool IsValidOrderBy()
        {
            if (string.IsNullOrWhiteSpace(OrderBy)) return true;
            var fields = OrderBy.Split(',').Select(o => o.Trim().Split(' ')[0]).ToList(); 
            var data = fields.All(field => AllowedOrderFields.Contains(field));
            return data;
        }
        #endregion
        #region Cache
        public virtual string CacheParameters { get; set; } = string.Empty;
        public virtual string CreateCacheParameter(string key = "search") => CacheParameters = $"YazOkulu_CK:C_{CompanyID}:L_{LanguageID}:P_{Page}:PS_{PageSize}";
        #endregion
    }
}