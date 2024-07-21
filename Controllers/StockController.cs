using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;
[Route("api/stock")]
[ApiController]
public class StockController: ControllerBase
{
    // Bring in the ApplicationDbContext
    readonly ApplicationDbContext _dbContext;
    
    // Bring in StockRepository
    private readonly IStockRepository _stockRepository;
    
    // Inject the ApplicationDbContext into the constructor
    public StockController(ApplicationDbContext dbContext, IStockRepository stockRepository)
    {
        // Inject the ApplicationDbContext into the constructor
        _dbContext = dbContext;
        // Inject the StockRepository into the constructor
        _stockRepository = stockRepository;
    }
    
    
    // Implement the GetAll method
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Call the GetAllStocksAsync method from the StockRepository
        var stocks = await _stockRepository.GetAllStocksAsync();
        
        // Map the Stock model to the StockDto
        var stockDtos = stocks.Select(stock => stock.ToStockDto());
        
        // Return the StockDto
        return Ok(stockDtos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        // Call the GetStockByIdAsync method from the StockRepository
        var stock = await _stockRepository.GetStockByIdAsync(id);
        
        // If the stock is not found, return NotFound
        if(stock == null)
        {
            return NotFound();
        }
        
        // Map the Stock model to the StockDto
        var stockDto = stock.ToStockDto();
        
        // Return the StockDto
        return Ok(stockDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DTOs.Stock.StockDto createStockRequestDto)
    
    {
        // If the model state is not valid, return BadRequest
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // Map the StockDto to the Stock model
        var stockModel = createStockRequestDto.ToStockModel();
        
        // Call the CreateStockAsync method from the StockRepository
        var stock = await _stockRepository.CreateStockAsync(stockModel);
        
        // Map the Stock model to the StockDto
        var stockDto = stock.ToStockDto();
        
        // Return the StockDto
        return Ok(stockDto);
    }
    
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] DTOs.Stock.UpdateStockRequestDto updateStockRequestDto)
    {
        // If the model state is not valid, return BadRequest
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // Call the UpdateStockAsync method from the StockRepository
        var stock = await _stockRepository.UpdateStockAsync(id, updateStockRequestDto);
        
        // If the stock is not found, return NotFound
        if(stock == null)
        {
            return NotFound();
        }
        
        // Map the Stock model to the StockDto
        var stockDto = stock.ToStockDto();
        
        // Return the StockDto
        return Ok(stockDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        // Call the DeleteStockAsync method from the StockRepository
        var stock = await _stockRepository.DeleteStockAsync(id);
        
        // If the stock is not found, return NotFound
        if(stock == null)
        {
            return NotFound();
        }
        
        // Map the Stock model to the StockDto
        var stockDto = stock.ToStockDto();
        
        // Return the StockDto
        return Ok(stockDto);
    }
    
    
    
    
    
    
}