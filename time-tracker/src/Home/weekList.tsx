import React, { useState, useEffect } from 'react';
import styles from "@/Home/weekList.module.css";
import { startOfWeek, endOfWeek, addDays, format, subWeeks, addWeeks } from 'date-fns';
import {DayBlock} from "@/Home/dayBlock";

export const WeekList = () => {
    const [currentWeek, setCurrentWeek] = useState<Date[]>([]);
    const [weeksOffset, setWeeksOffset] = useState(0); // Número de semanas antes o después de la actual

    const updateWeek = (startOfWeekDate: Date) => {
        const daysInWeek = [];
        let currentDay = startOfWeekDate;

        while (currentDay <= endOfWeek(startOfWeekDate, { weekStartsOn: 1 })) {
            daysInWeek.push(currentDay);
            currentDay = addDays(currentDay, 1);
        }

        setCurrentWeek(daysInWeek);
        console.log(daysInWeek);
    };

    useEffect(() => {
        const today = new Date();
        const startOfCurrentWeek = startOfWeek(today, { weekStartsOn: 1 });
        const startOfWeekWithOffset = addWeeks(startOfCurrentWeek, weeksOffset);
        updateWeek(startOfWeekWithOffset);
    }, [weeksOffset]);

    const goToPreviousWeek = () => {
        if (weeksOffset > -5) {
            setWeeksOffset((prevOffset) => prevOffset - 1);
        }
    };

    const goToNextWeek = () => {
        if (weeksOffset < 5) {
            setWeeksOffset((prevOffset) => prevOffset + 1);
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

            {currentWeek.map((day, index) => (

                <DayBlock day={day}/>
            ))}

        </div>
    );
};
