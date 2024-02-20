export const apiUrl = 'https://localhost:7225';

// @ts-ignore
export const getTokenFromLocalStorage = (): string => localStorage.getItem('token')


export const apiGetDayHours = (data: ApiGetDayHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Time/GetDayHours',
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
        url: apiUrl + '/Time/GetProjectHours',
        data: {
            from: data.from,
            to: data.to
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export type ApiGetDayHoursData = {
    from: Date,
    to: Date
};

export const apiInsertDayHours = (data: ApiInsertDayHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Time/InsertDayHours',
        data: {
            type: data.type,
            date: data.date
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export type ApiInsertDayHoursData = {
    type: boolean,
    date: Date
};





export const apiInsertProjectHours = (data: ApiInsertProjectHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Time/InsertProjectHours',
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
export type ApiInsertProjectHoursData = {
    projectId: number,
    clientId: number,
    minutes: string,
    description: string,
    date: Date
};










