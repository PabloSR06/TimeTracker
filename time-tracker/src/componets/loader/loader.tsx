import styles from './loader.module.css'
import React from "react";

interface LoaderProps {
    Loading?: boolean;
}

const Loader: React.FC<LoaderProps> = ({ Loading = true }) => {

    return (
        <>
            {Loading ? <div className={styles.loaderContainer}>
                <span className={styles.loader}></span>
            </div> : null}
        </>

    )
}

export default Loader
