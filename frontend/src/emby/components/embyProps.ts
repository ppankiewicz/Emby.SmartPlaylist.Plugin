export type EmbyProps = {
    disabled?: boolean;
    class?: string;
};

export function parseEmbyProps<T extends EmbyProps>(props: T): Omit<T, 'disabled'> {
    const isDisabled = props.disabled;
    const { disabled, ...embyProps } = props;
    
    return {
        ...embyProps,
        ...(isDisabled && { disabled }),
    };
}
