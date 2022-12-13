import React, { useState, useEffect } from 'react';
import API from '../../Api';

export const Logs = () => {

    const [logs, setLogs] = useState([]);

    useEffect(() => {
        const handleLogs = () => {
            API.get('api/Logs/logs/getAllLogs')
            .then(response => {
                setLogs(response.data);
            })
            .catch(error => {
                console.log(error);
                setLogs([]);
            });
        }
        handleLogs();
    }); 
    
    return <div>
        <table class="table table-striped">
            <thead>
                <tr>
                <th>#</th>
                <th>Date</th>
                <th>IP</th>
                <th>Method name</th>
                <th>Request</th>
                <th>URL</th>
                <th>Username</th>
                </tr>
            </thead>
            <tbody>
            {logs.map(log => (
                <tr>
                <td>{ log.id }</td>
                <td>{ log.date }</td>
                <td>{ log.ip }</td>
                <td>{ log.methodName }</td>
                <td>{ log.request }</td>
                <td>{ log.url }</td>
                <td>{ log.userName }</td>
                </tr>
            ))}
            </tbody>
        </table>
    </div>;
}

export default Logs;