using api.Data;
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
    // Database context for entity framework operations
    readonly ApplicationDbContext _dbContext;

    // Repository for comment-related data operations
    private readonly ICommentRepository _commentRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentController"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="commentRepo">The comment repository.</param>
    public CommentController(ApplicationDbContext dbContext, ICommentRepository commentRepo)
    {
        _dbContext = dbContext;
        _commentRepo = commentRepo;
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
}