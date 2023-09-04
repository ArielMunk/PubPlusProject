import React, { Component } from 'react';
import Dashboard from './Dashboard';

export class StatusesScreen extends Component {
  constructor(props) {
    super(props);
    this.state = { currentCount: 0 };
  }

  /*incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }*/

  render() {
    return (
      <div>
       <Dashboard />
      </div>
    );
  }
}
