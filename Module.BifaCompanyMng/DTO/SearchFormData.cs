namespace Module.BifaCompanyMng.DTO
{
    public class SearchFormData
    {
        public System.Collections.Generic.List<BifaCompanySearchResult> Data { get; set; }

        public SearchFormData()
        {
            Data = new System.Collections.Generic.List<BifaCompanySearchResult>();
        }
    }
}
