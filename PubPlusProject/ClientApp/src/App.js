import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import {NotFound} from './components/NotFound';
import {StatusesScreen} from './components/StatusesScreen';
import './custom.css'

import { BrowserRouter,Switch} from 'react-router-dom';

export default class App extends Component {
  static displayName = App.name;
  render () {
    return (
      <Layout>
        <BrowserRouter>
        <Switch>
          <Route exact path='/' component={Home} />
          <Route path='/statuses' component={StatusesScreen} />
          <Route path='*' component={NotFound}/>
        </Switch>
        </BrowserRouter>
      </Layout>
    );
  }
}
