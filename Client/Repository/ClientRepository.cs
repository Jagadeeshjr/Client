using AutoMapper;
using ClientNamespace.Data;
using ClientNamespace.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace ClientNamespace.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientDbContext _context;
        private readonly IMapper _mapper;

        public ClientRepository(ClientDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClientModel>> GetAllClientsAsync()
        {
            var clients = await _context.Clients.ToListAsync();
            return _mapper.Map<List<ClientModel>>(clients);
        }

        public async Task<ClientModel> GetClientByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            return _mapper.Map<ClientModel>(client);
        }

        public async Task<int> AddClientAsync(ClientModel clientModel)
        {
            var client = new ClientModel()
            {
                ClientName = clientModel.ClientName,
                Description = clientModel.Description,
                LicenceKey = Guid.NewGuid(),
            };

            clientModel.LicenceStartDate = DateTime.Now;
            clientModel.LicenceEndDate = DateTime.Now.AddDays(365);
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client.LicenceId;
        }

        public async Task UpdateClientAsync(int id, ClientModel clientModel)
        {
            var client = await _context.Clients.FindAsync(id);
            var clients = new ClientModel()
            {
                ClientName = clientModel.ClientName,
                Description = clientModel.Description
            };

            clientModel.LicenceStartDate = DateTime.Now;
            clientModel.LicenceEndDate = DateTime.Now.AddDays(365);
            _context.Clients.Update(clients);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientPatchAsync(int id, JsonPatchDocument clientModel)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                clientModel.ApplyTo(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
