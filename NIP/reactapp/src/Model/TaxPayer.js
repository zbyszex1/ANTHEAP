import './AccountNumber';
import './Person.js';
import './AuthorizedClerk';
import './Partner';
import './Representative';
class TaxPayer {
  constructor() {
    this.Name = null;
    this.Nip = null;
    this.SatusVat = null;
    this.Regon = null;
    this.Pesel = null;
    this.Krs = null;
    this.ResidenceAddress = null;
    this.WorkingAddress = null;
    this.Created = null;
    this.RegistrationLegalDate = null;
    this.RegistrationDenialDate = null;
    this.RegistrationDenialBasis = null;
    this.RestorationBasis = null;
    this.RestorationDate = null;
    this.RemovalBasis = null;
    this.RemovalDate = null;
    this.HasVirtualAccounts = false;
    this.AccountNumbers = [];
    this.AuthorizedClerks = [];
    this.Partners = [];
    this.Representatives = [];
  }

  FromObject(data) {
    let taxPayer = new TaxPayer();
    return taxPayer;
  }

  Apply(key, value) {
    switch (key) {
      case 'name':
        this.Name = value;
        break;
      case 'nip':
        this.Nip = value;
        break;
      case 'statusVat':
        this.StatusVat = value;
        break;
      case 'regon':
        this.Regon = value;
        break;
      case 'pesel':
        this.Pesel = value;
        break;
      case 'krs':
        this.Krs = value;
        break;
      case 'residenceAddress':
        this.ResidenceAddress = value;
        break;
      case 'workingAddress':
        this.WorkingAddress = value;
        break;
      case 'created':
        this.Crreated = value;
        break;
      case 'registrationLegalDate':
        this.RegistrationLegalDate = value;
        break;
      case 'registrationDenialDate':
        this.RegistrationDenialDate = value;
        break;
      case 'registrationDenialBasis':
        this.RegistrationDenialBasis = value;
        break;
      case 'restorationBasis':
        this.RestorationBasis = value;
        break;
      case 'restorationDate':
        this.RestorationDate = value;
        break;
      case 'removalBasis':
        this.RemovalBasis = value;
        break;
      case 'removalDate':
        this.RemovalDate = value;
        break;
      case 'hasVirtualAccounts':
        this.HasVirtualAccounts = value;
        break;
      case 'id':
        this.Id = value;
        break;
      default:
        break;
    }
  }

}

export default TaxPayer;
