using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Nip.Models.Interfaces
{
  public interface ICreatedModel
  {
    public DateTime? Created { get; set; }
  }
}
