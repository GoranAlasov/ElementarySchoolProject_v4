import React from 'react';

class LoginForm extends React.Component {
    constructor() {
        super();
        this.state = {
            userName: '',
            password: '',
            token: ''
        }

        this.handleSubmit = this.handleSubmit.bind(this);
    }    

    handleSubmit(event) {
        event.preventDefault();

        const getTokenEndpoint = 'http://localhost:57847/token';
        const usr = event.target.username.value;
        const pwd = event.target.password.value;

        fetch(getTokenEndpoint, {
            method: 'POST',
            header: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: `grant_type=password&userName=${usr}&password=${pwd}`
        })
            .then(response => response.json())
            .then(json => {
                console.log('token is: ', json.access_token);
                sessionStorage.setItem('token', json.access_token);
                this.setState({
                    userName: usr,
                    password: pwd,
                    token: `${json.access_token}`
                }, function () {
                    console.log(this.state.value);
                })
                
            })            
            .catch(error => {
                console.log('error: ', error)
            })
    }

    render() {
        return (
            <div className="loginDiv">
                <form onSubmit={this.handleSubmit}>
                    <label>Username</label><br />
                    <input type="text" name="username" ></input><br />
                    <label>Password</label><br />
                    <input type="password" name="password" ></input><br /><br />
                    <input type="submit" value="Log In"></input>
                </form>
            </div>
        );
    }
}

export default LoginForm;