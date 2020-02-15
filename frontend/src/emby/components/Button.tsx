import * as React from 'react';
import { Button as ButtonBase, ButtonProps } from '~/common/components/Button';
import './Button.css';
import { EmbyProps, parseEmbyProps } from '~/emby/components/embyProps';

type EmbyButtonProps = ButtonProps & EmbyProps;

export const Button: React.FC<EmbyButtonProps> = props => {
    let className = 'raised emby-button ';
    className += props.type === 'submit' ? 'button-submit ' : '';
    className += props.isBlock ? 'block ' : '';

    const embyProps = parseEmbyProps(props);
    embyProps.class = className;

    return (
        <ButtonBase is="emby-button" {...embyProps}>
            {props.children}
        </ButtonBase>
    );
};
