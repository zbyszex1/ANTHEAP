using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Nip.Models.Interfaces;

namespace Nip.Models.Interfaces
{
    public class CreatedModel : ICreatedModel, IIdentityModel
  {
    public int Id { get; set; }
    public DateTime? Created { get; set; }
  }
}
