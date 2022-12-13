import React, { useState, useEffect } from 'react';
import API from '../../Api';
import EC2Instance from './EC2Instance';

export const EC2Panel = () => {

    const [instances, setInstances] = useState([]);

    useEffect(() => {
        const getInstances = async () => {
            API.get('api/awsc2')
               .then(response => {
                setInstances(response.data);
            })
               .catch(error => {
                    console.log(error);
                    setInstances([]);
                });
            const logsData = {
                url: window.location.href,
                ip: window.location.href,
                request: JSON.stringify({})
            }
    
            API.post('api/logs/Logs/insertLog', logsData)
            .then(() => {
                console.log(logsData)
            })
            .catch(() => {
            });
        }
        const intervalId = setInterval(getInstances, 500);

        return () => clearInterval(intervalId);
    });

    return <div>
        {instances.map(instance => (
            <EC2Instance instance={instance} key={instance.instanceId} />
        ))}
    </div>;
}

export default EC2Panel;