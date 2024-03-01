import React, {useEffect, useState} from 'react';
import styles from "./dayBlock.module.css";
import {differenceInHours, format, minutesToHours} from "date-fns";
import {fetchHours, InsertDayHours} from "../slice/hoursSlice";
import {CalendarCheck, CalendarX} from "react-bootstrap-icons";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {combineDate, getTime} from "../tools.ts";
import {ColorBar} from "./colorBar.tsx";
import {ApiInsertDayHoursData} from "../types/api/chronos.ts";

interface DayBlockProps {
    day: CustomDay;
    index: number;

}

export const DayBlock: React.FC<DayBlockProps> = ({day}) => {
    const dispatch = useDispatch();
    const navigate = useNavigate();


    const [projectCount, setProjectCount] = useState<number>(0);
    const [dayCount, setDayCount] = useState<number>(0);

    const [dayStarted, setDayStarted] = useState<boolean>(false);
    const [dayFinished, setDayFinished] = useState<boolean>(false);

    const [fromDate, setFromDate] = useState<Date>();
    const [toDate, setToDate] = useState<Date>();

    const [state, setState] = useState<number>(0);


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
        setState(0);

        day.data.forEach(item => {
            const date = new Date(item.date);
            if (item.type) {
                fromDate = date;

                setDayStarted(true);
                setFromDate(date);
                setState(1);
            } else {
                toDate = date;
                setDayFinished(true);
                setToDate(date);
                setState(2);
            }
        });
        setDayCount(differenceInHours(toDate, fromDate));
    }


    const startDay = async () => {

        const data: ApiInsertDayHoursData = {
            date: combineDate(day.date),
            type: true
        };
        InsertDayHours(data).then(() => fetchHours(dispatch));
    }
    const endDay = async () => {
        const data: ApiInsertDayHoursData = {
            date: combineDate(day.date),
            type: false
        };
        InsertDayHours(data).then(() => fetchHours(dispatch));
    }

    useEffect(() => {
        DayCount();
        ProjectCount();
    }, [day]);

    const goToDay = async () => {
        navigate(`/day`, {state: {id: day.id}});
    }

    return (
        <div className={`blockContainer ${styles.dayContainer}`} onClick={goToDay}>
                <div className={styles.leftContainer}>
                    <ColorBar state={state}/>
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
            <div className={styles.buttonContainer}>
                <div className={styles.hourContainer}>
                    {!dayStarted ? (<button className={styles.startButton} onClick={(e) => {
                                e.stopPropagation();
                                startDay();
                            }}>Open</button>
                        ) :
                        <p><span><CalendarCheck className={styles.calendarIcon}/></span>{getTime(fromDate)}</p>}
                    {dayStarted && !dayFinished ? (
                            <button className={styles.startButton} onClick={(e) => {
                                e.stopPropagation();
                                endDay();
                            }}>Close</button>) :
                        dayFinished ? <p><span><CalendarX
                            className={styles.calendarIcon}/></span>{getTime(toDate)}
                        </p> : null}
                </div>
            </div>
        </div>


    );
};
