import TaxPayer from './../Model/TaxPayer.js';
import AccountNumber from './../Model/AccountNumber.js';
import Person from './../Model/Person.js';
class Decode {
  constructor() {
    this.taxPayer = new TaxPayer();
    this.accountNumbersArray = [];
    this.authorizedClerksArray = [];
    this.partnersArray = [];
    this.representativesArray = [];
  }

  Process(rawData) {
    let instance = this;
    if (rawData != null &&
      rawData.result != null &&
      rawData.result.subject != null) {
      Object.keys(rawData.result.subject).forEach(function (key) {
        const val = rawData.result.subject[key];
        if (val != null) {
          if (Array.isArray(val)) {
            if (val.length > 0) {
              instance.ProcessArray(key, val);
            }
          } else {
            instance.taxPayer.Apply(key, val)
          }
        }
      })
    }
  }

  ProcessArray(name, rows) {
    if (rows == null || !Array.isArray(rows) || rows.count === 0)
      return '';
    let arr = null;
    switch (name) {
      case 'accountNumbers':
        arr = this.accountNumbersArray;
        break;
      case 'authorizedClerks':
        arr = this.authorizedClerksArray;
        break;
      case 'partners':
        arr = this.partnersArray;
        break;
      case 'representatives':
        arr = this.representativesArray;
        break;
      default:
        break;
    }
    rows.forEach(row => {
      if (Array.isArray(row)) {
        let person = new Person();
        Object.keys(row).forEach(function (key) {
          person[key] = row[key];
        });
        arr.push(person);
      } else {
        if (typeof row === 'string') {
          let accountNumber = new AccountNumber();
          const key = 'Number';
          accountNumber[key] = row;
          arr.push(accountNumber)
        } else {
          arr.push(row);
        }
      }
    })
  }

}

export default Decode;
