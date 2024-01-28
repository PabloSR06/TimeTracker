import React, {useState, useEffect} from 'react';
import styles from "@/Home/weekList.module.css";
import {addWeeks, subWeeks, startOfDay, endOfDay, eachDayOfInterval, startOfWeek, endOfWeek} from 'date-fns';
import {DayBlock} from "@/Home/dayBlock";
import axios from "axios";

export const WeekList = () => {
    const todayDate = new Date();
    const startDateRange = subWeeks(startOfDay(todayDate), 2);
    const endDateRange = endOfDay(addWeeks(todayDate, 2));

    const [apiData, setApiData] = useState<DayHours[]>([]);
    const [projectHours, setProjectHours] = useState<ProjectHours[]>([]);
    const [allData, setAllData] = useState<CustomDay[]>([]);

    const [currentDate, setCurrentDate] = useState<Date>(todayDate);


    useEffect(() => {
        const filterData = () => {
            const startOfCurrentWeek = startOfWeek(currentDate, {weekStartsOn: 1});
            const endOfCurrentWeek = endOfWeek(currentDate, {weekStartsOn: 1});

            const diasIntervalo = eachDayOfInterval({
                start: startOfCurrentWeek,
                end: endOfCurrentWeek,
            });

            const datosPorDia = diasIntervalo.map(dia => ({
                date: dia,
                data: apiData.filter(item => {
                    const itemDate = new Date(item.date);
                    return startOfDay(itemDate).getTime() === startOfDay(dia).getTime();
                }),
                projects: projectHours.filter(item => {
                    const itemDate = new Date(item.date);
                    return startOfDay(itemDate).getTime() === startOfDay(dia).getTime();
                })
            }));
            setAllData(datosPorDia);
        }
        filterData();
    }, [apiData, currentDate, projectHours]);

    useEffect(() => {
        const fetchData = async () => {
            // TODO: CONFIG FILE
            const config = {
                method: 'post',
                url: 'https://localhost:7225/Time/GetDayHours',
                data:
                    {
                        "userId": 0,
                        "from": startDateRange,
                        "to": endDateRange
                    }
            };
            try {
                const response = await axios.request(config);
                setApiData(response.data);
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.log(error);
                }
            }
        };
        const fetchData2 = async () => {
            // TODO: CONFIG FILE
            const config = {
                method: 'post',
                url: 'https://localhost:7225/Time/GetProjectHours',
                data:
                    {
                        "userId": 0,
                        "from": startDateRange,
                        "to": endDateRange
                    }
            };
            try {
                const response = await axios.request(config);
                setProjectHours(response.data);
            } catch (error) {
                if (axios.isAxiosError(error)) {
                    console.log(error);
                }
            }
        };

        fetchData();
        fetchData2();
    }, []);

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
                <DayBlock key={index} day={day}/>
            ))}

        </div>
    );
};
