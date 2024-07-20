

using api.Models;

namespace api.Mappers;

public static class StockMapper
{
    public static DTOs.Stock.StockDto ToStockDto(this Stock stock)
    {
        return new DTOs.Stock.StockDto
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
    
    public static Stock ToStockModel(this DTOs.Stock.StockDto stockDto)
    {
        return new Stock
        {
            Id = stockDto.Id,
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
    
}