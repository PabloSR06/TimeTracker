import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {apiGetAllClients} from "../types/config";
import axios from "axios";
import {Dispatch} from "redux";

const initialState: ClientModel[] = [];

const clientsSlice = createSlice({
    name: 'clients',
    initialState,
    reducers: {
        loadClients(_state, action: PayloadAction<[]>) {
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