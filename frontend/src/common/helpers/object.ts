export function convertObjectPropValues(obj, converter: (o: any) => any) {
    Object.keys(obj).forEach(key => {
        obj[key] = converter(obj[key]);

        if (typeof obj[key] === 'object') {
            convertObjectPropValues(obj[key], converter);
        }
    });
}
