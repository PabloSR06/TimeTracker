import React, {useState, useEffect} from 'react';
import styles from "./weekBlock.module.css";
import {addWeeks, subWeeks, startOfWeek, endOfWeek, isSameDay} from 'date-fns';
import {useSelector} from "react-redux";
import {RootState} from "../slice/store.tsx";
import {ArrowLeft, ArrowRight} from "react-bootstrap-icons";
import {useNavigate} from "react-router-dom";

interface WeekBlockProps {
    date: Date;
}


export const WeekBlock: React.FC<WeekBlockProps> = ({date}) => {
    const navigate = useNavigate();

    const todayDate = new Date();
    const weekAmount = 2;
    const startDateRange = subWeeks(startOfWeek(todayDate, {weekStartsOn: 1}), weekAmount);
    const endDateRange = addWeeks(endOfWeek(todayDate, {weekStartsOn: 1}), weekAmount);


    const [currentDate, setCurrentDate] = useState<Date>(date);
    const [weekToShow, setWeekToShow] = useState<CustomDay[]>([]);


    const allData = useSelector((state: RootState) => state.hours);


    useEffect(() => {
        const filterData = () => {
            const startOfCurrentWeek = startOfWeek(currentDate, {weekStartsOn: 1});
            const endOfCurrentWeek = endOfWeek(currentDate, {weekStartsOn: 1});

            const selectedWeek = allData.filter((day) => {
                return startOfCurrentWeek <= new Date(day.date) && new Date(day.date) <= endOfCurrentWeek;
            }).map((day) => ({
                ...day,
                selected: isSameDay(new Date(day.date), currentDate) // Check if the day is the same as currentDate
            }));

            setWeekToShow(selectedWeek);
        }
        filterData();
    }, [currentDate, allData]);


    const goToPreviousWeek = () => {
        const newDate = subWeeks(currentDate, 1);
        if (newDate >= startDateRange) {
            setCurrentDate(newDate);
        }
    };
    const goToNextWeek = () => {
        const newDate = addWeeks(currentDate, 1);
        if (newDate <= endDateRange) {
            setCurrentDate(addWeeks(currentDate, 1));
        }
    };

    const goToDay = async (id:number) => {
        navigate(`../day`, {state: {id: id}, replace: true});

        setWeekToShow(prevWeek => (
            prevWeek.map(day => ({
                ...day,
                selected: day.id === id
            }))
        ));
    }


    return (
        <div>
            <div className={styles.weekBlockContainer}>
                <ArrowLeft className={styles.weekButton} onClick={goToPreviousWeek} size={20}/>
                <div className={styles.weekContainer}>
                    {weekToShow.map((day, index) => (
                        <div key={index} className={`${styles.dayContainer} ${day.selected ? styles.selected : ''}`} onClick={() => goToDay(day.id)}>
                            {new Date(day.date).getUTCDate()}
                        </div>
                    ))}
                </div>
                <ArrowRight className={styles.weekButton} onClick={goToNextWeek} size={20}/>
            </div>


        </div>
    );
};
