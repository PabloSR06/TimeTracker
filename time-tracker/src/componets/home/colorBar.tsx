import React, {useEffect, useState} from "react";
import styles from "./colorBar.module.css";

interface ColorBarProps {
    state: number;
}
export const ColorBar: React.FC<ColorBarProps> = ({state}) => {
    const [color, setColor] = useState<string>('');

    useEffect(() => {
        switch (state) {
            case 0:
                setColor(styles.colorRed);
                break;
            case 1:
                setColor(styles.colorOrange);
                break;
            case 2:
                setColor(styles.colorGreen);
                break;
            default:
                setColor(styles.colorRed);
                break;
        }
    }, [state]);

    return (
        <div className={`${color} ${styles.colorBar}`}>
            <p className={styles.colorBarLabel}></p>
        </div>
    );
};