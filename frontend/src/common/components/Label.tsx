import * as React from 'react';

export const Label: React.FC<{}> = props => {
    return <div style={{ margin: 'auto 0', padding: '0 .25em' }}>{props.children}</div>;
};
