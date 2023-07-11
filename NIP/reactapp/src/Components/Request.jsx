import {React, Component } from "react";
import './Request.css';
import 'react-datepicker/dist/react-datepicker.css';
import search from './../Images/search.svg';
import DatePicker from 'react-datepicker';
import { registerLocale, setDefaultLocale } from "react-datepicker";
import pl from 'date-fns/locale/pl';
registerLocale('pl', pl)

class Entry extends Component {
    constructor(props) {
        super(props);
    this.handleChange = this.handleChange.bind(this);
    this.handleDateChange = this.handleDateChange.bind(this);
    this.handleDateSelect = this.handleDateSelect.bind(this);
    this.handleStart = this.handleStart.bind(this);
    setDefaultLocale('pl');
      this.state = {
      nipNumber: "",
      requestDate: new Date()
    };
  }

  handleChange(e) {
    const re = /^[0-9\-\b]+$/;
    if (e.target.value === '' || re.test(e.target.value)) {
      this.setState({ [e.target.name]: e.target.value })
    }
  }

  handleDateChange(e) {
    this.setState({ requestDate: e });
    console.log(e);
  }

  handleDateSelect(e) {
    this.setState({ requestDate: e });
    console.log(e);
  }

  handleStart() {
    const shortNip = this.state.nipNumber.replace(/-/g, '');

    const dateString = this.state.requestDate.toISOString().substring(0, 10);
    this.props.onSave({ number: shortNip, date: dateString });
  }

    
    render() {
      const disabled = this.state.nipNumber.length < 10;
        return (
          <div className="gc entry">
            <div className="c3entry">
              <div>
                <input 
                  type="text" 
                  className="input"
                  name="nipNumber"
                  placeholder='podaj NIP podatnika'
                  value={this.state.nipNumber}
                  onChange={this.handleChange}
                />
                <span className="separator">&nbsp;&nbsp;</span>
                <DatePicker
                  className="picker"
                  locale='pl'
                  dateFormat="YYY-MM-dd"
                  selected={this.state.requestDate}
                  onSelect={this.handleDateSelect}
                  onChange={this.handleDateChange}
                />
              </div>
            </div>
            <div className="c4entry">
              <button className="button" onClick={this.handleStart} disabled={disabled}>
                <img src={search} className={disabled ? "disabled" : "search"} alt="search" />
              </button>
            </div>
          </div>
        )
    }
}

export default Entry;
