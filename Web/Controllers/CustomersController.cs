using Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [Authorize(Policy = Permissions.Customer.View)]
    public async Task<IActionResult> Index(string? searchTerm)
    {
        var customers = string.IsNullOrWhiteSpace(searchTerm) 
            ? await _customerService.GetAllCustomersAsync()
            : await _customerService.SearchCustomersAsync(searchTerm);
        
        ViewData["SearchTerm"] = searchTerm;
        return View(customers);
    }

    [Authorize(Policy = Permissions.Customer.Create)]
    public IActionResult Create()
    {
        return View(new CustomerViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Permissions.Customer.Create)]
    public async Task<IActionResult> Create(CustomerViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _customerService.CreateCustomerAsync(model);
        TempData["SuccessMessage"] = "Klant aangemaakt.";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Policy = Permissions.Customer.View)]
    public async Task<IActionResult> Details(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    [Authorize(Policy = Permissions.Customer.Update)]
    public async Task<IActionResult> Edit(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Permissions.Customer.Update)]
    public async Task<IActionResult> Edit(int id, CustomerViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var success = await _customerService.UpdateCustomerAsync(id, model);
        if (!success)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Klant bijgewerkt.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Permissions.Customer.Delete)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _customerService.DeleteCustomerAsync(id);
        if (!success)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Klant verwijderd.";
        return RedirectToAction(nameof(Index));
    }
}




