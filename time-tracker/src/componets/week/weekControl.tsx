import React from 'react';
import styles from "./weekControl.module.css";
import {addWeeks, subWeeks, startOfWeek, endOfWeek} from 'date-fns';
import {ArrowLeft, ArrowRight} from "react-bootstrap-icons";
import {useTranslation} from "react-i18next";

interface WeekControl {
    date: Date;
    setDate: (date: Date) => void;
}

export const WeekControl: React.FC<WeekControl> = ({date, setDate}) => {
    const {t} = useTranslation();

    const todayDate = new Date();
    const weekAmount = 2;
    const startDateRange = subWeeks(startOfWeek(todayDate, {weekStartsOn: 1}), weekAmount);
    const endDateRange = addWeeks(endOfWeek(todayDate, {weekStartsOn: 1}), weekAmount);


    const goToPreviousWeek = () => {
        const newDate = subWeeks(date, 1);
        if (newDate >= startDateRange) {
            setDate(newDate);
        }
    };
    const goToNextWeek = () => {
        const newDate = addWeeks(date, 1);
        if (newDate <= endDateRange) {
            setDate(addWeeks(date, 1));
        }
    };


    return (
        <div className={`blockContainer ${styles.weekControlContainer}`}>
            <ArrowLeft className={styles.controlButton} onClick={goToPreviousWeek} size={20}/>
            <p>{t('weekControl')}</p>
            <ArrowRight className={styles.controlButton} onClick={goToNextWeek} size={20}/>
        </div>
    );
};
