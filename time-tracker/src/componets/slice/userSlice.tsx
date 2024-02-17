import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {apiForgotPassword, apiLogInUser, ApiLogInUserData} from "../types/config";
import axios from "axios";
import {Dispatch} from "redux";

const initialState= {
    userToken: '',
    isTokenValid: false
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        loadToken(state, action: PayloadAction<[]>) {
            state.userToken = action.payload.toString();
            state.isTokenValid = true;
            localStorage.setItem('token', action.payload.toString());
        }
    }
});

export const logIn = async (dispatch: Dispatch, data: ApiLogInUserData) => {
    try {
        const response = await axios.request(apiLogInUser(data));
        dispatch(loadToken(response.data.token));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};

export const ForgotPasswordEmail = async (dispatch: Dispatch, email: string) => {

    await axios.request(apiForgotPassword(email));

};

export const {loadToken} = userSlice.actions;
export default userSlice.reducer;