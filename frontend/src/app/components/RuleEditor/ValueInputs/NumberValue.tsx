import * as React from 'react';
import { NumberValue } from '~/app/types/rule';
import { Input } from '~/common/components/Input';

type NumberValueInputProps = {
    value: NumberValue;
    onChange(value: NumberValue): void;
};

export const NumberValueInput: React.FC<NumberValueInputProps> = props => {
    return (
        <Input
            value={props.value.value}
            type="number"
            onBlur={e =>
                props.onChange({
                    ...props.value,
                    value: e.target.valueAsNumber,
                })
            }
        />
    );
};
