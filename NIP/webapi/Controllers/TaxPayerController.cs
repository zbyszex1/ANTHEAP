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
  public class TaxPayerController : ControllerBase
  {
    private readonly NipContext _nipContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public TaxPayerController(NipContext nipContext, ILogger<WeatherForecastController> logger)
    {
      _nipContext = nipContext;
      _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<TaxPayer>> Get()
    {
      try
      {
        var TaxPayers = _nipContext.TaxPayers.ToArray();
        if (TaxPayers == null)
        {
          return BadRequest("Fail access to 'TaxPayer' table");
        }
        return Ok(TaxPayers);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<TaxPayer> Get(int id)
    {
      var TaxPayer = _nipContext.TaxPayers
          .FirstOrDefault(m => m.Id == id);

      if (TaxPayer == null)
      {
        return NotFound();
      }
      return Ok(TaxPayer);
    }

    [HttpPost]
    public ActionResult Post([FromBody] TaxPayer model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _nipContext.TaxPayers.Add(model);
      _nipContext.SaveChanges();

      int id = model.Id;
      return Created("api/TaxPayer/" + id, id);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] TaxPayer model)
    {
      var TaxPayer = _nipContext.TaxPayers
          .FirstOrDefault(m => m.Id == id);

      if (TaxPayer == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      TaxPayer.Name = model.Name;
      TaxPayer.Krs = model.Krs;
      TaxPayer.Nip = model.Nip;

      _nipContext.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var TaxPayer = _nipContext.TaxPayers
          .FirstOrDefault(m => m.Id == id);

      if (TaxPayer == null)
      {
        return NotFound();
      }

      _nipContext.Remove(TaxPayer);
      _nipContext.SaveChanges();

      return NoContent();
    }
  }
}
