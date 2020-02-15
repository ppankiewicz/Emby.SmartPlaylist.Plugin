export function normalizeArray<T>(array: T[], indexKey: keyof T) {
    const normalizedObject: any = {};
    for (let i = 0; i < array.length; i++) {
        const key = array[i][indexKey];
        normalizedObject[key] = array[i];
    }
    return normalizedObject as { [key: string]: T };
}

export function addItemAt<T>(array: T[], item: T, index: number) {
    return [...array.slice(0, index), item, ...array.slice(index)];
}

export function selectMany<TIn, TOut>(input: TIn[], selectListFn: (t: TIn) => TOut[]): TOut[] {
  return input.reduce((out, inx) => {
    out.push(...selectListFn(inx));
    return out;
  }, new Array<TOut>());
}