import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import axios from "axios";
import {Dispatch} from "redux";
import {apiForgotPassword, apiLogInUser, ApiLogInUserData} from "../types/api/auth.ts";

const initialState = {
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

    const response = await axios.request(apiLogInUser(data));
    dispatch(loadToken(response.data.token));

};

export const ForgotPasswordEmail = async (email: string) => {

    await axios.request(apiForgotPassword(email));

};

export const {loadToken} = userSlice.actions;
export default userSlice.reducer;