import * as React from 'react';

export type CheckBoxProps = { label?: string } & React.InputHTMLAttributes<HTMLInputElement>;

export const CheckBox: React.FC<CheckBoxProps> = props => {
    return <input type="checkbox" {...props} />;
};
