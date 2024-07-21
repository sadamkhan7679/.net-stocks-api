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
}