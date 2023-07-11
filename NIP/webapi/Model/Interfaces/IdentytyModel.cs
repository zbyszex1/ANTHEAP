using System.ComponentModel.DataAnnotations;

namespace Nip.Models.Interfaces
{
  public class IdentityModel : IIdentityModel
  {
    [Required]
    public int Id { get; set; }
  }
}
