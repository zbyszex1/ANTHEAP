import React from "react";
import PropTypes from 'prop-types';
import Person from './../Model/Person.js';

import './Results.css';

const Results = props => {
  let id = 0;

  function renderTaxRow(row) {
    if (row != null && row.value != null) {
      return (
        <tr key={row.key}>
          <td>{row.key}</td>
          <td>{row.value.toString()}</td>
        </tr>
      )
    } else {
      return ('');
    }
  }

  function renderSubRow(row) {
    let textRow = '';
    if (typeof row === 'string') {
      textRow = row;
    } else {
      Object.keys(row).forEach(function (key) {
        if (row[key] !=null)
          textRow += row[key] + ' ';
      })
    }
    return (
      <tr key={id++}>
        <td>{textRow}</td>
        <td></td>
      </tr>
    )
  }

  function getHeader(tableName) {
    if (tableName === 'accountNumbers') {
      //subArray = accountNumbersArray;
      return (<h4>Rachunki bankowe podmiotu.</h4>)
    }
    if (tableName === 'authorizedClerks') {
      //subArray = authorizedClerksArray;
      return (<h4>Imiona i nazwiska prokurentów oraz ich numery NIP i/lub PESEL</h4>)
    }
    if (tableName === 'partners') {
      //subArray = partnersArray;
      return (<h4>Imiona i nazwiska lub firmę (nazwa) wspólnika oraz jego numeryNIP i / lub PESELL</h4>)
    }
    if (tableName === 'representatives') {
      //subArray = representativesArray;
      return (<h4>Imiona i nazwiska osób wchodzących w skład organu uprawnionego do reprezentowania podmiotu oraz ich numery NIP i/lub PESEL</h4>)
    }
  }
  function getArray(array) {
    return (
    <table className='table table-striped' aria-labelledby="tabelLabel">
      <tbody>
        {array.map(row =>
          renderTaxRow(row)
        )}
      </tbody>
    </table>
    )
  }

  function getSubArray(array) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <tbody>
          {array.map(row =>
            renderSubRow(row)
          )}
        </tbody>
      </table>
    )
  }
  if (props.decode != null && props.decode.taxPayer.Nip != null) {
    const tablePayer = [
      { key: 'Firma (nazwa) lub imię i nazwisko', value: props.decode.taxPayer.Name },
      { key: 'NIP', value: props.decode.taxPayer.Nip },
      { key: 'Status podatnika VAT', value: props.decode.taxPayer.StatusVat },
      { key: 'Numer identyfikacyjny REGON', value: props.decode.taxPayer.Regon },
      { key: 'numer PESEL jeżeli został nadany', value: props.decode.taxPayer.Pesel },
      { key: 'numer KRS jeżeli został nadany', value: props.decode.taxPayer.Krs },
      { key: 'Adres siedziby działalności gospodarczej (Adres siedziby OSOBY FIZYCZNEJ prowadzącej działalność gospodarczą)', value: props.decode.taxPayer.ResidenceAddress },
      { key: 'Adres rejestracyjny (Adres zamieszkania OSOBY FIZYCZNEJ lub adres siedziby ORGANIZACJI.).', value: props.decode.taxPayer.WorkingAddress },
      { key: 'Data rejestracji jako podatnika VAT', value: props.decode.taxPayer.RegistrationLegalDate },
      { key: 'Data odmowy rejestracji jako podatnika VAT', value: props.decode.taxPayer.RegistrationDenialDate },
      { key: 'Podstawa prawna odmowy rejestracji', value: props.decode.taxPayer.RegistrationDenialBasis },
      { key: 'Data przywrócenia jako podatnika VAT', value: props.decode.taxPayer.RestorationDate },
      { key: 'Podstawa prawna przywrócenia jako podatnika VAT', value: props.decode.taxPayer.RestorationBasis },
      { key: 'Data wykreślenia odmowy rejestracji jako podatnika VAT', value: props.decode.taxPayer.RemovalDate },
      { key: 'Podstawa prawna wykreślenia odmowy rejestracji jako podatnika VAT', value: props.decode.taxPayer.RemovalBasis },
      { key: 'Podmiot posiada maski kont wirtualnych', value: props.decode.taxPayer.HasVirtualAccounts }
    ];
    const tablePerson = [
      { key: 'Imię', value: Person.FirstName },
      { key: 'Nazwisko', value: Person.LastName },
      { key: 'numer NIP', value: Person.Nip },
      { key: 'numer PESEL', value: Person.Pesel },
      { key: 'Adres firmy', value: Person.CompanyName },
    ]
    id = 0;
    return (
      <div className="taxer">
        <h2>Podstawowe dane podatnika udostępnione przez administrację podatkową.</h2>
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <tbody>
          {tablePayer.map(row =>
            renderTaxRow(row)
          )}
        </tbody>
      </table>
      {getHeader('accountNumbers')}
      {getSubArray(props.decode.accountNumbersArray)}
      {getHeader('authorizedClerks')}
      {getSubArray(props.decode.authorizedClerksArray)}
      {getHeader('partners')}
      {getSubArray(props.decode.partnersArray)}
      {getHeader('representatives')}
      {getSubArray(props.decode.representativesArray)}
      </div>
    );
  }
  if (props.rawData.code != null) {
    return (
      <div className="error">
        <p>{props.rawData.code}</p>
        <p>{props.rawData.message}</p>
      </div>);
  }
}

Results.propTypes = {
  key: PropTypes.string,
  value: PropTypes.any
}
export default Results;

