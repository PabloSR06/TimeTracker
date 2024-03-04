import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {logIn} from "../slice/userSlice.tsx";
import {RootState} from "../slice/store.tsx";
import styles from './login.module.css';
import {useTranslation} from "react-i18next";
import {useForm} from "react-hook-form";
import {ApiLogInUserData, checkTokenValidity} from "../types/api/auth.ts";
import CryptoJS from "crypto-js";

export const Login = () => {
    const {t} = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const token = useSelector((state: RootState) => state.user.userToken);
    const [isLoading, setIsLoading] = useState(false);

    const {register, handleSubmit, formState: {errors}} = useForm();
    const [formData, setFormData] = useState<ApiLogInUserData>({email: '', password: ''});

    const [badLogin, setBadLogin] = useState(false);


    const goToForgotPassword = () => navigate('/password/forgot', {replace: true});

    useEffect(() => {
        if (checkTokenValidity()) {
            navigate('/', {replace: true, state: {isLoggedIn: true}});
        }
    }, [token]);

    const onSubmit = async () => {
        setIsLoading(true);

        const data: ApiLogInUserData = {
            email: formData.email,
            password: CryptoJS.SHA256(formData.password).toString()
        };

        Promise.all([
            logIn(dispatch, data)
                .then(() => {
                    setFormData({email: '', password: ''})
                    setIsLoading(false);
                })
                .catch(() => {
                    setBadLogin(true);
                    setIsLoading(false);
                })
        ]);


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
            <h3>{t('logIn')}</h3>
            <form className={styles.loginForm} onSubmit={handleSubmit(onSubmit)}>
                <label className={styles.loginLabel}>
                    <p>{t("email")}</p>
                    <input
                        className={styles.loginInput}
                        {...register("email", {required: true, pattern: /^\S+@\S+$/i})}
                        placeholder={t('email')}
                        type="text"
                        name="email"
                        value={formData.email}
                        onChange={handleInputChange}
                        autoComplete={"email"}
                    />
                    {errors.email?.type === 'required' && <span className={styles.errorMsg}>{t('emailRequired')}</span>}
                    {errors.email?.type === 'pattern' && <span className={styles.errorMsg}>Invalid email format</span>}
                </label>
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
                    {errors.password?.type === 'pattern' &&
                        <span className={styles.errorMsg}>{t('passwordRequirement')}</span>}

                </label>
                <a className={styles.forgotPassword} onClick={goToForgotPassword}>Forgot Password</a>
                <div>
                    {badLogin && <span className={styles.errorMsg}>{t('badLogin')}</span>}
                    <button disabled={isLoading} className={styles.loginSubmit} type="submit">
                        {t("logIn")}
                    </button>
                </div>
            </form>
        </div>
    );
};