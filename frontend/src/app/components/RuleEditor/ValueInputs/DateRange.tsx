import * as React from 'react';
import { DateRangeValue } from '~/app/types/rule';
import { DateInput } from '~/common/components/DateInput';
import { parseOrDefault } from '~/common/helpers/date';
import { Label } from '~/common/components/Label';

type DateRangeProps = {
    value: DateRangeValue;
    onChange(value: DateRangeValue): void;
};

export const DateRange: React.FC<DateRangeProps> = props => {
    const fromDate = parseOrDefault(props.value.from);
    const toDate = parseOrDefault(props.value.to);

    return (
        <>
            <DateInput
                type="date"
                value={fromDate}
                onChange={d =>
                    props.onChange({
                        ...props.value,
                        from: d,
                    })
                }
            />
            <Label>to:</Label>
            <DateInput
                type="date"
                value={toDate}
                onChange={d =>
                    props.onChange({
                        ...props.value,
                        to: d,
                    })
                }
            />
        </>
    );
};
