import {apiUrl, getTokenFromLocalStorage} from "./config.ts";

export const apiGetAllProjects = () => {
    return {
        method: 'GET',
        url: apiUrl + '/Projects',
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};