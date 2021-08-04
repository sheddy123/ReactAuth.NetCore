import React, { useState, useEffect } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Login from './Pages/Login/Login';
import Register from './Pages/Register/Register';
import Home from './Pages/Home/Home';
import axios from 'axios';

import './custom.css';
import { BrowserRouter } from 'react-router-dom';

const App = () => {
  const [name, setName] = useState('');
  useEffect(() => {
    axios.get('/api/Auth/GetUser').then(
      (response) => {
        setName(response.data.userName);
        //  console.log(response.data);
      },
      (error) => {
        console.log(error);
      }
    );
    // (async () => {
    //   const response = await fetch('/api/Auth/GetUser', {
    //     method: 'GET',
    //     headers: { 'Content-Type': 'application/json' },
    //     credentials: 'include',
    //   });
    //   const content = response.json();
    //   console.log(content);
    //   setName(content.userName);
    // })();
  }, []);

  return (
    <BrowserRouter>
      <Layout name={name} setName={setName}>
        <Route exact path='/' component={() => <Home name={name} />} />
        <Route path='/login' component={() => <Login setName={setName} />} />
        <Route path='/register' component={Register} />
      </Layout>
    </BrowserRouter>
  );
};

export default App;
