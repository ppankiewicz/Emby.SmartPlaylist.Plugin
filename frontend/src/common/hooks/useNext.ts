import * as React from 'react';

export function useNext<T>(value: T) {
    const valueRef = React.useRef(value);
    const resolvesRef = React.useRef([]);
    React.useEffect(() => {
        if (valueRef.current !== value) {
            for (const resolve of resolvesRef.current) {
                resolve(value);
            }
            resolvesRef.current = [];
            valueRef.current = value;
        }
    }, [value]);
    return () =>
        new Promise<T>(resolve => {
            resolvesRef.current.push(resolve);
        });
}
