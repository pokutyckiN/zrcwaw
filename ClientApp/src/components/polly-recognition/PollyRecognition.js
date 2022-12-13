import React, { useState, useEffect } from 'react';
import supportedLanguages from './Languages';
import API from '../../Api';
import LanguageSelect from './LanguageSelect';

export const PollyRecognition = () => {

    const [text, setText] = useState("");
    const [languages, setLanguages] = useState([]);

    const getPollyCode = (comprehendCode) => 
        supportedLanguages.find(l => l.comprehendCode === comprehendCode)?.pollyCode;

    useEffect(() => {
        const getVoice = async () => {
            const resLanguages = await API.post('api/comprehend', {text: text});
            const logsData = {
                url: window.location.href,
                ip: window.location.href,
                request: JSON.stringify({text: text}),
                methodName: 'api/comprehend'
            }
    
            API.post('api/logs/Logs/insertLog', logsData)
            .then(() => {
                console.log(logsData)
            })
            .catch(() => {
            });

            setLanguages(resLanguages.data);
            
            const code = resLanguages.data[0].languageCode;
            if (code) {
                selectElement("select", getPollyCode(code));     
            }
        }
        
        if (text) {
            getVoice();
        }
    }, [text]);

    const onInputChange = (event) => setText(event.target.value);
    const selectElement = (id, value) => document.getElementById(id).value = value;

    const playVoice = async () => {
        const code = document.getElementById("select").value;
        
        if (code) {
            const payload = {
                text: text,
                languageCode: code
            }

            const resVoice = await API.post('api/polly', payload, { responseType: "arraybuffer" });
            const logsData = {
                url: window.location.href,
                ip: window.location.href,
                request: JSON.stringify({responseType: "arraybuffer"}),
                methodName: 'api/polly'
            }
    
            API.post('api/logs/Logs/insertLog', logsData)
            const audioContext = new AudioContext();
            audioContext.decodeAudioData(resVoice.data, buffer => {
                const source = audioContext.createBufferSource();
                source.buffer = buffer;
                source.connect(audioContext.destination);
                source.start(0);
            });
        }
    }

    return <div>
        <span>
            <label htmlFor="text">Write some text here: </label>
            <input id="text" type="text" onChange={onInputChange} />
            <button onClick={playVoice}>Play voice</button>
            <LanguageSelect />
        </span>
        <hr />
        
        {languages.length > 0 && 
            <>
                <div>
                    {languages.map(language => (
                        <div key={language.languageCode}>
                            <p>Language code: {language.languageCode}</p>
                            <p>Score: {language.score}</p>
                        </div>
                    ))}
                </div>
                <hr />
            </>
        }
    </div>;
}

export default PollyRecognition;