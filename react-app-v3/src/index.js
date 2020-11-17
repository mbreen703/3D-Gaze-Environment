import React from 'react';
//import Unity, { UnityContent } from 'react-unity-webgl';
import ReactDOM from 'react-dom';
import $ from 'jquery';
import './index.css';
//import './contents/helper.js';
import App from './App';
//import reportWebVitals from './reportWebVitals';
//import * as serviceWorker from './serviceWorker';



ReactDOM.render(
  <App />,
  document.getElementById('unity-canvas')
);

  /*
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('unity-canvas')
  //document.getElementById('root')
  */


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
//reportWebVitals();

