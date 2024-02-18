import {useState} from "react";
import {useDispatch,} from "react-redux";
import {ForgotPasswordEmail} from "../slice/userSlice.tsx";
import styles from './login.module.css';
import {useTranslation} from "react-i18next";
import toast from "react-hot-toast";
import {useNavigate} from "react-router-dom";
import {useForm} from "react-hook-form";

export const ForgotPassword = () => {
    const {t} = useTranslation();
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const {register, handleSubmit, formState: {errors}} = useForm();
    const [formData, setFormData] = useState({email: ''});
    const [isLoading, setIsLoading] = useState(false);

    const onSubmit = async () => {
        setIsLoading(true);

        await toast.promise(ForgotPasswordEmail(dispatch, formData.email).then(() => {
            setTimeout(() => {
                navigate('/', {replace: true});
            }, 1000);
        }), {
            loading: t("sendingEmail"),
            success: t("emailSend"),
            error: t("error"),
        });

        setIsLoading(false);
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

            <h3>{t("forgotPassword")}</h3>
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
                <div>
                    <button
                        disabled={isLoading}
                        className={styles.loginSubmit}>{t("logIn")}</button>
                </div>
            </form>
        </div>


    );
};