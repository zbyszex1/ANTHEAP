import React from "react";
import './Header.css';
import logo from './../Images/time-event.svg';

const Header = props => (
  <header className="gc header">
    <div className="c1">
      <div>
        <img src={logo} className="time-event" alt="time-event" />
      </div>
    </div>
    <div className="c2">
      <span className="header-text">Zadania, Ʃ {props.sigma}</span>
    </div>
  </header>
);

export default Header;

