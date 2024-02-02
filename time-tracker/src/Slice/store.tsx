import {combineReducers, configureStore} from '@reduxjs/toolkit'
import projectsSlice from "@/Slice/projectsSlice";
import clientsSlice from "@/Slice/clientsSlice";
import hoursSlice from "@/Slice/hoursSlice";

const rootReducer = combineReducers({
    projects: projectsSlice,
    clients: clientsSlice,
    hours: hoursSlice
});

export const store = configureStore({
    reducer: rootReducer,
});

export type RootState = ReturnType<typeof store.getState>


