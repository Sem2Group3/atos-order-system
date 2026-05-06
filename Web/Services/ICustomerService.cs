using Web.Models;

namespace Web.Services;

public interface ICustomerService
{
    Task<CustomerViewModel?> GetCustomerByIdAsync(int id);
    Task<List<CustomerViewModel>> GetAllCustomersAsync();
    Task<List<CustomerViewModel>> SearchCustomersAsync(string? searchTerm);
    Task<int> CreateCustomerAsync(CustomerViewModel customer);
    Task<bool> UpdateCustomerAsync(int id, CustomerViewModel customer);
    Task<bool> DeleteCustomerAsync(int id);
}

