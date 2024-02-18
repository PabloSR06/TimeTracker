import {useState, useEffect} from 'react';
import {startOfWeek, endOfWeek} from 'date-fns';
import {DayBlock} from "../home/dayBlock.tsx";
import { useSelector} from "react-redux";
import {RootState} from "../slice/store.tsx";
import {WeekControl} from "./weekControl.tsx";

export const WeekList = () => {
    const todayDate = new Date();


    const [currentDate, setCurrentDate] = useState<Date>(todayDate);
    const [weekToShow, setWeekToShow] = useState<CustomDay[]>([]);


    const allData = useSelector((state: RootState) => state.hours);


    useEffect(() => {
        const filterData = () => {
            const startOfCurrentWeek = startOfWeek(currentDate, {weekStartsOn: 1});
            const endOfCurrentWeek = endOfWeek(currentDate, {weekStartsOn: 1});

            const selectedWeek = allData.filter((day) => {
                return startOfCurrentWeek <= new Date(day.date) && new Date(day.date) <= endOfCurrentWeek;
            });

            setWeekToShow(selectedWeek);
        }
        filterData();
    }, [currentDate, allData]);


    return (
        <div>
            <WeekControl date={currentDate} setDate={setCurrentDate}/>

            {weekToShow.map((day, index) => (
                <DayBlock key={index} day={day} index={index} />
            ))}
        </div>
    );
};
