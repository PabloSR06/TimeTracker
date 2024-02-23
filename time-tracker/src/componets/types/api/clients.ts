import {apiUrl, getTokenFromLocalStorage} from "./config.ts";

export const apiGetAllClients = () => {
    return {
        method: 'GET',
        url: apiUrl + '/Clients',
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};