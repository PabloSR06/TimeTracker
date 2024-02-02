import {createSlice, PayloadAction, UnknownAction} from '@reduxjs/toolkit';
import {apiGetAllClients, apiGetAllProjects, apiUrl} from "@/Types/config";
import axios from "axios";
import {Dispatch} from "redux";

const clientsSlice = createSlice({
    name: 'clients',
    initialState: [] as ClientModel[],
    reducers: {
        loadClients(state, action: PayloadAction<[]>) {
            return action.payload;
        }
    }
});

export const fetchClients = async (dispatch: Dispatch) => {
    try {
        const response = await axios.request(apiGetAllClients());
        dispatch(loadClients(response.data));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const {loadClients} = clientsSlice.actions;
export default clientsSlice.reducer;