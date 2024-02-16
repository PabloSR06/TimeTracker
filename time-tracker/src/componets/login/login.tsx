import {BaseSyntheticEvent, useEffect, useState} from "react";
import { ApiLogInUserData, checkTokenValidity} from "../types/config.ts";
import {useNavigate} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {logIn} from "../slice/userSlice.tsx";
import {RootState} from "../slice/store.tsx";
import styles from './login.module.css';
import {useTranslation} from "react-i18next";

export const Login = () => {
    const { t } = useTranslation();

    const navigate = useNavigate();
    const dispatch = useDispatch();


    const [selectedEmail, setSelectedEmail] = useState("");
    const [selectedPassword, setSelectedPassword] = useState("");


    const token = useSelector((state: RootState) => state.user.userToken);

    const handleEmail = (e: BaseSyntheticEvent) => {
        setSelectedEmail(e.target.value);
    };
    const handlePassword = (e: BaseSyntheticEvent) => {
        setSelectedPassword(e.target.value);
    };


    //TODO: LOADER WHEN LOGGING IN
    const handleSend = async () => {
        const data: ApiLogInUserData = {
            email: selectedEmail,
            password: selectedPassword
        };
        await logIn(dispatch, data).then(() => {
            console.log('Token: ' + token)
            if (token) {
                setSelectedEmail("");
                setSelectedPassword("");

                navigate('/', {replace: true, state: {isLoggedIn: true}});
            } else {
                console.log('No token');
            }
        });
    }

    useEffect(() => {
        if (checkTokenValidity()) {
            navigate('/', {replace: true, state: {isLoggedIn: true}});
        }
    }, [token]);

    return (
        <div className={styles.loginContainer}>

            <form className={styles.loginForm}>
                <label className={styles.loginLabel}>
                    <p>{t("email")}</p>
                    <input className={styles.loginInput} onChange={handleEmail} type="text"/>
                </label>
                <label className={styles.loginLabel}>
                    <p>{t("password")}</p>
                    <input className={styles.loginInput} onChange={handlePassword} type="password"/>
                </label>
                <div className={styles.loginSubmit}>
                    <a onClick={handleSend}>{t("logIn")}</a>
                </div>
            </form>
        </div>


    );
};