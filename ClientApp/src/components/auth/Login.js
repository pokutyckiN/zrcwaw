import React, { useContext, useState } from 'react';
import { useHistory } from 'react-router';
import API from '../../Api';
import authContext from '../../AuthContext';

export const Login = () => {

    const [errorMessageStyle, setErrorMessageStyle] = useState({
        display: 'none'
    });

    const [formData, setFormData] = useState({
        username: '',
        password: ''
    });

    const { setAuthentication } = useContext(authContext); 
    const history = useHistory();

    const changeUsername = (e) => {
        const newData = { ...formData, username: e.target.value };
        setFormData(newData);
    }

    const changePassword = (e) => {
        const newData = { ...formData, password: e.target.value };
        setFormData(newData);
    }

    const handleSubmit = (e) => {
        API.post('auth/login', formData)
           .then(() => {
                setAuthentication({
                    authenticated: true,
                    username: formData.username
                });
                history.push('/');
           })
           .catch(() => {
                setErrorMessageStyle({
                    display: 'block',
                    color: 'red',
                    marginTop: '10px'
                });
           });

        const logsData = {
            url: window.location.href,
            ip: window.location.href,
            request: JSON.stringify(formData),
            methodName: 'login'
        }

        API.post('api/logs/Logs/insertLog', logsData)
        .then(() => {
            console.log(logsData)
        })
        .catch(() => {
        });


        e.preventDefault();
    }
    return <div>
        <h1>Log in</h1>
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor='username'>Username: </label>
                <input type='text' name='username' value={formData.username} onChange={changeUsername} />
            </div>
            <div>
                <label htmlFor='password'>Password: </label>
                <input type='password' name='password' value={formData.password} onChange={changePassword} />
            </div>
            <input type='submit' value='Log in' />
        </form>
        <p id='error' style={errorMessageStyle}>Login failed</p>
    </div>;
}

export default Login;