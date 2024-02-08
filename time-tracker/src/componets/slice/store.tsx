import {combineReducers, configureStore} from '@reduxjs/toolkit'
import projectsSlice from "../slice/projectsSlice";
import clientsSlice from "../slice/clientsSlice";
import hoursSlice from "../slice//hoursSlice";

const rootReducer = combineReducers({
    projects: projectsSlice,
    clients: clientsSlice,
    hours: hoursSlice
});

export const store = configureStore({
    reducer: rootReducer,
});

export type RootState = ReturnType<typeof store.getState>


