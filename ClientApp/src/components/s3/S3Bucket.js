import React, { useState } from 'react';
import API from '../../Api';

export const S3Bucket = ({ bucket }) => {

    const name = bucket.bucketName;
    let fileReader;

    const handleFileRead = (e) => {
        
    }

    const handleFileChosen = (file) => {
        fileReader = new FileReader();
        fileReader.onloadend = handleFileRead;
        fileReader.readAsText(file);
        const postBody = { 
            'name': file.name, 
            'bucketName': name,
            'type': file.type,
        }
        console.log(postBody);
        API.post('api/s3', postBody);

        const logsData = {
            url: window.location.href,
            ip: window.location.href,
            request: JSON.stringify({postBody}),
            methodName: 'api/s3'
        }

        API.post('api/logs/Logs/insertLog', logsData)
    }

    return <div>
        <h1>
            Name: {name}
        </h1>
        <input type="file" onChange={e => handleFileChosen(e.target.files[0])} />
        <hr />
    </div>;
}

export default S3Bucket;