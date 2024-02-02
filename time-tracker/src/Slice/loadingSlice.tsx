import { configureStore, createSlice, combineReducers } from '@reduxjs/toolkit';

const loadingSlice = createSlice({
    name: 'loading',
    initialState: false,
    reducers: {
        setLoading(state, action) {
            return action.payload;
        },
    },
});

export const {setLoading} = loadingSlice.actions;
export default loadingSlice.reducer;
