namespace api.Helpers;

public enum SortByFields
{
    Symbol,
    CompanyName
}
public class QueryObject
{
    public string Symbol { get; set; } = String.Empty;
    public string CompanyName { get; set; } = String.Empty;
    
    public SortByFields? SortBy { get; set; }  = null;
    
    public bool IsAscending { get; set; } = false;
    
    // Add Pagination properties
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    // Add A Constructor
    public QueryObject( string symbol, string companyName, SortByFields? sortBy, bool isAscending, int page, int pageSize)
    {
        Symbol = symbol;
        CompanyName = companyName;
        SortBy = sortBy;
        IsAscending = isAscending;
        Page = page;
        PageSize = pageSize;
    }
}