using SnackisAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public interface IChatDAL
    {
        IEnumerable<Post> GetAllChats();
        Task CreateChat(Chat model);
    }
}