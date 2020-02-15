import * as React from 'react';
import { Input as InputBase, InputProps } from '~/common/components/Input';
import './Input.css';
import './Base.css';
import { EmbyProps, parseEmbyProps } from '~/emby/components/embyProps';

type EmbyInputProps = InputProps & EmbyProps;

export const Input: React.FC<EmbyInputProps> = props => {
    const embyProps = parseEmbyProps(props);
    embyProps.class = 'input-padding-fix';
    let wrapperClass = 'inputContainer';
    if (!props.label) {
        wrapperClass += ' inline';
    }
    if (props.maxWidth) {
        wrapperClass += ' max-width';
    }
    const input = <InputBase {...embyProps} is="emby-input" />;
    const wrappedInput = <div className={wrapperClass}>{input}</div>;

    return <>{wrappedInput}</>;
};
