import styles from "./blockData.module.css";
import React from "react";
import {Kanban} from "react-bootstrap-icons";

interface DataBlockProps {
    minutes: number;
    clientName: string;
    projectName: string;

}

export const ProjectHourBlock: React.FC<DataBlockProps> = ({minutes, clientName, projectName}) => {

    return (
        <div className={`${styles.blockContainer} ${styles.infoContainer}`}>

            <div className={styles.timeNumber}>
                <p>{minutes}</p>
            </div>
            <div className={styles.rightContainer}>
                <div className={styles.textContainer}>
                    <div className={styles.projectName}>
                        <p>{projectName}</p>
                    </div>
                    <div className={styles.clientName}>
                        <p>{clientName}</p>
                    </div>
                </div>
                <div className={styles.iconContainer}>
                    <Kanban size={13}/>
                </div>
            </div>
        </div>
    );
};
