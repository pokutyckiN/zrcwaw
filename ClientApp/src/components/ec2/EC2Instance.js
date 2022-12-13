import React, { useState, useEffect } from 'react';
import API from '../../Api';

export const EC2Instance = ({ instance }) => {

    const [action, setAction] = useState("");

    useEffect(() => {
        defineAction();
    });

    const defineAction = () => {
        if (instance.state.name.value === 'running') {
            setAction('STOP');
        }
        else {
            setAction('START');
        }
    }

    const toggleInstance = () => {
        const postBody = { 'instanceId': instance.instanceId };

        if (action === 'STOP') {
            API.post('awsc2/stop', postBody);
            const logsData = {
                url: window.location.href,
                ip: window.location.href,
                request: JSON.stringify(postBody),
                methodName: 'awsc2/stop'
            }
    
            API.post('api/logs/Logs/insertLog', logsData)
            .then(() => {
                console.log(logsData)
            })
            .catch(() => {
            });
        }
        else if (action === 'START') {
            API.post('awsc2/start', postBody);
            const logsData = {
                url: window.location.href,
                ip: window.location.href,
                request: JSON.stringify(postBody),
                methodName: 'awsc2/start'
            }
    
            API.post('api/logs/Logs/insertLog', logsData)
            .then(() => {
                console.log(logsData)
            })
            .catch(() => {
            });
        }
    }

    const id = instance.instanceId;
    const type = instance.instanceType.value;
    const ip = instance.networkInterfaces[0].association?.publicIp;
    const state = instance.state.name.value;

    return <div>
        <ul>
            <li>Id: {id}</li>
            <li>Instance type: {type}</li>
            <li>Public IP: {ip}</li>
            <li>State: {state}</li>
        </ul>
        
        <button onClick={toggleInstance} disabled={state === 'pending' || state === 'stopping'}>
            {action}
        </button>
        
        <hr />
    </div>;
}

export default EC2Instance;