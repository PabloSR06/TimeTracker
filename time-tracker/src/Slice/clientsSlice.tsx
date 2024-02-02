import {AnyAction, createSlice, PayloadAction, UnknownAction} from '@reduxjs/toolkit';
import {apiUrl} from "@/Types/config";
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
    const config = {
        method: 'GET',
        url: apiUrl + '/Project/GetAllProjects'
    };
    try {
        const response = await axios.request(config);
        dispatch(loadClients(response.data));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const {loadClients} = clientsSlice.actions;
export default clientsSlice.reducer;