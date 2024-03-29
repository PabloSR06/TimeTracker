import React from "react";
import styles from "./blockData.module.css";
import {fetchHours, InsertDayHours} from "../slice/hoursSlice.tsx";
import {useDispatch} from "react-redux";
import {Clock} from "react-bootstrap-icons";
import {useTranslation} from "react-i18next";
import {combineDate} from "../tools.ts";
import {ApiInsertDayHoursData} from "../types/api/chronos.ts";

interface NewDayHourBlockProps {
    isStart: boolean;
    date: Date;
}

export const NewDayHourBlock: React.FC<NewDayHourBlockProps> = ({isStart, date}) => {

    const dispatch = useDispatch();
    const { t } = useTranslation();

    const handleClick = async () => {
        console.log('click')
        const data: ApiInsertDayHoursData = {
            date: combineDate(date),
            type: isStart
        };
        InsertDayHours(data).then(() => fetchHours(dispatch));
    }

    return (
        <div className={`${styles.blockContainer} ${styles.newProjectContainer}`} onClick={handleClick}>
            <div className={styles.clockInContainer}>
                <Clock size={13}/>
                <p className={styles.clockInText}>{isStart ? t('clockIn') : t('clockOut')}</p>
            </div>
        </div>
    );
};
