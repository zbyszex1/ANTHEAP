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
  public class PartnerController : ControllerBase
  {
    private readonly NipContext _nipContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public PartnerController(NipContext nipContext, ILogger<WeatherForecastController> logger)
    {
      _nipContext = nipContext;
      _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<Partner>> Get()
    {
      try
      {
        var Partners = _nipContext.Partners.ToArray();
        if (Partners == null)
        {
          return BadRequest("Fail access to 'Partner' table");
        }
        return Ok(Partners);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Partner> Get(int id)
    {
      var Partner = _nipContext.Partners
          .FirstOrDefault(m => m.Id == id);

      if (Partner == null)
      {
        return NotFound();
      }
      return Ok(Partner);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Partner model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _nipContext.Partners.Add(model);
      _nipContext.SaveChanges();

      int id = model.Id;
      return Created("api/Partner/" + id, id);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Partner model)
    {
      var Partner = _nipContext.Partners
          .FirstOrDefault(m => m.Id == id);

      if (Partner == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Partner.FirstName = model.FirstName;
      Partner.LastName = model.LastName;
      Partner.CompanyName = model.CompanyName;
      Partner.Pesel = model.Pesel;
      Partner.Nip = model.Nip;

      _nipContext.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var Partner = _nipContext.Partners
          .FirstOrDefault(m => m.Id == id);

      if (Partner == null)
      {
        return NotFound();
      }

      _nipContext.Remove(Partner);
      _nipContext.SaveChanges();

      return NoContent();
    }
  }
}
