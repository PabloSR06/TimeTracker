import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import { apiUrl } from "@/Types/config";
import axios from "axios";
import {Dispatch} from "redux";

const projectsSlice = createSlice({
    name: 'projects',
    initialState: [],
    reducers: {
        projectsLoaded(state, action) {
            return action.payload; // Replace the state with the todos fetched from API
        }
    }
});
export const fetchProjects = async (dispatch: Dispatch) => {
    const config = {
        method: 'GET',
        url: apiUrl + '/Project/GetAllProjects'
    };
    try {
        const response = await axios.request(config);
        dispatch(projectsLoaded(response.data)); // Dispatch action to update the state with fetched todos
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const { projectsLoaded } = projectsSlice.actions;
export default projectsSlice.reducer;