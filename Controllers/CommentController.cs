using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;
/// <summary>
/// Defines the <see cref="CommentController" /> for managing comment-related requests.
/// </summary>
[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{

    // Repository for comment-related data operations
    private readonly ICommentRepository _commentRepo;
    
    // Repository for stock-related data operations
    private readonly IStockRepository _stockRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentController"/> class.
    /// </summary>
    /// <param name="commentRepo">The comment repository.</param>
    /// <param name="stockRepository">The stock repository.</param>
    public CommentController( ICommentRepository commentRepo, IStockRepository stockRepository)
    {
        _commentRepo = commentRepo;
        _stockRepository = stockRepository;
    }

    /// <summary>
    /// Retrieves all comments.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation with an <see cref="IActionResult"/> result containing all comments.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepo.GetAllCommentsAsync();

        // Convert comments to DTOs for client response
        var commentDtos = comments.Select(comment => comment.ToCommentDto());

        return Ok(commentDtos);
    }
    
    /// <summary>
    /// Retrieves a comment by Id
    /// </summary>
    ///     <param name="id">The comment Id</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation with an <see cref="IActionResult"/> result containing the comment.</returns>
    ///
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById([FromRoute] int id)
    {
        var comment = await _commentRepo.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        var commentDto = comment.ToCommentDto();

        return Ok(commentDto);
    }
    
    /// <summary>
    /// Create a new comment
    /// </summary>
    ///     <param name="createCommentRequestDto">The comment to create</param>
    ///     <param name="stockId">The stock Id</param>
    ///     <returns>A <see cref="Task"/> representing the asynchronous operation with an <see cref="IActionResult"/> result containing the created comment.</returns>
    
    [HttpPost("{stockId}")]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDto createCommentRequestDto, [FromRoute] int stockId)
    {
        var stock = await _stockRepository.StockExistsAsync(stockId);

        if (!stock)
        {
            return BadRequest("Stock does not exist");
        }

        var comment = createCommentRequestDto.ToCommentModel(stockId);
        comment.StockId = stockId;
        

        await _commentRepo.CreateCommentAsync(comment);

        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDto());
    }
    
    /// <summary>
    /// Update a comment
    /// </summary>
    ///     <param name="id">The comment Id</param>
    ///     <param name="updateCommentRequestDto">The comment to update</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation with an <see cref="IActionResult"/> result containing the updated comment.</returns>
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateCommentRequestDto)
    {
        var comment = await _commentRepo.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        var updatedComment = updateCommentRequestDto.ToCommentModel();
        var result = await _commentRepo.UpdateCommentAsync(id, updatedComment);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result.ToCommentDto());
    }
    
    /// <summary>
    ///     Delete a comment
    ///     </summary>
    ///     <param name="id">The comment Id</param>
    ///     <returns>A <see cref="Task"/> representing the asynchronous operation with an <see cref="IActionResult"/> result containing the deleted comment.</returns>
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int id)
    {
        var comment = await _commentRepo.GetCommentByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        var result = await _commentRepo.DeleteCommentAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result.ToCommentDto());
    }
}