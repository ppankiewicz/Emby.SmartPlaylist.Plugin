import * as React from 'react';

export const AutoSize: React.FC<{}> = props => {
    return <div style={{ width: 'auto' }}>{props.children}</div>;
};
