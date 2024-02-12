import styles from "./blockData.module.css";
import React from "react";
import {Plus} from "react-bootstrap-icons";
import {useNavigate} from "react-router-dom";

interface NewProjectBlockProps {
    date: Date;
}

export const NewProjectBlock: React.FC<NewProjectBlockProps> = ({date}) => {

    const navigate = useNavigate();
    const goToForm  = () => {
        // navigate(`/day/${index}`);
        navigate(`/day/input`, {state: {date: date}});
    }

    return (
        <div className={`${styles.blockContainer} ${styles.newProjectContainer}`} onClick={goToForm}>
                <div>
                    <Plus size={30}/>
                </div>
        </div>
    );
};
