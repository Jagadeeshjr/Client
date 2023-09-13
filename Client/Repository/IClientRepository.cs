using ClientNamespace.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace ClientNamespace.Repository
{
    public interface IClientRepository
    {
        Task<List<ClientModel>> GetAllClientsAsync();

        Task<ClientModel> GetClientByIdAsync(int id);

        Task<int> AddClientAsync(ClientModel clientModel);

        Task UpdateClientAsync(int id, ClientModel clientModel);

        Task UpdateClientPatchAsync(int id, JsonPatchDocument clientModel);

        Task DeleteClientAsync(int id);
    }
}