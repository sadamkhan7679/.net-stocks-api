using api.Data;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class CommentRepository:ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<List<Comment>> GetAllCommentsAsync()
    {
        return _dbContext.Comments.ToListAsync();
    }
    
    public Task<Comment?> GetCommentByIdAsync(int id)
    {
        return _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<Comment> CreateCommentAsync(Comment commentModel)
    {
        await _dbContext.Comments.AddAsync(commentModel);
        await _dbContext.SaveChangesAsync();
        return commentModel;
    }
    
    public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if(comment == null)
        {
            return null;
        }
        
        comment.Title = commentModel.Title;
        comment.Content = commentModel.Content;
        
        await _dbContext.SaveChangesAsync();
        return comment;
    }
    
    
    public async Task<Comment?> DeleteCommentAsync(int id)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if(comment == null)
        {
            return null;
        }
        
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }
    
}