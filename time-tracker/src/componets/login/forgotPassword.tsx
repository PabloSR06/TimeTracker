import {BaseSyntheticEvent, useState} from "react";
import {useDispatch,} from "react-redux";
import {ForgotPasswordEmail} from "../slice/userSlice.tsx";
import styles from './login.module.css';
import {useTranslation} from "react-i18next";

export const ForgotPassword = () => {
    const { t } = useTranslation();
    const dispatch = useDispatch();

    const [selectedEmail, setSelectedEmail] = useState("");
    const [isLoading, setIsLoading] = useState(false);


    const handleEmail = (e: BaseSyntheticEvent) => {
        setSelectedEmail(e.target.value);
    };

    
    const handleSend = async () => {
        setIsLoading(true);


        await ForgotPasswordEmail(dispatch, selectedEmail);

        setIsLoading(false);
    }

    return (
        <div className={styles.loginContainer}>

            <form className={styles.loginForm}>
                <label className={styles.loginLabel}>
                    <p>{t("email")}</p>
                    <input className={styles.loginInput} onChange={handleEmail} type="text"/>
                </label>
                <div>
                    <button disabled={isLoading} onClick={handleSend} className={styles.loginSubmit}>{t("logIn")}</button>
                </div>
            </form>
        </div>


    );
};