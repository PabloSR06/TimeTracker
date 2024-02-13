import {useState, useEffect} from 'react';
import styles from "./weekList.module.css";
import {addWeeks, subWeeks, startOfWeek, endOfWeek} from 'date-fns';
import {DayBlock} from "../home/dayBlock.tsx";
import { useSelector} from "react-redux";
import {RootState} from "../slice/store.tsx";

export const WeekList = () => {
    const todayDate = new Date();
    const weekAmount = 2;
    const startDateRange = subWeeks(startOfWeek(todayDate, {weekStartsOn: 1}), weekAmount);
    const endDateRange = addWeeks(endOfWeek(todayDate, {weekStartsOn: 1}), weekAmount);


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


    const goToPreviousWeek = () => {
        const newDate = subWeeks(currentDate, 1);
        if (newDate >= startDateRange) {
            setCurrentDate(newDate);
        }
    };
    const goToNextWeek = () => {
        const newDate = addWeeks(currentDate, 1);
        if (newDate <= endDateRange) {
            setCurrentDate(addWeeks(currentDate, 1));
        }
    };

    return (
        <div>
            <div className={styles.weekControl}>
                <button className={styles.weekButton} onClick={goToPreviousWeek}>Semana Anterior</button>
                <p>Week Control</p>
                <button className={styles.weekButton} onClick={goToNextWeek}>Semana Siguiente</button>

            </div>

            {weekToShow.map((day, index) => (
                <DayBlock key={index} day={day} index={index} />
            ))}
        </div>
    );
};
