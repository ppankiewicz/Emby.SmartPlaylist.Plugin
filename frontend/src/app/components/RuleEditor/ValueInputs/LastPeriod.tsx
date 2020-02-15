import * as React from 'react';
import { LastPeriodValue, Period } from '~/app/types/rule';
import { Input } from '~/common/components/Input';
import { Select } from '~/common/components/Select';
import { PeriodValues } from '~/app/app.const';

type LastPeriodProps = {
    value: LastPeriodValue;
    onChange(value: LastPeriodValue): void;
};

export const LastPeriod: React.FC<LastPeriodProps> = props => {
    const lastPeriodValue = props.value;

    return (
        <>
            <Input
                value={lastPeriodValue.value}
                type="number"
                onBlur={e =>
                    props.onChange({
                        ...lastPeriodValue,
                        value: Number(e.target.value),
                    })
                }
            />
            <Select
                values={PeriodValues.map(x => x)}
                value={lastPeriodValue.periodType}
                onChange={newValue =>
                    props.onChange({
                        ...lastPeriodValue,
                        periodType: newValue as Period,
                    })
                }
            />
        </>
    );
};
