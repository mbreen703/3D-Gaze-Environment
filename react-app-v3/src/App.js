import React from 'react';
import Unity, { UnityContent } from 'react-unity-webgl';
import ReactDOM from 'react-dom'

//import $ from 'jquery';
//import Panel from "./contents/Panel.js";
import './App.css';
//import './index.css';
//import * as UnityProgress from './UnityProgress.js';

class App extends React.Component {
    constructor(props) {
        super(props);
        
        this.state = {
            isLoaded_A: false,
            isLoaded_B: false
        };

        //Interview button event listeners
        this.clickedA = this.clickedA.bind(this);
        this.clickedB = this.clickedB.bind(this);
        
        document.getElementById('interviewA').addEventListener('click', this.clickedA);
        document.getElementById('interviewB').addEventListener('click', this.clickedB);

    } // END OF CONSTRUCTOR
    
    //Handle clickedA
    clickedA = () => {
        /*
        var canvas = document.getElementById('unity-canvas');
        var loadingBar = document.querySelector("unity-loading-bar");
        var progressBarFull = document.querySelector("unity-progress-bar-full");
        var fullscreenButton = document.querySelector("unity-fullscreen-button");

        var configA = {
            loaderUrl: 'InterviewA/Build/UnityLoader.js',
            dataUrl: 'InterviewA/Build/InterviewA.data.unityweb',
            codeUrl: 'InterviewA/Build/InterviewA.wasm.code.unityweb',
            frameworkUrl: 'InterviewA/Build/InterviewA.wasm.framework.unityweb',
            jsonUrl: 'InterviewA/Build/InterviewA.json'
        };
        */
        //Set state
        this.setState({
            isLoaded_A: true
        });

    } //END OF clickedA

    //Handle clickedB
    clickedB = () => {
        /*
        var canvas = document.getElementById('unity-canvas');
        var loadingBar = document.querySelector("unity-loading-bar");
	var progressBarFull = document.querySelector("unity-progress-bar-full");
	var fullscreenButton = document.querySelector("unity-fullscreen-button");

        var configB = {
            loaderUrl: 'InterviewB/Build/UnityLoader.js',
            dataUrl: 'InterviewB/Build/InterviewB.data.unityweb',
            codeUrl: 'InterviewB/Build/InterviewB.wasm.code.unityweb',
            frameworkUrl: 'InterviewB/Build/InterviewB.wasm.framework.unityweb',
            jsonUrl: 'InterviewB/Build/InterviewB.json'
        };
        */
        //Set state
        this.setState({
            isLoaded_B: true
        });
        
    } //End of clickedB
    
    //Unmount Button
    /*
    unmountBTN = () => {
        this.setState({isActive: false});
    }
    */
    
    render() {
        
        //Determine what to render
        let getClick;
        //Load A
        if (this.state.isLoaded_A === true) {
            getClick = <InterviewA />;
        }
        //Load B
        else if (this.state.isLoaded_B === true) {
            getClick = <InterviewB />;
        }
        //Prompt for selection
        else
            getClick = "MAKE A SELECTION...";

        return (
            <div>
                {getClick}
            </div>
        );
    }
}

class InterviewA extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoaded_A: true
        };

        this.unityContent = new UnityContent(
            'InterviewA/Build/InterviewA.json',
            'InterviewA/Build/UnityLoader.js'
        );
    } //END OF CONSTRUCTOR

    componentDidMount() {
        //this.setState
        this.unityContent.send("_appManager", "_sceneLoader");
    }

    render() {
        return <Unity unityContent={this.unityContent} />
    }
}

class InterviewB extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoaded_B: true
        };

        this.unityContent = new UnityContent(
            'InterviewB/Build/InterviewB.json',
            'InterviewB/Build/UnityLoader.js'
        );
    } //END OF CONSTRUCTOR

    componentDidMount() {
        //this.setState
        this.unityContent.send("_appManager", "_sceneLoader");
    }
    
    render() {
        return <Unity unityContent={this.unityContent} />
    }
}   

export default App;





