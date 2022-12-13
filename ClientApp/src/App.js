import React, { useState } from 'react';
import { Redirect, Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import EC2Panel from './components/ec2/EC2Panel';
import S3Panel from './components/s3/S3Panel';
import RekognitionPanel from './components/rekognition/RekognitionPanel';
import PollyRecognition from './components/polly-recognition/PollyRecognition';
import Login from './components/auth/Login';
import authContext from './AuthContext';
import Logs from './components/logs/Logs';

import './custom.css'
import Logout from './components/auth/Logout';

export const App = () => {
  const [authentication, setAuthentication] = useState({
    authenticated: false,
    username: null
  });

  const authenticate = (component) => authentication.authenticated ? component : <Redirect to='/login' />;

  return (
  <authContext.Provider value={{ authentication, setAuthentication }}>
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/ec2'>{authenticate(<EC2Panel/>)}</Route>
        <Route path='/s3'>{authenticate(<S3Panel/>)}</Route>
        <Route path='/rekognition'>{authenticate(<RekognitionPanel/>)}</Route>
        <Route path='/polly-recognition'>{authenticate(<PollyRecognition/>)}</Route>
        <Route path='/login' component={Login} />
        <Route path='/logout' component={Logout} />
        <Route path='/logs'>{authenticate(<Logs/>)}</Route>
      </Layout>
  </authContext.Provider>
  );
}

export default App;