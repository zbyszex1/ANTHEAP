using Nip.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nip.Models
{
  public class AccountNumber : SubModel
  {
    [MaxLength(26)]
    public string? Number { get; set; }
  }
}
