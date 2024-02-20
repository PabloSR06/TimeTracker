import {useEffect, useState} from "react";
import styles from './login.module.css';
import {useTranslation} from "react-i18next";
import {useParams} from "react-router-dom";
import axios from "axios";
import toast from "react-hot-toast";
import {useForm} from "react-hook-form";
import {isTokenValid} from "../types/api/auth.ts";
import {apiResetPassword, ResetPasswordData} from "../types/api/users.ts";

export const ChangePassword = () => {
    const {t} = useTranslation();

    const {token} = useParams<string>();

    const {register, handleSubmit, formState: {errors}} = useForm();
    const [formData, setFormData] = useState({password: '', confirmPassword: ''});

    const [isValid, setIsValid] = useState(false);


    useEffect(() => {
        if (token != undefined) {
            setIsValid(isTokenValid(token));
        }
    }, [token]);


    const onSubmit = async () => {
        if(isValid){
            const data: ResetPasswordData = {
                password: formData.password,
                token: token
            }

            await toast.promise(axios.request(apiResetPassword(data)), {
                loading: t("loading"),
                success: t("passwordChanged"),
                error: t("error"),
            });
        }else{
            toast.error(t("invalidToken"));
        }


    };

    const handleInputChange = (e: { target: { name: string; value: string; }; }) => {
        const {name, value} = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    return (
        <div className={styles.loginContainer}>
            <h3>{t("changePassword")}</h3>
            {/*{!isValid && <p>{t("invalidToken")}</p>}*/}

            <form className={styles.loginForm} onSubmit={handleSubmit(onSubmit)}>
                <label className={styles.loginLabel}>
                    <p>{t("password")}</p>
                    <input
                        className={styles.loginInput}
                        {...register("password", {
                            required: true,
                            pattern: /(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/
                        })}
                        placeholder={t('password')}
                        type="password"
                        name="password"
                        value={formData.password}
                        onChange={handleInputChange}
                        autoComplete={"current-password"}
                    />
                    {errors.password?.type === 'required' &&
                        <span className={styles.errorMsg}>{t('passwordRequired')}</span>}

                </label>
                <label className={styles.loginLabel}>
                    <p>{t("repeatPassword")}</p>
                    <input
                        className={styles.loginInput}
                        {...register("confirmPassword", {
                            required: true,
                            validate: value => value === formData.password
                        })}
                        placeholder={t('repeatPassword')}
                        type="password"
                        name="confirmPassword"
                        value={formData.confirmPassword}
                        onChange={handleInputChange}
                        autoComplete={"current-password"}
                    />
                    {errors.confirmPassword?.type === 'required' &&
                        <span className={styles.errorMsg}>{t('passwordRequired')}</span>}
                    {errors.confirmPassword?.type === 'validate' &&
                        <span className={styles.errorMsg}>{t('passwordsDoNotMatch')}</span>}
                </label>

                <div>
                    <button
                        className={styles.loginSubmit}>{t("changePassword")}</button>
                </div>
            </form>
        </div>


    );
};