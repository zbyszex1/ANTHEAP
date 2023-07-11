import React, { Component } from 'react';
import './App.css';
import Header from './Components/Header';
import Request from './Components/Request';
import Results from './Components/Results';
import Decode from './Work/Decode.js'

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.headers = { 'Content-type': 'application/json; charset=UTF-8' };
    this.decode = new Decode();
    this.populateNipData = this.populateNipData.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.state = {
      used: false,
      forecasts: [],
      rawData: '',
      decode: null,
      nipData: [
        { key: 'key', value: 'value' }
      ],
      nipNumber: '',
      loading: true,
      fetching: true
    };
  }

  handleChange(e) {
    this.setState({ nipNumber: e.target.value });
  }

  componentDidMount() {
  }


  render() {
    return (
      <div>
        <Header />
        <Request onSave={(nip) => { this.populateNipData(nip) }} />
        <Results rawData={this.state.rawData} decode={this.state.decode} />
      </div>
    );
  }

  async populateNipData(request) {
    const response = await fetch("https://wl-api.mf.gov.pl/api/search/nip/"
      + request.number + "?date=" + request.date, {
      method: "GET",
      headers: {}
    });
    let data = await response.json();
    //data = JSON.parse('{ "result" : { "subject" : { "authorizedClerks" : [ { "firstName" : "Jan", "lastName" : "Nowak", "nip" : "1111111111", "companyName" : "Nazwa firmy" }, { "firstName" : "Jan", "lastName" : "Nowak", "nip" : "1111111111", "companyName" : "Nazwa firmy" } ], "regon" : "regon", "restorationDate" : "2019-02-21", "workingAddress" : "ul/ Prosta 49 00-838 Warszawa", "hasVirtualAccounts" : true, "statusVat" : "Zwolniony", "krs" : "0000636771", "restorationBasis" : "Ustawa o podatku od towarów i us³ug art. 96", "accountNumbers" : ["90249000050247256316596736", "90249000050247256316596763"], "registrationDenialBasis" : "Ustawa o podatku od towarów i us³ug art. 96", "nip" : "1111111111", "removalDate" : "2019-02-21", "partners" : [], "name" : "ABC Jan Nowak", "registrationLegalDate" : "2018-02-21", "removalBasis" : "Ustawa o podatku od towarów i us³ug Art. 97", "pesel" : "22222222222", "representatives" : [{ "firstName": "Jan", "lastName": "Nowak", "nip": "1111111111", "companyName": "Nazwa firmy" }, { "firstName": "Jan", "lastName": "Nowak", "nip": "1111111111", "companyName": "Nazwa firmy" }], "residenceAddress" : "ul/ Chmielna 85/87 00-805 Warszawa", "registrationDenialDate" : "2019-02-21" }, "requestDateTime": "19-11-2019 14:58:49", "requestId" : "d2n10-84df1a1" } }');
    let decode = new Decode();
    decode.Process(data);
    this.setState({ rawData: data, decode: decode });

    const dataSets = [
      { data: decode.accountNumbersArray, uri: '/accountnumber' },
      { data: decode.authorizedClerksArray, uri: '/authorizedclerk' },
      { data: decode.partnersArray, uri: '/partner' },
      { data: decode.representativesArray, uri: '/representative' }
    ]

    setTimeout(() => {
      this.doWriteLoop(dataSets);
    },100)
  }

  async doWriteLoop(dataSets) {
    const id = await this.writeTable('/taxpayer', this.state.decode.taxPayer)
    for (const dataSet of dataSets) {
      if (dataSet != null && dataSet.data.length > 0) {
        for (const table of dataSet.data) {
          table.TaxPayerId = id;
          await this.writeTable(dataSet.uri, table);
        }
      }
    }
  }

  async writeTable(uri, table) {
    const response = await fetch(uri, {
      method: 'POST',
      headers: this.headers,
      body: JSON.stringify(table)
    })
    const data = await response.json();
    return data;
  }

}
