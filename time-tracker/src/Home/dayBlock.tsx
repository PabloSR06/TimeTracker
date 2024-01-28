import React, {useEffect, useState} from 'react';
import styles from "@/Home/dayBlock.module.css";
import {differenceInHours, format, minutesToHours} from "date-fns";
import {number} from "prop-types";
import {minutesInHour} from "date-fns/constants";

export const DayBlock = (props: { day: CustomDay; }) => {

    const {day} = props;

    const [projectCount, setProjectCount] = useState<number>(0);
    const [dayCount, setDayCount] = useState<number>(0);



    function ProjectCount() {
        let count = 0;
        day.projects.forEach(item => {
            count += item.minutes;
        })

        setProjectCount(minutesToHours(count));
    }
    function DayCount() {
        let fromDate: Date = new Date();
        let toDate: Date = new Date();

        day.data.forEach(item => {
            const date = new Date(item.date);
            if (item.type) {
                fromDate = date;
            } else {
                toDate = date;
            }
        });
        setDayCount(differenceInHours(toDate, fromDate));
    }

    useEffect(() => {
        DayCount();
        ProjectCount();
    }, [day]);


    //format(dia, 'EEEE, d MMM yyyy')
    return (
        <div className={styles.dayContainer}>
            <div className={styles.dateContainer}>
                <p>{format(day.date, 'EEEE')}</p>
                <p>{format(day.date, 'd/MM')}</p>
                <p>{format(day.date, '/yyyy')}</p>
            </div>
            <div>
                <p>{day.data.length}</p>
                <p>Other</p>
                <p>{day.projects.length}</p>
            </div>
            <div>
                <p>{dayCount}</p>
                <p>{projectCount}</p>
            </div>
        </div>
    );
};
