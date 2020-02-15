export function filterDict<T, K = { [index: string]: T }>(
    dict: K,
    filterFn: (value: string, index: number, array: string[]) => unknown,
) {
    return Object.keys(dict)
        .filter(filterFn)
        .reduce((obj, key) => {
            obj[key] = dict[key];
            return obj;
        }, {});
}
