export const apiUrl = 'https://localhost:7225';

// @ts-ignore
export const getTokenFromLocalStorage = (): string => localStorage.getItem('token');

export const deleteTokenFromLocalStorage = (): void => localStorage.removeItem('token');












