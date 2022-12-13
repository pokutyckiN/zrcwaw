using System.Threading.Tasks;
using zrcwaw_l2.Models;

namespace zrcwaw_l2
{
    public interface IDynamoDBCRUD
    {
        Task<bool> CreateClient(Client client);
        Task<Models.Client[]> GetClients();
        Task<bool> Update(string Id, Models.Client client);
        Task<Models.Client> GetOneClient(string Id);
        Task<bool> Delete(string Id);
    }
}