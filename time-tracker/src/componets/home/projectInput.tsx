import {useEffect, useState} from "react";
import {ApiInsertProjectHoursData} from "../types/config";
import {useSelector} from "react-redux";
import {RootState} from "../slice/store";
import DatePicker from "react-datepicker";
import styles from './projectInput.module.css';
import {useLocation} from "react-router-dom";
import {useForm} from "react-hook-form";
import {useTranslation} from "react-i18next";



export const ProjectInput = () => {
    const {t} = useTranslation();

    const clients = useSelector((state: RootState) => state.clients);
    const projects = useSelector((state: RootState) => state.projects);

    const location = useLocation();
    const stateDate: Date = location.state.date ? location.state.date : undefined;



    const [filteredProjects, setFilteredProjects] = useState<ProjectModel[]>([]);


    const {register, handleSubmit, formState: {errors}} = useForm();
    const [formData, setFormData] = useState<ApiInsertProjectHoursData>({date: stateDate, clientId: -1, projectId: -1, minutes: 0, description: ""});



    useEffect(() => {
        if (formData?.clientId != -1) {
            const filteredProjects = projects.filter(project => project.clientId == formData?.clientId);
            setFilteredProjects(filteredProjects);
        } else {
            setFilteredProjects([]);
        }
    }, [formData]);



    // const sendData = async () => {
    //     const data: ApiInsertProjectHoursData = {
    //         userId: 1,
    //         projectId: selectedProject,
    //         minutes: formMinutes,
    //         date: selectedDate
    //     };
    //     try {
    //         await axios.request(apiInsertProjectHours(data));
    //     } catch (error) {
    //         if (axios.isAxiosError(error)) {
    //             console.log(error);
    //         }
    //     }
    // };


        const handleSend = () => {
            // Here you can perform any action you want with the form data
            console.log("Client:", formData?.clientId);
            console.log("Project:", formData?.projectId);
            console.log("Minutes:", formData?.minutes);
            console.log("Description:", formData?.description);
            console.log("Date:", formData?.date);

            // sendData().then(() => {
            //     setSelectedClient(-1);
            //     setSelectedProject(-1);
            //     setFormMinutes(0);
            //     setFormDescription("");
            //     setSelectedDate(new Date());
            // });


        }

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
                    className={styles.formInput} onChange={handleInputChange}>
                    <option value="-1">{t('selectClient')}</option>
                    {clients.map(client => <option key={client.id} value={client.id}>{client.name}</option>)}
                </select>
                {errors.clientId?.type === 'required' && <span className={styles.errorMsg}>{t('clientRequired')}</span>}
            </div>
            <div>
                <select
                    {...register("projectId", {required: true, min: 1})}
                    className={styles.formInput}
                    onChange={handleInputChange}>
                    <option value="-1">{t('selectProject')}</option>
                    {filteredProjects.map(project => <option key={project.id}
                                                             value={project.id}>{project.name}</option>)}
                </select>
                {errors.projectId?.type === 'required' &&
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
                    {errors.date?.type === 'required' &&
                        <span className={styles.errorMsg}>{t('dateRequired')}</span>}


                    <input
                        className={`${styles.formInput} ${styles.formNumber}`}
                        type="number"
                        {...register("minutes", {required: true, max: 1000, min: 1})}
                        placeholder={t('enterMinutes')}
                        onChange={handleInputChange}/>
                    {errors.minutes?.type === 'required' &&
                        <span className={styles.errorMsg}>{t('minutesRequired')}</span>}


            </div>

            <div>
                <textarea
                    className={`${styles.formInput} ${styles.formArea}`}
                    {...register("description", {required: true})}
                    placeholder={t('enterDescription')}
                    onChange={handleInputChange}></textarea>
                {errors.description?.type === 'required' &&
                    <span className={styles.errorMsg}>{t('descriptionRequired')}</span>}

            </div>

            <button className={styles.formButton}>{t('save')}</button>
        </form>
    );
};