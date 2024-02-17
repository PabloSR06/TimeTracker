import React from 'react';
import styles from "./buttonsBlock.module.css";
import {BoxArrowInRight, House} from "react-bootstrap-icons";
import {format} from "date-fns";
import {useTranslation} from "react-i18next";
import {useNavigate} from "react-router-dom";


interface ButtonsBlockProps {
    date: Date;
}

export const ButtonsBlock: React.FC<ButtonsBlockProps> = ({date}) => {
    const { t } = useTranslation();
    const navigate = useNavigate();


    const dateFormat = t('smallDateFormat');


    const handleHome = () => {
        navigate(`/`, {replace: true});
    };

    return (
        <div className={styles.buttonsBlockContainer}>
            <div className={styles.iconContainer} onClick={handleHome}>
                <House size={20}/>
            </div>
            <div>
                <p className={styles.dateText}>{format(date, dateFormat)}</p>
            </div>
            <div className={styles.iconContainer}>
                <BoxArrowInRight size={20}/>
            </div>
        </div>


    );
};
