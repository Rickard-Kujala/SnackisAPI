using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public interface IPostDAL
    {
        IEnumerable<Models.Post> GetCollectionFromDB();
        Task CreatePostToDB(Models.Post model);
    }
}