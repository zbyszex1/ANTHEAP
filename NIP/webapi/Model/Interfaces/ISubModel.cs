using System.ComponentModel.DataAnnotations;

namespace Nip.Models.Interfaces
{
  public interface ISubModel
  {
    public int TaxPayerId { get; set; }
  }
}
