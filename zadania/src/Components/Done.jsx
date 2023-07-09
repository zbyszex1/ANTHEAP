import React from "react";
import PropTypes from 'prop-types';
import './Done.css';

const Done = props => {
    return (
      <div className="gc list">
        <div className="c3">{props.zadanie}</div>
        <div className="c4">{props.czas}</div>
      </div>
    )
}

Done.propTypes = {
  zadanie: PropTypes.string,
  czas: PropTypes.string
}
export default Done;

