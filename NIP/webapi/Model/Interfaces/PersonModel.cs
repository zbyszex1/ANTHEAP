using Nip.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nip.Models
{
  public class PersonModel : IIdentityModel, ISubModel, IPersonModel
  {
    public int Id { get; set; }
    public int TaxPayerId { get; set; }
    [MaxLength(64)]
    public string? FirstName { get; set; }
    [MaxLength(64)]
    public string? LastName { get; set; }
    [MaxLength(10)]
    public string? Nip { get; set; }
    [MaxLength(11)]
    public string? Pesel { get; set; }
    [MaxLength(64)]
    public string? CompanyName { get; set; }
  }
}
