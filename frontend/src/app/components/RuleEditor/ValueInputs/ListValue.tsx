import * as React from 'react';
import { ListValue } from '~/app/types/rule';
import { Select } from '~/common/components/Select';

type StringValueInputProps = {
    value: ListValue;
    values: ListValue[];
    onChange(value: ListValue): void;
};

export const ListValueInput: React.FC<StringValueInputProps> = props => {
    const { onChange } = props;
    const values = props.values.map(x => x.value);
    const value = props.value.value;
    const listValue = props.value;

    return (
        <Select
            values={values}
            value={value}
            onChange={newVal =>
                onChange({
                    ...listValue,
                    ...props.values.find(x => x.value === newVal),
                    value: newVal,
                })
            }
        />
    );
};
