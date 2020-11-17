import React from 'react';

import UnityLoader from './panelComponents/UnityLoader.js';

const Panel = props => {
	return(
            <div id="unity-canvas">
                <UnityLoader unityContent={props.unityContent}/>
            </div>
	);
}

export default Panel;

/*
                <div className="App-panel">
                        <RandomColourButton unityContent={props.unityContent}/>
                        <CookieBaker unityContent={props.unityContent}/>
                        <Score unityContent={props.unityContent}/>
                        <UnityLoader unityContent={props.unityContent}/>
                </div>

*/






