import React, { useState, useEffect } from 'react';
import API from '../../Api';
import RekognitionObject from './RekognitionObject';

export const RekognitionPanel = () => {

    const [objects, setObjects] = useState([]);

    useEffect(() => {
        const getObjects = () => {
            API.get('api/rek/Rekognition/getFiles')
                .then(response => {
                    setObjects(response.data.s3Objects);
                })
                .catch(error => {
                    console.log(error);
                    setObjects([]);
                });
                
        }

        const logsData = {
            url: window.location.href,
            ip: window.location.href,
            request: JSON.stringify({}),
            methodName: 'Rekognition/getFiles'
        }

        API.post('api/logs/Logs/insertLog', logsData)
        const intervalId = setInterval(getObjects, 500);

        return () => clearInterval(intervalId);
    }); 

    return <div>
        {objects.map(object => (
            <RekognitionObject object={object} key={object.key} />
        ))}
    </div>;
}

export default RekognitionPanel;