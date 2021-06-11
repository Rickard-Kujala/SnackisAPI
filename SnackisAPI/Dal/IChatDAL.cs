using SnackisAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnackisAPI.Dal
{
    public interface IChatDAL
    {
        IEnumerable<Chat> GetAllChats();
        Task CreateChat(Chat model);
        Task DeletechatById(Guid id);
    }
}