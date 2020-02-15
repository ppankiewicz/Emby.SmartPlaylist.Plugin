import * as React from 'react';
import { CheckBox as CheckBoxBase, CheckBoxProps } from '~/common/components/CheckBox';
import { EmbyProps, parseEmbyProps } from '~/emby/components/embyProps';
import './CheckBox.css';

type EmbyCheckboxProps = EmbyProps & CheckBoxProps;

export const CheckBox: React.FC<EmbyCheckboxProps> = props => {
    const embyProps = parseEmbyProps(props);
    return (
        <label className="checkbox-container">
            <CheckBoxBase is="emby-checkbox" {...embyProps}>
                {props.children}
            </CheckBoxBase>
            {props.label && <span className="checkboxLabel">{props.label}</span>}
        </label>
    );
};
