/* index.js */
//Imports
import React from 'react';
import ReactDOM from 'react-dom';
import $ from 'jquery';

//import Unity, { UnityContent } from 'react-unity-webgl';

import './index.css';
import App from './App';

//import reportWebVitals from './reportWebVitals';
//import * as serviceWorker from './serviceWorker';
/* --------------------------------------------------- */

//Render the DOM imported from App component
ReactDOM.render(
  <App />,
  document.getElementById('unity-canvas')
);

//reportWebVitals();

