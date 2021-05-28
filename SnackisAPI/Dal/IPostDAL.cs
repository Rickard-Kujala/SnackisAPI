using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public interface IPostDAL
    {
        IEnumerable<Models.Post> GetAllPosts();
        Task CreatePost(Models.Post model);
        public IEnumerable<string> GetAllcetegories();
        Task DeleteCategoryToDB(string name)
    }
}