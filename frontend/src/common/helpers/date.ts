export function isValidDate(d: any) {
    return d instanceof Date && !isNaN(d as any);
}

export const parseOrDefault = (value: any) => {
    const date = parseDate(value);
    return isValidDate(date) ? date : new Date();
};

export const parseDate = (value: any): Date => {
    if (isValidDate(value)) {
        return new Date(value);
    }

    return parseIsoDate(value);
};

export function parseIsoDate(value) {
    const reISO = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*))(?:Z|(\+|-)([\d|:]*))?$/;
    if (typeof value === 'string') {
        const a = reISO.exec(value);
        if (a) {
            return new Date(value);
        }
    }
    return value;
}
