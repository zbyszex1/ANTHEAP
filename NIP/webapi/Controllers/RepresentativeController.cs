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
  public class RepresentativeController : ControllerBase
  {
    private readonly NipContext _nipContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public RepresentativeController(NipContext nipContext, ILogger<WeatherForecastController> logger)
    {
      _nipContext = nipContext;
      _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<Representative>> Get()
    {
      try
      {
        var Representatives = _nipContext.Representatives.ToArray();
        if (Representatives == null)
        {
          return BadRequest("Fail access to 'Representative' table");
        }
        return Ok(Representatives);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Representative> Get(int id)
    {
      var Representative = _nipContext.Representatives
          .FirstOrDefault(m => m.Id == id);

      if (Representative == null)
      {
        return NotFound();
      }
      return Ok(Representative);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Representative model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _nipContext.Representatives.Add(model);
      _nipContext.SaveChanges();

      int id = model.Id;
      return Created("api/Representative/" + id, id);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Representative model)
    {
      var Representative = _nipContext.Representatives
          .FirstOrDefault(m => m.Id == id);

      if (Representative == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      Representative.FirstName = model.FirstName;
      Representative.LastName = model.LastName;
      Representative.CompanyName = model.CompanyName;
      Representative.Pesel = model.Pesel;
      Representative.Nip = model.Nip;

      _nipContext.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var Representative = _nipContext.Representatives
          .FirstOrDefault(m => m.Id == id);

      if (Representative == null)
      {
        return NotFound();
      }

      _nipContext.Remove(Representative);
      _nipContext.SaveChanges();

      return NoContent();
    }
  }
}
