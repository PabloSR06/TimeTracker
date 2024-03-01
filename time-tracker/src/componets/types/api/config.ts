export const apiUrl = 'https://localhost:5001';

// @ts-ignore
export const getTokenFromLocalStorage = (): string => localStorage.getItem('token');

export const deleteTokenFromLocalStorage = (): void => localStorage.removeItem('token');












