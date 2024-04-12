using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(string id);

        Task<Comment> CreateAsync(Comment commentModel);

        Task<Comment?> RemoveAsync(string id);

        Task<Comment?> UpdateAsync(string id, UpdateCommentRequestDto commentDto);
    }
}
