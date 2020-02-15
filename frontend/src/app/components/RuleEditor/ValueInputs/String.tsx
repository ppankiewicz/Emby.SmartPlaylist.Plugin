import * as React from 'react';
import { StringValue } from '~/app/types/rule';
import { Input } from '~/common/components/Input';

type StringValueInputProps = {
    value: StringValue;
    onChange(value: StringValue): void;
};

export const StringValueInput: React.FC<StringValueInputProps> = props => {
    return (
        <Input
            value={props.value.value}
            onBlur={e =>
                props.onChange({
                    ...props.value,
                    value: e.target.value,
                })
            }
        />
    );
};
