import React, {useEffect, useState} from 'react';
import styles from "@/Home/dayBlock.module.css";
import {addWeeks, differenceInHours, format, minutesToHours, differenceInSeconds} from "date-fns";
import {DayInfo} from "@/Home/dayInfo";
import {useDispatch} from "react-redux";
import {InsertDayHours} from "@/Slice/hoursSlice";
import {ApiInsertDayHoursData} from "@/Types/config";

interface DayBlockProps {
    day: CustomDay;
    reloadComponent: () => void;
}

export const DayBlock: React.FC<DayBlockProps> = ({ reloadComponent, day }) => {

    const dispatch = useDispatch();

    const [projectCount, setProjectCount] = useState<number>(0);
    const [dayCount, setDayCount] = useState<number>(0);

    const [dayStarted, setDayStarted] = useState<boolean>(false);
    const [dayFinished, setDayFinished] = useState<boolean>(false);


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
            } else {
                toDate = date;
                setDayFinished(true);
            }

        });
        setDayCount(differenceInHours(toDate, fromDate));
    }
    const combineDate = (date: Date) => {
        let dayOfMonth = day.date.getDate();
        let month = day.date.getMonth();
        let year = day.date.getFullYear();


        let currentDate = new Date();
        let hours = currentDate.getHours();
        let minutes = currentDate.getMinutes();
        let seconds = currentDate.getSeconds();

        return new Date(year, month, dayOfMonth, hours, minutes, seconds);
    }

    const startDay = async () => {


        const data: ApiInsertDayHoursData = {
            userId: 1,
            date: combineDate(day.date),
            type: true
        };
        InsertDayHours(dispatch, data).then(() => reloadComponent());
    }
    const endDay = async () => {
        const data: ApiInsertDayHoursData = {
            userId: 1,
            date: combineDate(day.date),
            type: false
        };
        InsertDayHours(dispatch, data).then(() => reloadComponent());
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
                <div>
                    <p>Day</p>
                    <p>{dayCount}</p>
                </div>
                <div>
                    <p>Projects</p>
                    <p>{projectCount}</p>
                </div>
            </div>
            <div>

                {!dayStarted ? (<button onClick={startDay}>Open</button>) : null}
                {dayStarted && !dayFinished ? (<button onClick={endDay}>Close</button>) : null}


            </div>
            <DayInfo day={day}/>
        </div>

    );
};
