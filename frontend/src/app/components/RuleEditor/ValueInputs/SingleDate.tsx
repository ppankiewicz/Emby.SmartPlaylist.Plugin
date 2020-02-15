import * as React from 'react';
import { DateInput } from '~/common/components/DateInput';
import { DateValue } from '~/app/types/rule';

type SingleDateProps = {
    value: DateValue;
    onChange(value: DateValue): void;
};

export const SingleDate: React.FC<SingleDateProps> = props => {
    return (
        <DateInput
            type="date"
            value={props.value.value}
            onChange={d =>
                props.onChange({
                    ...props.value,
                    value: d,
                })
            }
        />
    );
};
