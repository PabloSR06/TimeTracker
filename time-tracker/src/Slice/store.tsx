import {combineReducers, configureStore} from '@reduxjs/toolkit'
import projectsSlice from "@/Slice/projectsSlice";
import clientsSlice from "@/Slice/clientsSlice";
import loadingSlice from "@/Slice/loadingSlice";
import hoursSlice from "@/Slice/hoursSlice";


const rootReducer = combineReducers({
    projects: projectsSlice,
    clients: clientsSlice,
    hours: hoursSlice,
    //loading: loadingSlice,
});

export const store = configureStore({
    reducer: rootReducer,
});

//export type RootState = ReturnType<typeof rootReducer>;

export type RootState = ReturnType<typeof store.getState>


