import * as React from 'react';

export type ButtonProps = {
    isBlock?: boolean;
} & React.ButtonHTMLAttributes<HTMLButtonElement>;

export const Button: React.FC<ButtonProps> = props => {
    const { isBlock, ...buttonProps } = props;

    return (
        <button type="button" {...buttonProps}>
            {props.children}
        </button>
    );
};
