import React from "react";
import styles from "./blockData.module.css";
import {Clock} from "react-bootstrap-icons";

interface DayHourBlock {
    isStart: boolean;
    date: Date;
}

export const DayHourBlock: React.FC<DayHourBlock> = ({isStart, date}) => {


    const time = new Date(date);

    return (
        <div className={`${styles.blockContainer} ${styles.infoContainer}`}>
            <div className={styles.timeNumber}>
                <p>{time.getHours() + ":" + time.getMinutes()}</p>
            </div>
            <div className={styles.rightContainer}>
                <div className={styles.textContainer}>
                    <div className={styles.projectName}>
                        {isStart ? (<p>Entrada</p>) : <p>Salida</p>}
                    </div>
                </div>


                <div className={styles.iconContainer}>
                    <Clock size={13}/>
                </div>
            </div>
        </div>
    );
};
