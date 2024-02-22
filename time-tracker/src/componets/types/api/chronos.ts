import {apiUrl, getTokenFromLocalStorage} from "./config.ts";

export type ApiInsertProjectHoursData = {
    projectId: number,
    clientId: number,
    minutes: string,
    description: string,
    date: Date
};
export type ApiGetDayHoursData = {
    from: Date,
    to: Date
};
export type ApiInsertDayHoursData = {
    type: boolean,
    date: Date
};
export const apiGetDayHours = (data: ApiGetDayHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Chronos/day',
        data: {
            from: data.from,
            to: data.to
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export const apiGetProjectHours = (data: ApiGetDayHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Chronos/project',
        data: {
            from: data.from,
            to: data.to
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export const apiInsertDayHours = (data: ApiInsertDayHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Chronos/day/track',
        data: {
            type: data.type,
            date: data.date
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export const apiInsertProjectHours = (data: ApiInsertProjectHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Chronos/project/track',
        data: {
            projectId: data.projectId,
            minutes: data.minutes,
            date: new Date(data.date),
            description: data.description
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};

