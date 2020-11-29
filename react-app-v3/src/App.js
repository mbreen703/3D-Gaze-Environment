/* App.js */
//Imports
import React from 'react';
import Unity, { UnityContent } from 'react-unity-webgl';
import ReactDOM, { unmountComponentAtNode } from 'react-dom';

//import Panel from "./contents/Panel.js";
//import './App.css';
import './index.css';
/* ----------------------------------------------- */

//App component--main container component
class App extends React.Component {
    constructor(props) {
        super(props);
        
        this.state = {
            isLoaded_A: false,
            isLoaded_B: false
        };

        //Click event bindings
        this.clickedA = this.clickedA.bind(this);
        this.clickedB = this.clickedB.bind(this);
        this.unmountBTN = this.unmountBTN.bind(this);
        this.resetBTN = this.resetBTN.bind(this);
        this.toggleRealtime = this.toggleRealtime.bind(this);
        this.toggleAll = this.toggleAll.bind(this);
        //this.fullscreenMode = this.fullscreenMode.bind(this);
        
        //Click event listeners
        document.getElementById('interviewA').addEventListener('click', this.clickedA);
        document.getElementById('interviewB').addEventListener('click', this.clickedB);
        document.getElementById('endScene').addEventListener('click', this.unmountBTN);
        document.getElementById('reset-realtime').addEventListener('click', this.resetBTN);
        document.getElementById('show-realtime').addEventListener('click', this.toggleRealtime);
        document.getElementById('show-all-data').addEventListener('click', this.toggleAll);
        //document.getElementById('unity-fullscreen-button').addEventListener('click', this.fullscreenMode);
        
        //Reference to invoked child component
        this.child = React.createRef();

    } // END OF CONSTRUCTOR
    
    //Handle clickedA--interview A scene to be loaded
    clickedA = () => {
        
        //Set state
        this.setState({
            isLoaded_A: true
        });

    } //END OF clickedA

    //Handle clickedB--interview B scene to be loaded
    clickedB = () => {
        
        //Set state
        this.setState({
            isLoaded_B: true
        });
        
    } //End of clickedB

    //Unmount Component for child component to handle
    unmountBTN = () => {
        if(this.state.isLoaded_A === true || this.state.isLoaded_B === true) {

            this.child.current.handleUnmount();
        }
    }
    //Reset data for child component to handle
    resetBTN = () => {
        if (this.state.isLoaded_B === true) {
            this.child.current.handleReset();
        }
    }
    //Toggle realtime data for child component to handle
    toggleRealtime = () => {
        if (this.state.isLoaded_B === true) {
            this.child.current.handleRealtime();
        }
    }
    //Toggle conglomerate data for child component to handle
    toggleAll = () => {
        if (this.state.isLoaded_B === true) {
            this.child.current.handleAll();
        }
    }
    
    //Render data to DOM
    render() {
        //Determine what to render
        let getClick;

        //If interview A selected--invoke Interview A child component
        if (this.state.isLoaded_A === true) {
            getClick = <InterviewA ref={this.child} />;
        }
        //If interview B selected--invoke Interview B child component
        else if (this.state.isLoaded_B === true) {
            getClick = <InterviewB ref={this.child} />;
        }
        //If neither have yet to be loaded, display pre-mounting state
        else
            getClick = "- MAKE A SELECTION -";
        
        return (
            <div>
                {getClick}
            </div>
        );
    }
}

//Child component--InterviewA
class InterviewA extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoaded_A: true,
            unmountFlag: false
        };
        //Instantiate unity instance
        this.unityContent = new UnityContent(
            'InterviewA/Build/InterviewA.json',
            'InterviewA/Build/UnityLoader.js'
        );

    } //END OF CONSTRUCTOR

    //Mount Component--communicate with Unity scene to be loaded
    componentDidMount() {
        this.unityContent.send("_appManager", "_sceneLoader");
    }

    /*
    //Update Component--communicate DOM updates to Unity
    componentDidUpdate() {
        if (this.state.unmountFlag === true) {
            var uInstance = document.getElementById('unity-canvas');
            unmountComponentAtNode(uInstance);
        }
    }
    */

    //Handle unmount click so component knows to update
    handleUnmount = () => {
        this.setState({
            unmountFlag: true
        });
    }

    /*
    componentWillUnmount() {
        //TESTING
        if (this.state.unmountFlag === true) {
            this.unityContent.Quit(function() {
                alert("Component should be unmounting");
            });
            this.unityContent = null;
        }
    }
    */

    render() {
        let unityStatus;
        
        if (this.state.unmountFlag === false) {
            unityStatus = <Unity unityContent={this.unityContent} />;
        }
        else if (this.state.unmountFlag === true) {
            var uInstance = document.getElementById('unity-canvas');
            unmountComponentAtNode(uInstance);
        }   
        
        //Return updated state to main App component
        return (
            <div>
                {unityStatus}
            </div>
        );
    }
}

//Child component--InterviewB
class InterviewB extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoaded_B: true,
            unmountFlag: false,
            resetFlag: false,
            realtimeFlag: false,
            conglomFlag: false
        };
        //Instantiate unity instance
        this.unityContent = new UnityContent(
            'InterviewB/Build/InterviewB.json',
            'InterviewB/Build/UnityLoader.js'
        );

    } //END OF CONSTRUCTOR

    //Mount Component--communicate with Unity scene to be loaded
    componentDidMount() {
        //this.setState
        this.unityContent.send("_appManager", "_sceneLoader");
    }
    //Update Component--communicate DOM updates to Unity
    componentDidUpdate() {
        
        if(this.state.realtimeFlag === true) {
            this.unityContent.send("PointHeatMap", "ToggleRealTimeData");
        }
        else if(this.state.conglomFlag === true) {
            this.unityContent.send("PointHeatMap", "ToggleConglomerateData");
        }
        else if(this.state.resetFlag === true) {
            this.unityContent.send("PointHeatMap", "ResetRealTimeData");
        }
    }
    
    /*
    //Unmount Component
    componentWillUnmount() {
        if (this.state.unmountFlag === true) {
            var uInstance = document.getElementById('unity-canvas');
            unmountComponentAtNode(uInstance);
        }
    }
    */

    //Handle unmount click so component knows to update
    handleUnmount = () => {
        this.setState({
            unmountFlag: true
        });
    }
    //Handle reset click so component knows to update
    handleReset = () => {
        this.setState({
            resetFlag: true
        });
    }
    //Handle realtime click so component knows to update
    handleRealtime = () => {
        this.setState({
            realtimeFlag: true
        });
    }
    //Handle conglomerate click so component knows to update
    handleAll = () => {
        this.setState({
            conglomFlag: true
        });
    }

    render() {
        let unityStatus;
        
        if (this.state.unmountFlag === false) {
            unityStatus = <Unity unityContent={this.unityContent} />;
        }
        else if (this.state.unmountFlag === true) {
            var uInstance = document.getElementById('unity-canvas');
            unmountComponentAtNode(uInstance);
        }
        
        //Return updated state to main App component
        return (
            <div>
                {unityStatus}
            </div>
        );
    
    }
}

export default App;





