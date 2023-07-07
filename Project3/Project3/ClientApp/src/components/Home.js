import React, { Component } from 'react';
export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            login: "",
            pasword: "",
            
        };
    }
    changelogin = (e) => {
        this.setState({ login: e.target.value });
    }
    changepasword = (e) => {
        this.setState({ pasword: e.target.value });
    }

    login() {
        
        const {
            login,
            pasword,
        } = this.state
        fetch('login', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login: login,
                password: pasword,
            })
        }).then(response => { response.status===200 ? window.$logged = true : window.$logged = false })
            .then((result) => {  
                window.$login = login;
                window.$password = pasword;
                this.forceUpdate();
            }, (error) => {
                this.forceUpdate();
            })
    }


        logout() {
            this.setState({ pasword: ""});
            this.setState({ login: "" });
            window.$logged = false;
            this.forceUpdate();
        }


    render() {
        const {
            login,
            pasword,
        } = this.state
        return (
            <div>
                <h3>login</h3>
                <input disabled={window.$logged} type="text" className="form-control"
                    value={login }
                    onChange={this.changelogin} /><br />
                <h3>haslo</h3>
                <input disabled={window.$logged} type="text" className="form-control"
                    value={pasword}
                    onChange={this.changepasword} /><br />
                {window.$logged ?
                    <button onClick={() => this.logout()}>logout</button>:
                <button onClick={() => this.login()}>login</button>}

            </div>
        );
    }
}
