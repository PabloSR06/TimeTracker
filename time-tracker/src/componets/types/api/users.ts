import {apiUrl} from "./config.ts";

export type ApiCreateUserData = {
    name: string,
    email: string,
    password: string
};
export type ResetPasswordData = {
    password: string,
    token: string
};
export const apiCreateUser = (data: ApiCreateUserData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Users',
        data: {
            name: data.name,
            email: data.email,
            password: data.password
        }
    };
};
export const apiResetPassword = (data: ResetPasswordData) => {
    return {
        method: 'PUT',
        url: apiUrl + '/Users/password/token',
        data: {
            password: data.password,
        },
        headers: {
            Authorization: `Bearer ${data.token}`
        }
    };
};