using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{
    
    // Define the methods that will be implemented in the StockRepository
    
    // Get all stocks. This will return a list of stocks
    Task<List<Stock>> GetAllStocksAsync();
    
    /* Get a stock by its id. This will return a stock. The id will be passed as a parameter
     Stock? is used to return a stock or null if the stock is not found. This is a nullable type
     */
    Task<Stock?> GetStockByIdAsync(int id);
    
    // Add a stock. This will return a stock
    Task<Stock> CreateStockAsync(Stock stockModel);
    
    /* Update a stock. This will return a stock.
        The stock will be passed as a parameter
        Stock? is used to return a stock or null if the stock is not found. This is a nullable type
     */
    Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockRequestDto);
    
    /* Delete a stock. This will return a stock.
        The id will be passed as a parameter
        Stock? is used to return a stock or null if the stock is not found. This is a nullable type
     */
    Task<Stock?> DeleteStockAsync(int id);
    
    
}