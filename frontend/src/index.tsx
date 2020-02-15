import * as React from 'react';
import * as ReactDOM from 'react-dom';
import App from '~/app';

const appElements = document.querySelectorAll('#smartplaylist-root');
const appElement = appElements[appElements.length - 1];

ReactDOM.render(<App appId={appElement.getAttribute('data-app-id')} />, appElement);

if (process.env.NODE_ENV !== 'production') {
    // tslint:disable-next-line:no-var-requires
    const { whyDidYouUpdate } = require('why-did-you-update');
    whyDidYouUpdate(React);
}
