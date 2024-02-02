import {createSlice, PayloadAction, UnknownAction} from '@reduxjs/toolkit';
import {apiGetAllProjects, apiUrl} from "@/Types/config";
import axios from "axios";
import {Dispatch} from "redux";

const clientsSlice = createSlice({
    name: 'clients',
    initialState: [],
    reducers: {
        loadClients(state, action: PayloadAction<[]>) {
            return action.payload;
        }
    }
});

export const fetchClients = async (dispatch: Dispatch) => {
    //TODO: CHANGE FOR CLIENTS
    try {
        const response = await axios.request(apiGetAllProjects());
        dispatch(loadClients(response.data));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const {loadClients} = clientsSlice.actions;
export default clientsSlice.reducer;