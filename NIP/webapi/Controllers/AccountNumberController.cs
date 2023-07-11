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
  public class AccountNumberController : ControllerBase
  {
    private readonly NipContext _nipContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public AccountNumberController(NipContext nipContext, ILogger<WeatherForecastController> logger)
    {
      _nipContext = nipContext;
      _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<List<AccountNumber>> Get()
    {
      try
      {
        var AccountNumbers = _nipContext.AccountNumbers.ToArray();
        if (AccountNumbers == null)
        {
          return BadRequest("Fail access to 'AccountNumber' table");
        }
        return Ok(AccountNumbers);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<AccountNumber> Get(int id)
    {
      var AccountNumber = _nipContext.AccountNumbers
          .FirstOrDefault(m => m.Id == id);

      if (AccountNumber == null)
      {
        return NotFound();
      }
      return Ok(AccountNumber);
    }

    [HttpPost]
    public ActionResult Post([FromBody] AccountNumber model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _nipContext.AccountNumbers.Add(model);
      _nipContext.SaveChanges();

      int id = model.Id;
      return Created("api/AccountNumber/" + id, id);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] AccountNumber model)
    {
      var AccountNumber = _nipContext.AccountNumbers
          .FirstOrDefault(m => m.Id == id);

      if (AccountNumber == null)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      AccountNumber.Number = model.Number;

      _nipContext.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      var AccountNumber = _nipContext.AccountNumbers
          .FirstOrDefault(m => m.Id == id);

      if (AccountNumber == null)
      {
        return NotFound();
      }

      _nipContext.Remove(AccountNumber);
      _nipContext.SaveChanges();

      return NoContent();
    }
  }
}
