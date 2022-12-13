import React, { useState } from 'react';
import API from '../../Api';

export const RekognitionObject = ({ object }) => {

    const [labels, setLabels] = useState([]);
    const [texts, setTexts] = useState([]);

    let bucketname = object.bucketName;
    let filename = object.key;

    const handleGetLabels = () => {
        API.post('api/rek/Rekognition/getLabels', { 'BucketName': bucketname, 'FileName': filename})
            .then(response => {
                setLabels(response.data.labels);
                console.log(labels)
            })
            .catch(error => {
                console.log(error);
            });

        const logsData = {
            url: window.location.href,
            ip: window.location.href,
            request: JSON.stringify({'BucketName': bucketname, 'FileName': filename}),
            methodName: 'Rekognition/getLabels'
        }

        API.post('api/logs/Logs/insertLog', logsData)
}

    const handleDetectText = () => {
        API.post('api/rek/Rekognition/getText', { 'BucketName': bucketname, 'FileName': filename})
            .then(response => {
                setTexts(response.data.textDetections);
                console.log(texts)
            })
            .catch(error => {
                console.log(error);
            });
        const logsData = {
            url: window.location.href,
            ip: window.location.href,
            request: JSON.stringify({'BucketName': bucketname, 'FileName': filename}),
            methodName: 'Rekognition/getText'
        }

        API.post('api/logs/Logs/insertLog', logsData)
    }

    return <div>
        <h1>
            Bucket name: {bucketname}
        </h1>
        <h1>
            File name: {filename}
        </h1>
        {labels.map(label => (
            <li key={label.name}>Etykieta: {label.name}</li>
        ))}
        {texts.map(text => (
            <li key={text.id}>Zidentyfikowany tekst: {text.detectedText}</li>
        ))}
        
        {/* <li>bucket type: {type}</li>
        <li>Public IP: {ip}</li>
        <li>State: {state}</li> */}
        
        <button onClick={handleGetLabels}>Get labels</button>
        <br/>
        <button onClick={handleDetectText}>Detect text</button>
        <hr />
    </div>;
}

export default RekognitionObject;