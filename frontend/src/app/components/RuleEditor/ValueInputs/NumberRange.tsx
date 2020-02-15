import * as React from 'react';
import { NumberRangeValue } from '~/app/types/rule';
import { Input } from '~/common/components/Input';
import { Label } from '~/common/components/Label';

type NumberRangeValueInputProps = {
    value: NumberRangeValue;
    onChange(value: NumberRangeValue): void;
};

export const NumberRangeValueInput: React.FC<NumberRangeValueInputProps> = props => {
    return (
        <>
            <Input
                value={props.value.from}
                type="number"
                onBlur={e =>
                    props.onChange({
                        ...props.value,
                        from: e.target.valueAsNumber,
                    })
                }
            />
            <Label>to:</Label>
            <Input
                value={props.value.to}
                type="number"
                onBlur={e =>
                    props.onChange({
                        ...props.value,
                        to: e.target.valueAsNumber,
                    })
                }
            />
        </>
    );
};
