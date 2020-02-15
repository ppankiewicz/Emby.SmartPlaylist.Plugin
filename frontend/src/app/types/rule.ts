import { PeriodValues, RuleMatchTypes as GroupMatchModes } from '~/app/app.const';

export type Rule = {
    kind: 'rule';
    criteria: RuleCriteriaValue;
    id: string;
};

export type RuleGroup = {
    kind: 'ruleGroup';
    id: string;
    matchMode: GroupMatchMode;
};

export type RuleOrRuleGroup = Rule | RuleGroup;

export type CrtieriaValue =
    | StringValue
    | DateValue
    | LastPeriodValue
    | DateRangeValue
    | ListValue
    | NumberValue
    | NumberRangeValue;

export type RuleCriteriaValue = {
    name: string;
    operator: RuleCriteriaOperator;
    value: CrtieriaValue;
};

export type RuleCriteriaDefinition = {
    name: string;
    type: RuleCriteriaDefinitionType;
    values: CrtieriaValue[];
};

export type RuleCriteriaOperator = {
    name: string;
    type: OperatorType;
    defaultValue: CrtieriaValue;
};

export type CriteriaType = 'string' | 'date' | 'number' | 'listValue' | 'bool';
export type OperatorType =
    | 'string'
    | 'date'
    | 'lastPeriod'
    | 'dateRange'
    | 'listValue'
    | 'number'
    | 'numberRange'
    | 'bool';

export type RuleCriteriaDefinitionType = {
    name: CriteriaType;
    operators: RuleCriteriaOperator[];
};

export type StringValue = {
    kind: 'string';
    value: string;
};

export type NumberValue = {
    kind: 'number';
    value: number;
};

export type DateValue = {
    kind: 'date';
    value: Date;
};

export type LastPeriodValue = {
    kind: 'lastPeriod';
    value: number;
    periodType: Period;
};

export type DateRangeValue = {
    kind: 'dateRange';
    from: Date;
    to: Date;
};

export type NumberRangeValue = {
    kind: 'numberRange';
    from: number;
    to: number;
};

export type ListValue = {
    kind: 'listValue';
    value: string;
};

export type BoolValue = {
    kind: 'bool';
    value: boolean;
};

export type Period = typeof PeriodValues[number];
export type GroupMatchMode = typeof GroupMatchModes[number];
