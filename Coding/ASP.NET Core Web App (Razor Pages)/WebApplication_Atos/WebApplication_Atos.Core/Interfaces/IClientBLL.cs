using System;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Core.Interfaces;

public interface IClientBLL
{
    List<Client> GetClients();
    Task<List<Client>> GetClientsAsync();
    Client? GetClientFromID(int clientID);
    void AddClient(Client newClient);
    void UpdateClient(Client client);
}

