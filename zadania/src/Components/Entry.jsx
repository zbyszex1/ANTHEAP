import {React, Component } from "react";
import './Entry.css';
import { FaPlay, FaTrashAlt, FaStop, FaPause, FaHourglassHalf } from 'react-icons/fa';

class Entry extends Component {
    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
        this.handleStart = this.handleStart.bind(this);
        this.handleStop = this.handleStop.bind(this);
        this.handleTrash = this.handleTrash.bind(this);
        this.handlePause = this.handlePause.bind(this);
        this.state = {
            taskStarted: false,
            taskPaused: false,
            startTime: 0,
            stopTime: 0,
            spanTime: 0,
            taskName: ""
        };
    }

    handleChange(e) {
        this.setState({[e.target.name]: e.target.value});
    }

    handleStart() {
        this.setState({startTime: new Date()});
        this.setState({stopTime: new Date()});
        this.setState({taskStarted: true});
        this.setState({taskPaused: false});
    }

    handleStop() {
        this.setState({stopTime: new Date()});
        this.setState({taskStarted: false});
        this.setState({taskPaused: false});
        setTimeout(() => {
            let span = this.state.spanTime + (this.state.stopTime-this.state.startTime);
            this.setState({spanTime: span});
            this.setState({spanTime: 0});
            // this.props.onSave({name: this.state.taskName, span: this.printSpan(span)});
            this.props.onSave({name: this.state.taskName, span: span});
            this.setState({taskName: ""});
        },100);
    }

    handleTrash() {
        this.setState({taskStarted: false});
        this.setState({taskPaused: false});
        this.setState({spanTime: 0});
        this.setState({taskName: ""});
    }

    handlePause() {
        this.setState({stopTime: new Date()});
        this.setState({taskStarted: false});
        this.setState({taskPaused: true});
        setTimeout(() => {
            let span = parseInt(this.state.stopTime-this.state.startTime);
            span += this.state.spanTime;
            this.setState({spanTime: span});
        },100);
    }
    
    // printSpan(span) {
    //     let result = "";
    //     const second = 1000;
    //     const minute = second * 60;
    //     const hour = minute * 60;
    //     const day = hour * 24;
    //     const month = day * 31;
    //     if (span > month) {
    //         return '* ponad miesiąc!'
    //     }
    //     let days = Math.floor(span / day);
    //     span -= days * day;
    //     let hours = Math.floor(span / hour);
    //     span -= hours * hour;
    //     let minutes = Math.floor(span / minute);
    //     span -= minutes * minute;
    //     let seconds = Math.floor(span / second);
    //     span -= seconds * second;
    //     if (days > 0) {
    //         result += days.toString().padStart(2, '0') + ' ';
    //     }
    //     result += hours.toString().padStart(2, '0') + ':';
    //     result += minutes.toString().padStart(2, '0') + ':';
    //     result += seconds.toString().padStart(2, '0') + ',';
    //     result += span.toString().padStart(3, '0');
    //     return result;
    // }

    render() {
        const taskStarted = this.state.taskStarted;
        const taskPaused = this.state.taskPaused;
        const disabled = this.state.taskName.length < 3;
        const renderPlayButton = () => {
            if (!taskStarted) {
              return  (
                <div>
                  <FaHourglassHalf className={taskPaused? "icon": "hidden"}/>
                  <button className="button" onClick={this.handleStart} disabled={disabled}><FaPlay className={disabled? "disabled" : "icon"}/></button>
                  <button className="button" onClick={this.handleTrash}><FaTrashAlt className="icon"/></button>
                </div>)
            } else {
              return (
                <div>
                  <button className="button" onClick={this.handleStop}><FaStop className="icon"/></button>
                  <button className="button" onClick={this.handlePause}><FaPause className="icon"/></button>
                </div>)
            }
        }
      
        return (
          <div className="gc entry">
            <div className="c3">
                <label htmlFor="taskName">nazwa:</label>
                <input 
                type="text" 
                className="input"
                name="taskName"
                readOnly={this.state.taskStarted}
                placeholder='wpisz nazwę zadania'
                value={this.state.taskName}
                onChange={this.handleChange}/>
            </div>
            <div className="c4">
                {renderPlayButton()}
            </div>
          </div>
        )
    }
}

export default Entry;
