import {createSlice, PayloadAction, UnknownAction} from '@reduxjs/toolkit';
import {apiUrl} from "@/Types/config";
import axios from "axios";
import {Dispatch} from "redux";

const clientsSlice = createSlice({
    name: 'clients',
    initialState: [],
    reducers: {
        clientsLoaded(state, action: PayloadAction<any>) {
            console.log(action);
            return action.payload; // Replace the state with the todos fetched from API
        }
    }
});

export const fetchClients = async (dispatch: Dispatch) => {
    const config = {
        method: 'GET',
        url: apiUrl + '/Project/GetAllProjects'
    };
    try {
        const response = await axios.request(config);
        console.log("s");
        dispatch(clientsLoaded(response.data)); // Dispatch action to update the state with fetched todos
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const {clientsLoaded} = clientsSlice.actions;
export default clientsSlice.reducer;