import * as React from 'react';
import { Input } from '~/common/components/Input';

type DateTimeType = 'date' | 'datetime-local';

type DateInputProps = {
    type: DateTimeType;
    value: Date;
    onChange(value: Date): void;
};

export const DateInput: React.FC<DateInputProps> = props => {
    const formatDate = (date: Date) => {
        if (date instanceof Date) {
            return date.toISOString().split('T')[0];
        }
        return undefined;
    };

    return (
        <>
            <Input
                value={formatDate(props.value)}
                type={props.type}
                onBlur={e => props.onChange(new Date(e.target.value))}
            />
        </>
    );
};
