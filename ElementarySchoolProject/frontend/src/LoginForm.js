import React from 'react';
import UsersList from './UsersList';


class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userName: '',
            password: '',
            token: '',
            loginSuccess: false
        }

        
    }    

    handleSubmit = async (event) => {
        event.preventDefault();

        let getTokenEndpoint = 'http://localhost:57847/token';

        let response = await fetch(getTokenEndpoint, {
            method: 'POST',
            header: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: `grant_type=password&userName=${event.target.username.value}&password=${event.target.password.value}`
        });

        let jsonedResponse = await response.json();
        let myToken = jsonedResponse.access_token;

        if (myToken) {
            //alert(myToken);
            this.setState({
                token: myToken,
                loginSuccess: true
            })
        }        
    }

    // handleSubmit(event) {
    //     event.preventDefault();

    //     const getTokenEndpoint = 'http://localhost:57847/token';
    //     const usr = event.target.username.value;
    //     const pwd = event.target.password.value;

    //     fetch(getTokenEndpoint, {
    //         method: 'POST',
    //         header: {
    //             'Content-Type': 'application/x-www-form-urlencoded'
    //         },
    //         body: `grant_type=password&userName=${usr}&password=${pwd}`
    //     })
    //         .then(response => response.json())
    //         .then(json => {
    //             console.log('token is: ', json.access_token);
    //             sessionStorage.setItem('token', json.access_token);
    //             this.setState({
    //                 userName: usr,
    //                 password: pwd,
    //                 token: `${json.access_token}`
    //             }, function () {
    //                 console.log(this.state.value);
    //             })
                
    //         })            
    //         .catch(error => {
    //             console.log('error: ', error)
    //         })
    // }
    

    render() {
        if (this.state.loginSuccess) {
            return (
                <UsersList token={this.state.token}/>
            );
        } else {
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
}

export default LoginForm;