import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Login from './Pages/Login/Login';
import Register from './Pages/Register/Register';
import Home from './Pages/Home/Home';

import './custom.css';
import { BrowserRouter } from 'react-router-dom';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <BrowserRouter>
        <Layout>
          <Route exact path='/' component={Home} />
          <Route path='/login' component={Login} />
          <Route path='/register' component={Register} />
        </Layout>
      </BrowserRouter>
    );
  }
}
