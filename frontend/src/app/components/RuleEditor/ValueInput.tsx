import * as React from 'react';
import {
    CrtieriaValue,
    DateRangeValue,
    DateValue,
    LastPeriodValue,
    ListValue,
    NumberRangeValue,
    NumberValue,
    OperatorType,
    StringValue,
} from '~/app/types/rule';
import { SingleDate } from '~/app/components/RuleEditor/ValueInputs/SingleDate';
import { LastPeriod } from '~/app/components/RuleEditor/ValueInputs/LastPeriod';
import { DateRange } from '~/app/components/RuleEditor/ValueInputs/DateRange';
import { StringValueInput } from '~/app/components/RuleEditor/ValueInputs/String';
import { ListValueInput } from '~/app/components/RuleEditor/ValueInputs/ListValue';
import { NumberValueInput } from '~/app/components/RuleEditor/ValueInputs/NumberValue';
import { NumberRangeValueInput } from '~/app/components/RuleEditor/ValueInputs/NumberRange';

type ValueInputProps = {
    type: OperatorType;
    value: CrtieriaValue;
    values: CrtieriaValue[];
    onChange(value: CrtieriaValue): void;
};

export const ValueInput: React.FC<ValueInputProps> = props => {
    const { value, type, onChange, values } = props;

    const renderValueInput = () => {
        switch (type) {
            case 'string':
                return (
                    <StringValueInput
                        value={value as StringValue}
                        onChange={newVal => onChange(newVal)}
                    />
                );
            case 'date':
                return (
                    <SingleDate value={value as DateValue} onChange={newVal => onChange(newVal)} />
                );
            case 'lastPeriod':
                return (
                    <LastPeriod
                        value={value as LastPeriodValue}
                        onChange={newVal => onChange(newVal)}
                    />
                );
            case 'dateRange':
                return (
                    <DateRange
                        value={value as DateRangeValue}
                        onChange={newVal => onChange(newVal)}
                    />
                );
            case 'listValue':
                return (
                    <ListValueInput
                        value={value as ListValue}
                        values={values as ListValue[]}
                        onChange={newVal => onChange(newVal)}
                    />
                );
            case 'number':
                return (
                    <NumberValueInput
                        value={value as NumberValue}
                        onChange={newVal => onChange(newVal)}
                    />
                );
            case 'numberRange':
                return (
                    <NumberRangeValueInput
                        value={value as NumberRangeValue}
                        onChange={newVal => onChange(newVal)}
                    />
                );
            default:
                return <></>;
        }
    };

    return <>{renderValueInput()}</>;
};
