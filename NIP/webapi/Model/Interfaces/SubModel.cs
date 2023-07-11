using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Nip.Models.Interfaces;

namespace Nip.Models.Interfaces
{
    public class SubModel : IIdentityModel, ISubModel
  {
    public int Id { get; set; }
    public int TaxPayerId { get; set; }
  }
}
