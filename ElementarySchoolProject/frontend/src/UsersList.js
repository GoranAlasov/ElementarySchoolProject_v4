import React from 'react';
import { isProperty } from '@babel/types';
import UserItems from './UserItems';

class UsersList extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            users: []
        }
    }

    getUsers = async () => {
        
        let getUsersEndpoint = 'http://localhost:57847/api/accounts';

        let response = await fetch(getUsersEndpoint, {
            method: 'GET',
            header: {
                'Content-Type': 'application/json; charset=utf-8',
                'Authorization': 'Bearer ' + this.props.token
            }
        });

        let jsonedResponse = await response.json();

        this.setState({
            users: {jsonedResponse}
        })
    }

    renderTableHeader = () => {
        return Object.keys(this.state.users[0]).map(attr => (
            <th>
                {attr.toUpperCase()}
            </th>
        ))
    }

    async componentDidMount(){
        this.getUsers();
    }    

    

    render () {
        if (this.state.users > 0) {
            return(
            <div>
                <table>
                    <thead>
                        <tr>
                            {this.renderTableHeader()}
                        </tr>
                    </thead>
                    <tbody>
                        <UserItems users={this.props.users} />
                    </tbody>
                </table>
            </div>
            );    
        } else {
            return(
                <div>No users.</div>
            );
        }        
    }    
}

export default UsersList;