import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import { apiUrl } from "@/Types/config";
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
    // TODO: MAYBE CHANGE CONFIG FILE
    const config = {
        method: 'post',
        url: '',
        data:
            {
                "userId": 1,
                "from": startDateRange,
                "to": endDateRange
            }
    };
    const fetchDayHours = async () => {
        try {
            config.url = apiUrl + '/Time/GetDayHours';
            const response = await axios.request(config);
            dispatch(loadApiData(response.data));
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };
    const fetchProjectHours = async () => {
        try {
            config.url = apiUrl + '/Time/GetProjectHours';
            const response = await axios.request(config);
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
    const config = {
        method: 'post',
        url: apiUrl + '/Time/InsertDayHours',
        data:
            {
                "userId": userId,
                "type": true
            }
    };

    try {
        await axios.request(config);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};

export const EndDayHours = async (dispatch: Dispatch, userId:number) => {
    const config = {
        method: 'post',
        url: apiUrl + '/Time/InsertDayHours',
        data:
            {
                "userId": userId,
                "type": false
            }
    };

    try {
        await axios.request(config);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};
export const { loadApiData, loadProjectHours } = hoursSlice.actions;
export default hoursSlice.reducer;