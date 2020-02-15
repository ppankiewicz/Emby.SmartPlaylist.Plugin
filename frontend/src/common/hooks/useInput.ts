import * as React from 'react';

export const useInput = (
    initialValue: string | number | string[],
): React.InputHTMLAttributes<HTMLInputElement> => {
    const [value, setValue] = React.useState(initialValue);

    return {
        value: value,
        onChange: event => {
            setValue(event.target.value);
        },
    };
};
