import React, {useEffect, useState} from 'react';
import styles from "@/Home/dayBlock.module.css";
import {addWeeks, differenceInHours, format, minutesToHours} from "date-fns";
import {DayInfo} from "@/Home/dayInfo";
import {useDispatch} from "react-redux";
import {EndDayHours, StartDayHours} from "@/Slice/hoursSlice";

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

        day.data.forEach(item => {
            const date = new Date(item.date);
            if (item.type) {
                fromDate = date;
                setDayStarted(true);
            } else {
                toDate = date;
                setDayFinished(true);
            }
        });
        setDayCount(differenceInHours(toDate, fromDate));
    }

    const startDay = async () => {
        console.log(day);
        StartDayHours(dispatch, 1).then(() => reloadComponent());
    }
    const endDay = async () => {
        EndDayHours(dispatch, 1).then(() => reloadComponent());

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
            <div>
                <p>Open {'' +dayStarted}</p>
                <p>Close {'' +dayFinished}</p>

                <button onClick={startDay}>Open</button>
                <button onClick={endDay}>Close</button>
            </div>
            <DayInfo day={day}/>
        </div>

    );
};
