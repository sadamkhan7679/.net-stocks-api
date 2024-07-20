using api.Data;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;
[Route("api/stock")]
[ApiController]
public class StockController: ControllerBase
{
    readonly ApplicationDbContext _dbContext;
    public StockController(ApplicationDbContext dbContext)
    {
       this._dbContext = dbContext;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _dbContext.Stocks.ToListAsync();
        
        var stockDtos = stocks.Select(stock => stock.ToStockDto());
        return Ok(stockDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if(stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DTOs.Stock.StockDto createStockRequestDto)
    
    {
        var stock = createStockRequestDto.ToStockModel();
        await _dbContext.Stocks.AddAsync(stock);
        await _dbContext.SaveChangesAsync();
        // return CreatedAtRoute("GetStock", new {id = stock.Id}, stock.ToStockDto());
        return CreatedAtAction(nameof(GetById), new {id = stock.Id}, stock.ToStockDto());
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] DTOs.Stock.UpdateStockRequestDto updateStockRequestDto)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if(stock == null)
        {
            return NotFound();
        }
        stock.Symbol = updateStockRequestDto.Symbol;
        stock.CompanyName = updateStockRequestDto.CompanyName;
        stock.Purchase = updateStockRequestDto.Purchase;
        stock.LastDiv = updateStockRequestDto.LastDiv;
        stock.Industry = updateStockRequestDto.Industry;
        stock.MarketCap = updateStockRequestDto.MarketCap;
        
        await   _dbContext.SaveChangesAsync();
        return Ok(stock.ToStockDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        var stock = await _dbContext.Stocks.FindAsync(id);
        if(stock == null)
        {
            return NotFound();
        }
        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    
    
    
    
    
}