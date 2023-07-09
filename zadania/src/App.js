import { React, Component } from 'react';
import Header from './Components/Header';
import Entry from './Components/Entry';
import Done from './Components/Done';

class App extends Component {
    constructor() {
      super();
      this.state = {
        sigma: 0,
        tasks: [
          // {id:0, zadanie:"zadanie pierwsze", czas:925123 },
          // {id:1, zadanie:"zadanie drugie", czas:2713456 },
          // {id:2, zadanie:"baaardzo długie i niezwykle czasochłonne skomplikowane zadanie trzecie", czas:4230789 },
          // {id:3, zadanie:"zadanie czwarte", czas:993901 }
        ],
        taskId: 0
      }
      this.handleSaveTask = this.handleSaveTask.bind(this);
      let sigmaTemp = 0;
      let taskIdTemp = 0;
      this.state.tasks.forEach(t => {
        if (t.id > this.state.taskId)
          taskIdTemp = this.state.taskId;
        sigmaTemp += t.czas;
      })
      taskIdTemp++;
      this.setState({taskId: taskIdTemp});
      this.setState({sigma: sigmaTemp});
    }

    handleSaveTask(data) {
      this.setState({sigma: this.state.sigma + data.span});
      let newTask = {id:this.state.taskId, zadanie: data.name, czas: data.span};
      this.setState({taskId: this.state.taskId+1});
      this.setState(prevState => ({
          tasks: [...prevState.tasks, newTask],
      }));
    };

    printSpan(span, milis=false) {
      if ((typeof span) != 'number' || span < 0)
          return '*** złe dane!'
      let result = "";
      const second = 1000;
      const minute = second * 60;
      const hour = minute * 60;
      const day = hour * 24;
      const month = day * 31;
      if (span > month)
          return '*** ponad miesiąc!'
      let days = Math.floor(span / day);
      span -= days * day;
      let hours = Math.floor(span / hour);
      span -= hours * hour;
      let minutes = Math.floor(span / minute);
      span -= minutes * minute;
      let seconds = Math.floor(span / second);
      span -= seconds * second;
      if (days > 0)
          result += days.toString().padStart(2, '0') + ' ';
      result += hours.toString().padStart(2, '0') + ':';
      result += minutes.toString().padStart(2, '0') + ':';
      result += seconds.toString().padStart(2, '0');
      if (milis)
        result += ',' + span.toString().padStart(3, '0');
      return result;
    }

    render() {
      const tasks = this.state.tasks.map(task => {
        return <Done key={task.id} zadanie={task.zadanie} czas={this.printSpan(task.czas)} />
      })
      return (
      <div className="App">
        <Header sigma={this.printSpan(this.state.sigma)}/>
        <Entry onSave={(data) =>{ this.handleSaveTask(data)}} />
        {tasks}
      </div>
    );
  }
}

export default App;
