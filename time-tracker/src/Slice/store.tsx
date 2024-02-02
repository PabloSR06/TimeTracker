import { configureStore } from '@reduxjs/toolkit'
import projectsSlice from "@/Slice/projectsSlice";
import clientsSlice from "@/Slice/clientsSlice";


export const store = configureStore({
    reducer: {
        projects: projectsSlice,
        clients: clientsSlice
    }
});

export type RootState = ReturnType<typeof store.getState>