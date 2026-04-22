using System;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces;

public interface IClientDBManager
{
    List<Client> GetClients();
    Task<List<Client>> GetClientsAsync();
    Client? GetClientFromID(int clientID);
    void AddClient(Client newClient);
    public void UpdateClient(Client client);

}
