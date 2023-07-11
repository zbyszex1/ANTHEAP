using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nip.Controllers;
using Nip.Models.Interfaces;
using System.Data;
using Nip.Models;

namespace Nip.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthorizedClerkController : ControllerBase
  {
    private readonly NipContext _nipContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public AuthorizedClerkController(NipContext nipContext, ILogger<WeatherForecastController> logger)
    {
      _nipContext = nipContext;
      _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<AuthorizedClerk>> Get()
    {
      try
      {
        var AuthorizedClerks = _nipContext.AuthorizedClerks.ToArray();
        if (AuthorizedClerks == null)
        {
          return BadRequest("Fail access to 'AuthorizedClerk' table");
        }
        return Ok(AuthorizedClerks);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<AuthorizedClerk> Get(int id)
    {
      var AuthorizedClerk = _nipContext.AuthorizedClerks
          .FirstOrDefault(m => m.Id == id);

      if (AuthorizedClerk == null)
      {
        return NotFound();
      }
      return Ok(AuthorizedClerk);
    }

    [HttpPost]
    public ActionResult Post([FromBody] AuthorizedClerk model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _nipContext.AuthorizedClerks.Add(model);
      _nipContext.SaveChanges();

      int id = model.Id;
      return Created("api/AuthorizedClerk/" + id, id);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] AuthorizedClerk model)
    {
      var AuthorizedClerk = _nipContext.AuthorizedClerks
          .FirstOrDefault(m => m.Id == id);

      if (AuthorizedClerk == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      AuthorizedClerk.FirstName = model.FirstName;
      AuthorizedClerk.LastName = model.LastName;
      AuthorizedClerk.CompanyName = model.CompanyName;
      AuthorizedClerk.Pesel = model.Pesel;
      AuthorizedClerk.Nip = model.Nip;

      _nipContext.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var AuthorizedClerk = _nipContext.AuthorizedClerks
          .FirstOrDefault(m => m.Id == id);

      if (AuthorizedClerk == null)
      {
        return NotFound();
      }

      _nipContext.Remove(AuthorizedClerk);
      _nipContext.SaveChanges();

      return NoContent();
    }
  }
}
