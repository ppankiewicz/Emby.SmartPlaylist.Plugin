import * as React from 'react';

export const Inline: React.FC<{}> = props => {
    return <div style={{ width: '100%', display: 'inline-flex' }}>{props.children}</div>;
};
