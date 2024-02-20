import {useEffect, useState} from "react";
import {apiInsertProjectHours, ApiInsertProjectHoursData} from "../types/api/config.ts";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "../slice/store";
import DatePicker from "react-datepicker";
import styles from './projectInput.module.css';
import {useLocation, useNavigate} from "react-router-dom";
import {useForm} from "react-hook-form";
import {useTranslation} from "react-i18next";
import axios from "axios";
import toast from "react-hot-toast";
import {fetchHours} from "../slice/hoursSlice.tsx";


export const ProjectInput = () => {
    const {t} = useTranslation();
    const navigate = useNavigate();
    const dispatch = useDispatch();



    const clients = useSelector((state: RootState) => state.clients);
    const projects = useSelector((state: RootState) => state.projects);

    const location = useLocation();
    const stateDate: Date = location.state.date ? location.state.date : undefined;


    const [filteredProjects, setFilteredProjects] = useState<ProjectModel[]>([]);


    const {register, handleSubmit, formState: {errors}} = useForm();
    const [formData, setFormData] = useState<ApiInsertProjectHoursData>({
        date: stateDate,
        clientId: -1,
        projectId: -1,
        minutes: "",
        description: ""
    });


    useEffect(() => {
        if (formData?.clientId != -1) {
            const filteredProjects = projects.filter(project => project.clientId == formData?.clientId);
            setFilteredProjects(filteredProjects);
        } else {
            setFilteredProjects([]);
        }
    }, [formData]);


    const sendData = async () => {
        const data: ApiInsertProjectHoursData = {
            projectId: formData?.projectId,
            clientId: formData?.clientId,
            minutes: formData.minutes,
            date: formData.date,
            description: formData.description
        };
        await axios.request(apiInsertProjectHours(data));
    };


    const handleSend = () => {
        toast.promise(sendData().then(() => {
            fetchHours(dispatch);
            setFormData({date: formData.date, clientId: formData.clientId, projectId: formData.projectId, minutes: "", description: ""})
        }), {
            loading: t("loading"),
            success: t("dataSaved"),
            error: t("error"),
        })


    }

    const goBack = () => navigate(-1);

    const handleInputChange = (e: { target: { name: string; value: string; }; }) => {
        const {name, value} = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    return (
        <form className={styles.formContainer} onSubmit={handleSubmit(handleSend)}>
            <div>
                <select
                    {...register("clientId", {required: true, min: 1})}
                    className={styles.formInput} onChange={handleInputChange}
                    value={formData.clientId}>
                    <option value="-1">{t('selectClient')}</option>
                    {clients.map(client => <option key={client.id} value={client.id}>{client.name}</option>)}
                </select>
                {errors.clientId?.type === 'min' && <span className={styles.errorMsg}>{t('clientRequired')}</span>}
            </div>
            <div>
                <select
                    {...register("projectId", {required: true, min: 1})}
                    className={styles.formInput}
                    value={formData.projectId}
                    onChange={handleInputChange}>
                    <option value="-1">{t('selectProject')}</option>
                    {filteredProjects.map(project => <option key={project.id}
                                                             value={project.id}>{project.name}</option>)}
                </select>
                {errors.projectId?.type === 'min' &&
                    <span className={styles.errorMsg}>{t('projectRequired')}</span>}

            </div>
            <div className={styles.sideBySideContainer}>

                <DatePicker
                    className={styles.formInput}
                    showIcon
                    {...register("date")}
                    selected={stateDate}
                    onChange={(date: Date) => {
                        setFormData((prevData) => ({
                            ...prevData,
                            date: date,
                        }));
                    }}
                />


                <input
                    className={`${styles.formInput} ${styles.formNumber}`}
                    type="number"
                    value={formData.minutes}
                    {...register("minutes", {required: true, max: 1000, min: 1})}
                    placeholder={t('enterMinutes')}
                    onChange={handleInputChange}/>
            </div>
            <div>
                {(errors.minutes?.type === 'required' || errors.minutes?.type === 'min')
                    &&
                    <span className={styles.errorMsg}>{t('minutesRequired')}</span>}

            </div>
            <div>
                <textarea
                    value={formData.description}
                    className={`${styles.formInput} ${styles.formArea}`}
                    {...register("description", {required: true})}
                    placeholder={t('enterDescription')}
                    onChange={handleInputChange}></textarea>
                {errors.description?.type === 'required' &&
                    <span className={styles.errorMsg}>{t('descriptionRequired')}</span>}

            </div>
            <div className={styles.buttonContainer}>
                <button onClick={goBack}
                        className={`${styles.cancelButton} ${styles.formButton}`}>{t('cancel')}</button>
                <button className={`${styles.saveButton} ${styles.formButton}`}>{t('save')}</button>
            </div>

        </form>
    );
};