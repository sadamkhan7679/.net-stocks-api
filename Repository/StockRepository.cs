using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository: IStockRepository
{
    // Bring in the ApplicationDbContext
    private readonly ApplicationDbContext _dbContext;
    
    // Inject the ApplicationDbContext into the constructor
    public StockRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Implement the methods from the IStockRepository interface
    public Task<List<Stock>> GetAllStocksAsync(QueryObject query)
    {
        // return _dbContext.Stocks.Include(c => c.Comments).ToListAsync();
        var stocks = _dbContext.Stocks.AsQueryable();
        
        if(!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(stock => stock.Symbol.Contains(query.Symbol));
        }
        
        if(!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(stock => stock.CompanyName.Contains(query.CompanyName));
        }
        
        if(query.SortBy != null)
        {
            if(query.SortBy == SortByFields.Symbol)
            {
                stocks = query.IsAscending ? stocks.OrderBy(stock => stock.Symbol) : stocks.OrderByDescending(stock => stock.Symbol);
            }
            else if(query.SortBy == SortByFields.CompanyName)
            {
                stocks = query.IsAscending ? stocks.OrderBy(stock => stock.CompanyName) : stocks.OrderByDescending(stock => stock.CompanyName);
            }
        }
        
        
        return stocks.ToListAsync();
    }

    public Task<Stock?> GetStockByIdAsync(int id)
    {
        return _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
    }
    
    public async Task<Stock> CreateStockAsync(Stock stockModel)
    {
        await _dbContext.Stocks.AddAsync(stockModel);
        await _dbContext.SaveChangesAsync();
        return stockModel;
    }
    
    public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockRequestDto)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if(stock == null)
        {
            return null;
        }
        stock.Symbol = stockRequestDto.Symbol;
        stock.CompanyName = stockRequestDto.CompanyName;
        stock.Purchase = stockRequestDto.Purchase;
        stock.LastDiv = stockRequestDto.LastDiv;
        stock.Industry = stockRequestDto.Industry;
        stock.MarketCap = stockRequestDto.MarketCap;
        await _dbContext.SaveChangesAsync();
        return stock;
    }
    
    public async Task<Stock?> DeleteStockAsync(int id)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if(stock == null)
        {
            return null;
        }
        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();
        return stock;
    }
    
    
    public Task<bool> StockExistsAsync(int id)
    {
        return _dbContext.Stocks.AnyAsync(stock => stock.Id == id);
    }
}