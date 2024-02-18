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
export const combineDate = (date: Date) => {
    date = new Date(date);
    const dayOfMonth = date.getDate();
    const month = date.getMonth();
    const year = date.getFullYear();

    const currentDate = new Date();
    const hours = currentDate.getHours();
    const minutes = currentDate.getMinutes();
    const seconds = currentDate.getSeconds();

    return new Date(year, month, dayOfMonth, hours, minutes, seconds);
}