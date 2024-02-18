export const getTime = (date: Date | undefined) => {
    if (!date) return '';

    let result = '';
    const hours = date.getHours();
    const minutes = date.getMinutes();

    result += hours < 10 ? '0' + hours : hours;
    result += ':';
    result += minutes < 10 ? '0' + minutes : minutes;

    return result;
}