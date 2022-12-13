import React, { useState, useEffect } from 'react';
import API from '../../Api';
import S3Bucket from './S3Bucket';

export const S3Panel = () => {

    const [buckets, setBuckets] = useState([]);

    useEffect(() => {
        const getBuckets = () => {
            API.get('api/s3/getBuckets')
                .then(response => {
                    setBuckets(response.data.buckets);
                })
                .catch(error => {
                    console.log(error);
                    setBuckets([]);
                    
                });

            const logsData = {
                url: window.location.href,
                ip: window.location.href,
                request: JSON.stringify({}),
                methodName: 'api/s3/getBuckets'
            }
    
            API.post('api/logs/Logs/insertLog', logsData)
                
        }
        const intervalId = setInterval(getBuckets, 500);

        return () => clearInterval(intervalId);
    }); 

    return <div>
        {buckets.map(bucket => (
            <S3Bucket bucket={bucket} key={bucket.bucketName} />
        ))}
    </div>;
}

export default S3Panel;