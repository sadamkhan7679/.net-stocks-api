using api.Models;

namespace api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllCommentsAsync();
    
    Task<Comment?> GetCommentByIdAsync(int id);
    
}