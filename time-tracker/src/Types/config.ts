import {number} from "prop-types";

export const apiUrl = 'https://localhost:7225';

export const apiGetDayHours = (data: ApiGetDayHoursData) => {
    return{
        method: 'POST',
        url: apiUrl + '/Time/GetDayHours',
        data: {
            userId: data.userId,
            from: data.from,
            to: data.to
        }
    };
};
export const apiGetProjectHours = (data: ApiGetDayHoursData) => {
    return{
        method: 'POST',
        url: apiUrl + '/Time/GetProjectHours',
        data: {
            userId: data.userId,
            from: data.from,
            to: data.to
        }
    };
};
export type ApiGetDayHoursData = {
    userId: number,
    from: Date,
    to: Date
};

export const apiInsertDayHours = (data: ApiInsertDayHoursData) => {
    return{
        method: 'POST',
        url: apiUrl + '/Time/InsertDayHours',
        data: {
            userId: data.userId,
            type: data.type
        }
    };
};
export type ApiInsertDayHoursData = {
    userId: number,
    type: boolean
};

export const apiGetAllProjects = () => {
    return{
        method: 'GET',
        url: apiUrl + '/Project/GetAllProjects'
    };
};
export const apiGetAllClients = () => {
    return{
        method: 'GET',
        url: apiUrl + '/Client/GetAllClients'
    };
};


export const apiInsertProjectHours = (data: ApiInsertProjectHoursData) => {
    return{
        method: 'POST',
        url: apiUrl + '/Time/InsertProjectHours',
        data: {
            userId: data.userId,
            projectId: data.projectId,
            minutes: data.minutes,
            date: data.date
        }
    };
};
export type ApiInsertProjectHoursData = {
    userId: number,
    projectId: number,
    minutes: number,
    date: Date
};
