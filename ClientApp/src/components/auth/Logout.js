import React, { useContext, useEffect } from 'react';
import { Redirect } from 'react-router';
import authContext from '../../AuthContext';

export const Logout = () => {
    const { setAuthentication } = useContext(authContext); 

    useEffect(() => {
        setAuthentication({
            authenticated: false,
            username: null
        });
    });

    return <Redirect to='/' />;
}

export default Logout;