import * as React from 'react';

export type IconProps = {
    type: 'add' | 'remove' | 'group' | 'collapsed' | 'expanded';
};

export const Icon: React.FC<IconProps> = props => {
    return (
        <>
            {props.type === 'add' && '+'}
            {props.type === 'remove' && '-'}
            {props.type === 'group' && '#'}
            {props.type === 'expanded' && 'V'}
            {props.type === 'collapsed' && '>'}
        </>
    );
};
