import {createSlice} from '@reduxjs/toolkit';
import {apiGetAllProjects} from "../types/config";
import axios from "axios";
import {Dispatch} from "redux";

const initialState: ProjectModel[] = [];

const projectsSlice = createSlice({
    name: 'projects',
    initialState,
    reducers: {
        projectsLoaded(_state,  action) {
            return action.payload; // Replace the state with the todos fetched from API
        }
    }
});
export const fetchProjects = async (dispatch: Dispatch) => {
    try {
        const response = await axios.request(apiGetAllProjects());
        dispatch(projectsLoaded(response.data));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const { projectsLoaded } = projectsSlice.actions;
export default projectsSlice.reducer;