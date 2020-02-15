export function useOverrides<T>(
    overrides: Override,
    defaultComponent: React.ComponentType<T>,
): [React.ComponentType<T>, T] {
    const component = (overrides && overrides.component) || defaultComponent;
    const props = (overrides && overrides.props) || {};
    return [component, props];
}

export type Override<T = any> = {
    component?: React.ComponentType<T>;
    props?: Partial<T> | T;
};

export type OverrideProps<T = any> = Pick<Override<T>, 'props'>;
