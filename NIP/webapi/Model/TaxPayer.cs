using Nip.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Collections.Specialized.BitVector32;

namespace Nip.Models
{
  public class TaxPayer : CreatedModel
  {
    [MaxLength(64)]
    public string? Name { get; set; }
    [MaxLength(10)]
    public string? Nip { get; set; }
    [MaxLength(16)]
    public string? SatusVat { get; set; }
    [MaxLength(9)]
    public string? Regon { get; set; }
    [MaxLength(11)]
    public string? Pesel { get; set; }
    [MaxLength(10)]
    public string? Krs { get; set; }
    [MaxLength(64)]
    public string? ResidenceAddress { get; set; }
    [MaxLength(64)]
    public string? WorkingAddress { get; set; }
    public DateTime? RegistrationLegalDate { get; set; }
    [MaxLength(64)]
    public string? RegistrationDenialBasis { get; set; }
    public DateTime? RegistrationDenialDate { get; set; }
    [MaxLength(64)]
    public string? RestorationBasis { get; set; }
    public DateTime? RestorationDate { get; set; }
    [MaxLength(64)]
    public string? RemovalBasis { get; set; }
    public DateTime? RemovalDate { get; set; }
    public bool HasVirtualAccounts { get; set; }
    [MaxLength(16)]
    public string? RequestId { get; set; }
    public DateTime? RequesteDateTime { get; set; }
    [MaxLength(64)]
    public string? QueryResult { get; set; }
  }
}
