import React from 'react';
import styles from "@/Home/dayBlock.module.css";
import {format} from "date-fns";

export const DayBlock = (props: { day: any;  }) => {

    const { day } = props;
    //format(dia, 'EEEE, d MMM yyyy')
    return (
        <div className={styles.dayContainer}>
            <div className={styles.dateContainer}>
                <p>{format(day, 'EEEE')}</p>
                <p>{format(day, 'd/MM')}</p>
            </div>
            <div>
                <p>8h</p>
            </div>
        </div>
    );
};
