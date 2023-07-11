class Person {
  constructor() {
    this.TaxPayerId = null;
    this.FirstName = null;
    this.LastName = null;
    this.Nip = null;
    this.Pesel = null;
    this.CompanyName = null;
  }

  Apply(key, value) {
    switch (key) {
      case 'firstName':
        this.FirstName = value;
        break;
      case 'lastName':
        this.LastName = value;
        break;
      case 'companyName':
        this.CompanyName = value;
        break;
      case 'nip':
        this.Nip = value;
        break;
      case 'regon':
        this.Regon = value;
        break;
      case 'taxPayerId':
        this.TaxPayerId = value;
        break;
      case 'id':
        this.Id = value;
        break;
      default:
        break;
    }
  }

}

export default Person;

