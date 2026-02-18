using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly MongoService _mongoService;

    public ItemsController(MongoService mongoService) =>
        _mongoService = mongoService;

    [HttpGet]
    public async Task<List<Item>> Get() =>
        await _mongoService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Item>> Get(string id)
    {
        var item = await _mongoService.GetAsync(id);
        if (item is null) return NotFound();
        return item;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Item newItem)
    {
        await _mongoService.CreateAsync(newItem);
        return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
    }

    // --- THIS WAS MISSING ---
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Item updatedItem)
    {
        var item = await _mongoService.GetAsync(id);
        if (item is null) return NotFound();

        updatedItem.Id = item.Id;
        await _mongoService.UpdateAsync(id, updatedItem);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var item = await _mongoService.GetAsync(id);
        if (item is null) return NotFound();

        await _mongoService.RemoveAsync(id);

        return NoContent();
    }
}