import {jwtDecode} from "jwt-decode";
import {apiUrl} from "./config.ts";

export const checkTokenValidity = (): boolean => {

    const token = localStorage.getItem('token');
    if (token) {
        if(!checkToken(token)){
            localStorage.removeItem('token');
            return false;
        }else {
            return true;
        }
    }
    return false;
};
export const isTokenValid = (token: string): boolean => {
    if (token) {
        return checkToken(token);
    }
    return false;
};

const checkToken = (token: string): boolean=> {
    try {
        const decodedToken = jwtDecode(token);
        const currentTime = Date.now() / 1000;
        // @ts-ignore
        return currentTime <= decodedToken.exp;
    } catch (error) {
        return false;
    }
}
export type ApiLogInUserData = {
    email: string,
    password: string
};

export const apiLogInUser = (data: ApiLogInUserData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Auth/Login',
        data: {
            email: data.email,
            password: data.password
        }
    };
};
export const apiForgotPassword = (email: string) => {
    return {
        method: 'GET',
        url: apiUrl + '/Auth/password/' + email
    };
};