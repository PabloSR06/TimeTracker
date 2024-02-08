import React, {useEffect, useState} from 'react';
import styles from "./dayBlock.module.css";
import {differenceInHours, format, minutesToHours} from "date-fns";
import {DayInfo} from "./dayInfo";
import {InsertDayHours} from "../slice/hoursSlice";
import {ApiInsertDayHoursData} from "../types/config";
import {CalendarCheck, CalendarX} from "react-bootstrap-icons";

interface DayBlockProps {
    day: CustomDay;
    reloadComponent: () => void;
}

export const DayBlock: React.FC<DayBlockProps> = ({reloadComponent, day}) => {

    const [projectCount, setProjectCount] = useState<number>(0);
    const [dayCount, setDayCount] = useState<number>(0);

    const [dayStarted, setDayStarted] = useState<boolean>(false);
    const [dayFinished, setDayFinished] = useState<boolean>(false);

    const [fromDate, setFromDate] = useState<Date>();
    const [toDate, setToDate] = useState<Date>();


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

        setDayStarted(false);
        setDayFinished(false);

        day.data.forEach(item => {
            const date = new Date(item.date);
            if (item.type) {
                fromDate = date;
                toDate = combineDate(date)
                setDayStarted(true);
                setFromDate(date);
            } else {
                toDate = date;
                setDayFinished(true);
                setToDate(date);
            }

        });
        setDayCount(differenceInHours(toDate, fromDate));
    }

    const combineDate = (date: Date) => {
        const dayOfMonth = date.getDate();
        const month = date.getMonth();
        const year = date.getFullYear();

        const currentDate = new Date();
        const hours = currentDate.getHours();
        const minutes = currentDate.getMinutes();
        const seconds = currentDate.getSeconds();

        return new Date(year, month, dayOfMonth, hours, minutes, seconds);
    }

    const startDay = async () => {


        const data: ApiInsertDayHoursData = {
            userId: 1,
            date: combineDate(day.date),
            type: true
        };
        InsertDayHours(data).then(() => reloadComponent());
    }
    const endDay = async () => {
        const data: ApiInsertDayHoursData = {
            userId: 1,
            date: combineDate(day.date),
            type: false
        };
        InsertDayHours(data).then(() => reloadComponent());
    }

    useEffect(() => {
        DayCount();
        ProjectCount();
    }, [day]);

    const test = async () => {
        console.log('click');
        return <DayInfo day={day}/>
    }

    return (
        <div className={styles.dayContainer} onClick={test}>
            <div className={styles.dateContainer}>
                <div className={styles.monthContainer}>
                    <p>{format(day.date, 'd')}</p>
                    <p>/</p>
                    <p>{format(day.date, 'MM')}</p>
                </div>
                <div className={styles.yearContainer}>
                    <p>{format(day.date, 'yyyy')}</p>
                </div>
            </div>
            <div className={styles.countContainer}>
                <div className={styles.dayCountContainer}>
                    <p>Work day</p>
                    <p>{dayCount}</p>
                </div>
                <div className={styles.projectCountContainer}>
                    <p>Projects</p>
                    <p>{projectCount}</p>
                </div>
            </div>
            <div className={styles.buttomContainer}>
                <div className={styles.hourContainer}>


                    {!dayStarted ? (<button className={styles.startButton} onClick={startDay}>Open</button>) :
                        <p><span><CalendarCheck className={styles.calendarIcon}/></span>{fromDate?.getHours()}:{fromDate?.getMinutes()}</p>}
                    {dayStarted && !dayFinished ? (<button className={styles.startButton} onClick={endDay}>Close</button>) :
                        dayFinished ? <p><span><CalendarX className={styles.calendarIcon}/></span>{toDate?.getHours()}:{toDate?.getMinutes()}</p> : null}
                </div>
            </div>
        </div>


    );
};
