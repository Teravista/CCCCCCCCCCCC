import React, { Component } from 'react';

export class Counter extends Component {
  static displayName = Counter.name;

  constructor(props) {
    super(props);
      this.state = {
          currentCount: 0,
          login: "",
          pasword: "",
      };
    this.incrementCounter = this.incrementCounter.bind(this);
  }
    changelogin = (e) => {
        this.setState({ login: e.target.value });
    }
    changepasword = (e) => {
        this.setState({ pasword: e.target.value });
    }
  incrementCounter() {
    this.setState({
      currentCount: this.state.currentCount + 1
    });
  }

  render() {
    return (
        <div>
      <h3>login</h3>
            <input type="text" className="form-control"
                onChange={this.changeDepartmentName} /><br />
            <h3>haslo</h3>
            <input type="text" className="form-control"
                            onChange={this.changeDepartmentName} /><br />
        <button>login</button>
      </div>
    );
  }
}
