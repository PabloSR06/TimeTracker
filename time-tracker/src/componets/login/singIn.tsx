import {useState} from "react";
import axios from "axios";
import {apiCreateUser, ApiCreateUserData} from "../types/api/users.ts";
import CryptoJS from "crypto-js";
import styles from "./login.module.css";
import {useTranslation} from "react-i18next";
import {useForm} from "react-hook-form";
import {useNavigate} from "react-router-dom";

export const SingIn = () => {
    const {t} = useTranslation();

    const navigate = useNavigate();

    const {register, handleSubmit, formState: {errors}} = useForm();
    const [formData, setFormData] = useState<ApiCreateUserData>({email: '', password: '', name: ''});


    const [isLoading, setIsLoading] = useState(false);


    const sendData = async () => {
        const data: ApiCreateUserData = {
            name: formData.name,
            email: formData.email,
            password: CryptoJS.SHA256(formData.password).toString()
        };
        try {
            await axios.request(apiCreateUser(data));
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };

    const goToLogin = () => navigate('/login', {replace: true});

    const handleSend = () => {
        setIsLoading(true);
        sendData().then(() => {
            setFormData({email: '', password: '', name: ''})
            setIsLoading(false);
            goToLogin();
        });
    }
    const handleInputChange = (e: { target: { name: string; value: string; }; }) => {
        const {name, value} = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    return (
        <div className={styles.loginContainer}>
            <h3>{t('register')}</h3>
            <form className={styles.loginForm} onSubmit={handleSubmit(handleSend)}>
                <label className={styles.loginLabel}>
                    <p>{t("name")}</p>
                    <input
                        className={styles.loginInput}
                        {...register("name", {required: true})}
                        placeholder={t('name')}
                        type="text"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        autoComplete={"name"}
                    />
                    {errors.name?.type === 'required' && <span className={styles.errorMsg}>{t('nameRequired')}</span>}
                </label>
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
                    {errors.email?.type === 'pattern' && <span className={styles.errorMsg}>{t('emailFormat')}</span>}
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

                <div>
                    <button disabled={isLoading} className={styles.loginSubmit} type="submit">
                        {t("register")}
                    </button>
                </div>
            </form>
        </div>


    );
};