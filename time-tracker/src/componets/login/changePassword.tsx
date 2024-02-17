import {useEffect, useState} from "react";

import styles from './login.module.css';
import {useTranslation} from "react-i18next";
import {useParams} from "react-router-dom";
import {
    apiResetPassword,
    isTokenValid,
    ResetPasswordData
} from "../types/config.ts";
import axios from "axios";
import toast from "react-hot-toast";

export const ChangePassword = () => {
    const {t} = useTranslation();

    const [isLoading, setIsLoading] = useState(false);


    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const {token} = useParams<string>();

    const [error, setError] = useState('');

    const [isValid, setIsValid] = useState(false);
    const [isSend, setIsSend] = useState(false);


    useEffect(() => {
        if (token != undefined) {
            setIsValid(isTokenValid(token));
        }


    }, [token]);

    const handleChangePassword = async () => {
        setIsLoading(true);
        if (password !== confirmPassword) {
            setError('Las contraseñas no coinciden');
            setIsLoading(false);
            return;
        }

        await toast.promise(handleReset(), {
            loading: t("loading"),
            success: t("passwordChanged"),
            error: t("error"),
        });


        setIsLoading(false);
    }

    const handleReset = async () => {

        const data: ResetPasswordData = {
            password: password,
            token: token
        }
        await axios.request(apiResetPassword(data));

    }


    return (
        <div className={styles.loginContainer}>
            {error && <p>{error}</p>}
            {!isValid && <p>{t("invalidToken")}</p>}

            <div className={styles.loginForm}>
                <label className={styles.loginLabel}>
                    <p>{t("password")}</p>
                    <input className={styles.loginInput} onChange={(e) => setPassword(e.target.value)} type="text"/>
                </label>
                <label className={styles.loginLabel}>
                    <p>{t("repeatPassword")}</p>
                    <input className={styles.loginInput} onChange={(e) => setConfirmPassword(e.target.value)}
                           type="text"/>
                </label>
                <div>
                    <button disabled={isLoading || !isValid } onClick={handleChangePassword}
                            className={styles.loginSubmit}>{t("changePassword")}</button>
                </div>
            </div>
        </div>


    );
};