using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Nip.Models.Interfaces;

namespace Nip.Models.Interfaces
{
    public interface IPersonModel
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Nip { get; set; }
    public string? Pesel { get; set; }
    public string? CompanyName { get; set; }
  }
}
