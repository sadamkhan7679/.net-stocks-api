using api.Data;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        return Ok(_dbContext.Stocks.ToList().Select(stock=>stock.ToStockDto()));
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);
        if(stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] DTOs.Stock.StockDto createStockRequestDto)
    
    {
        var stock = createStockRequestDto.ToStockModel();
        _dbContext.Stocks.Add(stock);
        _dbContext.SaveChanges();
        // return CreatedAtRoute("GetStock", new {id = stock.Id}, stock.ToStockDto());
        return CreatedAtAction(nameof(GetById), new {id = stock.Id}, stock.ToStockDto());
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStock([FromRoute] int id, [FromBody] DTOs.Stock.UpdateStockRequestDto updateStockRequestDto)
    {
        var stock = _dbContext.Stocks.Find(id);
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
        
        _dbContext.SaveChanges();
        return Ok(stock.ToStockDto());
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStock([FromRoute] int id)
    {
        var stock = _dbContext.Stocks.Find(id);
        if(stock == null)
        {
            return NotFound();
        }
        _dbContext.Stocks.Remove(stock);
        _dbContext.SaveChanges();
        return NoContent();
    }
    
    
    
    
    
    
}