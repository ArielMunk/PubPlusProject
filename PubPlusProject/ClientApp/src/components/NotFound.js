import React, { Component } from 'react';
import {Link} from 'react-router-dom';

export class NotFound extends Component
 {
    render (){
    return (
        <div>
            <h1>Oops! You seem to be lost.</h1>
            <Link to="/">Sign-in Page</Link>

        </div>
    )
}
}
