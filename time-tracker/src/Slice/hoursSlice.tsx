import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {
    apiGetDayHours,
    ApiGetDayHoursData,
    apiGetProjectHours,
    ApiInsertDayHoursData,
    apiInsertDayHours,
    apiUrl
} from "@/Types/config";
import axios from "axios";
import {Dispatch} from "redux";

interface HoursState {
    ApiData: DayHours[];
    ProjectHours: ProjectHoursName[];
}

const initialState: HoursState = {
    ApiData: [],
    ProjectHours: []
};

const hoursSlice = createSlice({
    name: 'hours',
    initialState,
    reducers: {
        loadApiData(state, action) {
            state.ApiData = action.payload;
        },
        loadProjectHours(state, action) {
            state.ProjectHours = action.payload;
        }
    }
});
export const fetchHours = async (dispatch: Dispatch, startDateRange:Date, endDateRange:Date) => {
    const data: ApiGetDayHoursData = {
        userId: 1,
        from: startDateRange,
        to: endDateRange
    };

    const fetchDayHours = async () => {
        try {
            const response = await axios.request(apiGetDayHours(data));
            dispatch(loadApiData(response.data));
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };
    const fetchProjectHours = async () => {
        try {
            const response = await axios.request(apiGetProjectHours(data));
            dispatch(loadProjectHours(response.data));
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };
    fetchDayHours();
    fetchProjectHours();
};

export const StartDayHours = async (dispatch: Dispatch, userId:number) => {

    const data: ApiInsertDayHoursData = {
        userId: userId,
        type: true
    };

    try {
        await axios.request(apiInsertDayHours(data));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};

export const EndDayHours = async (dispatch: Dispatch, userId:number) => {
    const data: ApiInsertDayHoursData = {
        userId: userId,
        type: false
    };

    try {
        await axios.request(apiInsertDayHours(data));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const { loadApiData, loadProjectHours } = hoursSlice.actions;
export default hoursSlice.reducer;