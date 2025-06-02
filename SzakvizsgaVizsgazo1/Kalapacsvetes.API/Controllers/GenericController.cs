using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KozosKonyvtar;
using KozosKonyvtar.MODEL;

namespace Kalapacsvetes.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenericController<T> : ControllerBase where T : class
{
    private readonly ApplicationDbContext _context;

    //Ez a változó kapja meg, melyik táblával dolgozunk
    private readonly DbSet<T> _dbSet; 

    public GenericController(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> GetAll()
    {
        return Ok(await _dbSet.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> GetById(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<T>> Create(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return Ok(entity); // Egyszerűsített válasz
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
            return NotFound();

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
