export const apiUrl = 'https://localhost:7225';

// @ts-ignore
export const getTokenFromLocalStorage = ():string => localStorage.getItem('token')
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
    userId: number,
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
    userId: number,
    type: boolean,
    date: Date
};

export const apiGetAllProjects = () => {
    return {
        method: 'GET',
        url: apiUrl + '/Project/GetAllProjects',
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export const apiGetAllClients = () => {
    return {
        method: 'GET',
        url: apiUrl + '/Client/GetAllClients',
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};


export const apiInsertProjectHours = (data: ApiInsertProjectHoursData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Time/InsertProjectHours',
        data: {
            projectId: data.projectId,
            minutes: data.minutes,
            date: data.date
        },
        headers: {
            Authorization: `Bearer ${getTokenFromLocalStorage()}`
        }
    };
};
export type ApiInsertProjectHoursData = {
    userId: number,
    projectId: number,
    minutes: number,
    date: Date
};

export const apiInsertUser = (data: ApiInsertUserData) => {
    return {
        method: 'POST',
        url: apiUrl + '/Auth/Register',
        data: {
            email: data.email,
            password: data.password
        }
    };
};
export type ApiInsertUserData = {
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
export type ApiLogInUserData = {
    email: string,
    password: string
};


