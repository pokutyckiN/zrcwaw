import React from 'react';
import supportedLanguages from './Languages';

export const LanguageSelect = () => {
    return <div>
        <label htmlFor="select">Voice language: </label>
        <select id="select">
        {supportedLanguages.map(language => (
            <option key={language.name} value={language.pollyCode}>{language.name}</option>
            ))}
        </select>
    </div>;
}

export default LanguageSelect;