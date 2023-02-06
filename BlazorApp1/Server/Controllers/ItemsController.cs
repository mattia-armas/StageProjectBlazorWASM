using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Shared;
using BlazorApp1.Server.Data;



namespace BlazorApp1.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public ItemsController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Item>> GetItems()
    {
        return await _context.Items.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Item>> GetSingleItem(string id)
    {
        var items = await _context.Items.FindAsync(id);
        if (items == null)
        {
            return NotFound();
        }
        return items;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutItems(string id, Item items)
    {
        if (id != items.Id)
        {
            return BadRequest();
        }
        _context.Entry(items).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ItemsExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
    public async Task<IActionResult> PostItems(Item items)
    {
        _context.Items.Add(items);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSingleItem), new { id = items.Id }, items);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItems(int id)
    {
        var items = await _context.Items.FindAsync(id);
        if (items == null)
        {
            return NotFound();
        }
        _context.Items.Remove(items);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ItemsExists(string id)
    {
        return _context.Items.Any(e => e.Id == id);
    }
}
