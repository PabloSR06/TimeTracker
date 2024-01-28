import React from 'react';
import styles from "@/Home/dayBlock.module.css";
import {format} from "date-fns";

export const DayBlock = (props: { day: CustomDay;  }) => {

    const { day } = props;


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
        </div>
    );
};
