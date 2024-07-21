﻿using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class CreateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage ="Title must be at least 5 characters long")]
    [MaxLength(256, ErrorMessage = "Title must be less than 256 characters long")]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(5, ErrorMessage ="Content must be at least 5 characters long")]
    [MaxLength(1000, ErrorMessage = "Content must be less than 1000 characters long")]
    public string Content { get; set; } = string.Empty;
    // public string CreatedBy { get; set; } = string.Empty;
    
    [Required]
    public int? StockId { get; set; }
}