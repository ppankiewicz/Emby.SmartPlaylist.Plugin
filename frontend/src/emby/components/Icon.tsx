import * as React from 'react';
import { IconProps } from '~/common/components/Icon';

export const Icon: React.FC<IconProps> = props => {
    return (
        <>
            {props.type === 'add' && <i className="md-icon">add</i>}
            {props.type === 'remove' && <i className="md-icon">remove</i>}
            {props.type === 'group' && <i className="md-icon">playlist_add</i>}
            {props.type === 'expanded' && <i className="md-icon">keyboard_arrow_down</i>}
            {props.type === 'collapsed' && <i className="md-icon">keyboard_arrow_right</i>}
        </>
    );
};
