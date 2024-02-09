import {createSlice} from '@reduxjs/toolkit';
import {
    apiGetDayHours,
    ApiGetDayHoursData,
    apiGetProjectHours,
    ApiInsertDayHoursData,
    apiInsertDayHours
} from "../types/config";
import axios from "axios";
import {Dispatch} from "redux";
import {addWeeks, eachDayOfInterval, endOfWeek, startOfDay, startOfWeek, subWeeks} from "date-fns";


const initialState: CustomDay[] = [];

const todayDate = new Date();
const startDateRange = subWeeks(startOfWeek(todayDate, {weekStartsOn: 1}), 2);
const endDateRange = addWeeks(endOfWeek(todayDate, {weekStartsOn: 1}), 2);

const hoursSlice = createSlice({
    name: 'hours',
    initialState,
    reducers: {
        loadTimeData(_state, action) {
            return  action.payload;
        }
    }
});
export const fetchHours = async (dispatch: Dispatch) => {
    const data: ApiGetDayHoursData = {
        userId: 1,
        from: startDateRange,
        to: endDateRange
    };

    const daysRange = eachDayOfInterval({
        start: startDateRange,
        end: endDateRange,
    });

    const fetchDayHours = () => axios.request(apiGetDayHours(data));
    const fetchProjectHours = () => axios.request(apiGetProjectHours(data));

    Promise.all([fetchDayHours(), fetchProjectHours()])
        .then(([dayHoursResponse, projectHoursResponse]) => {
             const dataPerDay = daysRange.map(day => ({
                date: day.toString() as string,

                data: dayHoursResponse.data.filter(item => {
                    const itemDate = new Date(item.date);
                    return startOfDay(itemDate).getTime() === startOfDay(day).getTime();
                }),
                projects: projectHoursResponse.data.filter(item => {
                    const itemDate = new Date(item.date);
                    return startOfDay(itemDate).getTime() === startOfDay(day).getTime();
                })
            }));
            dispatch(loadTimeData(dataPerDay));
        })
        .catch(error => {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        });
};

export const InsertDayHours = async (input: ApiInsertDayHoursData) => {
    try {
        await axios.request(apiInsertDayHours(input));
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error);
        }
    }
};


export const { loadTimeData } = hoursSlice.actions;
export default hoursSlice.reducer;