import React from 'react';

function UserItems(props){
    let users = props.users;
    let retArray = [];

    for (const user of users) {
        retArray.push(
            <tr key={user.id}>
                {user}
            </tr>
        )
    }

    return (retArray);
}

export default UserItems;