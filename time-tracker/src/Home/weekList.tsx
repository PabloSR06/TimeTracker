import React, {useState, useEffect} from 'react';
import styles from "@/Home/weekList.module.css";
import {addWeeks, subWeeks, startOfDay, endOfDay, eachDayOfInterval, startOfWeek, endOfWeek} from 'date-fns';
import {DayBlock} from "@/Home/dayBlock";
import axios from "axios";
import {apiUrl} from "@/Types/config";

export const WeekList = () => {
    const todayDate = new Date();
    const startDateRange = subWeeks(startOfDay(todayDate), 2);
    const endDateRange = endOfDay(addWeeks(todayDate, 2));

    const [counter, setCounter] = useState(0);


    const [apiData, setApiData] = useState<DayHours[]>([]);
    const [projectHours, setProjectHours] = useState<ProjectHoursName[]>([]);
    const [allData, setAllData] = useState<CustomDay[]>([]);

    const [currentDate, setCurrentDate] = useState<Date>(todayDate);

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
                setApiData(response.data);
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
                setProjectHours(response.data);
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.log(error);
                }
            }
        };

        fetchDayHours();
        fetchProjectHours();
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

        </div>
    );
};
