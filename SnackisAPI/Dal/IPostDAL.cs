using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public interface IPostDAL
    {
        IEnumerable<Models.Post> GetAllPosts();
        Task CreatePost(Models.Post model);
        public IEnumerable<Post> GetAllcetegories();
        Task DeleteCategoryToDB(Guid id);
        Task UpdatePost(Guid id, Post updatedPost);
    }
}