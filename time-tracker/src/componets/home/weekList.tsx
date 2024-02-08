import {useState, useEffect} from 'react';
import styles from "./weekList.module.css";
import {addWeeks, subWeeks, startOfDay, endOfDay, eachDayOfInterval, startOfWeek, endOfWeek} from 'date-fns';
import {DayBlock} from "./dayBlock.tsx";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../slice/store";
import {fetchHours} from "../slice/hoursSlice";
import {ProjectInput} from "./projectInput.tsx";

export const WeekList = () => {
    const todayDate = new Date();
    const startDateRange = subWeeks(startOfDay(todayDate), 2);
    const endDateRange = endOfDay(addWeeks(todayDate, 2));

    const [counter, setCounter] = useState(0);
    const [allData, setAllData] = useState<CustomDay[]>([]);

    const [currentDate, setCurrentDate] = useState<Date>(todayDate);

    const dispatch = useDispatch();

    const apiData = useSelector((state: RootState) => state.hours.ApiData);
    const projectHours = useSelector((state: RootState) => state.hours.ProjectHours);

    const reloadComponent = () => {
        setCounter(counter + 1);
    };

    useEffect(() => {
        const filterData = () => {
            const startOfCurrentWeek = startOfWeek(currentDate, {weekStartsOn: 1});
            const endOfCurrentWeek = endOfWeek(currentDate, {weekStartsOn: 1});

            const daysRange = eachDayOfInterval({
                start: startOfCurrentWeek,
                end: endOfCurrentWeek,
            });

            const dataPerDay = daysRange.map(day => ({
                date: day,
                data: apiData.filter(item => {
                    const itemDate = new Date(item.date);
                    return startOfDay(itemDate).getTime() === startOfDay(day).getTime();
                }),
                projects: projectHours.filter(item => {
                    const itemDate = new Date(item.date);
                    return startOfDay(itemDate).getTime() === startOfDay(day).getTime();
                })
            }));
            setAllData(dataPerDay);
        }
        filterData();
    }, [apiData, currentDate, projectHours]);

    useEffect(() => {
        fetchHours(dispatch, startDateRange, endDateRange);
    }, [counter]);

    const goToPreviousWeek = () => {
        if (currentDate >= startDateRange) {
            setCurrentDate(subWeeks(currentDate, 1));
        }
    };
    const goToNextWeek = () => {
        if (currentDate <= endDateRange) {
            setCurrentDate(addWeeks(currentDate, 1));
        }
    };

    return (
        <div>
            <h2>Semana Actual</h2>
            <div className={styles.weekControl}>
                <button className={styles.weekButton} onClick={goToPreviousWeek}>Semana Anterior</button>
                <p>Week Control</p>
                <button className={styles.weekButton} onClick={goToNextWeek}>Semana Siguiente</button>

            </div>

            {allData.map((day, index) => (
                <DayBlock key={index} day={day} reloadComponent={reloadComponent}/>
            ))}

            <ProjectInput />

        </div>
    );
};
