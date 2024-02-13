import {BaseSyntheticEvent, useEffect, useState} from "react";
import {apiInsertProjectHours, ApiInsertProjectHoursData} from "../types/config";
import "react-datepicker/dist/react-datepicker.css";
import {useSelector} from "react-redux";
import {RootState} from "../slice/store";
import DatePicker from "react-datepicker";
import axios from "axios";
import styles from './projectInput.module.css';
import {useLocation} from "react-router-dom";


export const ProjectInput = () => {

    const clients = useSelector((state: RootState) => state.clients);
    const projects = useSelector((state: RootState) => state.projects);

    const location = useLocation();
    const stateDate: Date = location.state.date ? location.state.date : undefined;


    const [selectedDate, setSelectedDate] = useState(stateDate);
    const [selectedClient, setSelectedClient] = useState(-1);
    const [selectedProject, setSelectedProject] = useState(-1);
    const [formMinutes, setFormMinutes] = useState(0);
    const [formDescription, setFormDescription] = useState<string>("");

    const [filteredProjects, setFilteredProjects] = useState<ProjectModel[]>([]);



    useEffect(() => {
        if (selectedClient !== -1) {
            const filteredProjects = projects.filter(project => project.clientId === selectedClient);
            setFilteredProjects(filteredProjects);
        } else {
            setFilteredProjects([]);
        }
    }, [selectedClient]);


    const handleClientChange = (e: BaseSyntheticEvent) => {
        setSelectedClient(parseInt(e.target.value));
    };
    const handleProjectChange = (e: BaseSyntheticEvent) => {
        setSelectedProject(parseInt(e.target.value));
    };

    const handleMinutesChange = (e: BaseSyntheticEvent) => {
        setFormMinutes(parseInt(e.target.value));
    };
    const handleDescriptionChange = (e: BaseSyntheticEvent) => {
        setFormDescription(e.target.value);
    };
    // const handleDateChange = (date: Date) => {
    //     setSelectedDate(date);
    // };

    const sendData = async () => {
        const data: ApiInsertProjectHoursData = {
            userId: 1,
            projectId: selectedProject,
            minutes: formMinutes,
            date: selectedDate
        };
        try {
            await axios.request(apiInsertProjectHours(data));
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };


        const handleSend = () => {
            // Here you can perform any action you want with the form data
            console.log("Client:", selectedClient);
            console.log("Project:", selectedProject);
            console.log("Minutes:", formMinutes);
            console.log("Description:", formDescription);
            console.log("Date:", selectedDate);

            sendData().then(() => {
                setSelectedClient(-1);
                setSelectedProject(-1);
                setFormMinutes(0);
                setFormDescription("");
                setSelectedDate(new Date());
            });


        }


    return (
        <div className={styles.formContainer}>
            <div>
                <select className={styles.formInput} onChange={handleClientChange}>
                    <option value="-1">Select a Client</option>
                    {clients.map(client => <option key={client.id} value={client.id}>{client.name}</option>)}
                </select>
            </div>
            <div>
                <select className={styles.formInput} onChange={handleProjectChange}>
                    <option value="-1">Select a Project</option>
                    {filteredProjects.map(project => <option key={project.id} value={project.id}>{project.name}</option>)}
                </select>
            </div>
            <div>
                <input className={`${styles.formInput} ${styles.formNumber}`}  type="number" placeholder="Enter a Number" onChange={handleMinutesChange}/>
            </div>
            <div>
                <textarea className={`${styles.formInput} ${styles.formArea}`}  placeholder="Enter your message" onClick={handleDescriptionChange}></textarea>
            </div>
            <div>
                <DatePicker
                    className={styles.formInput}
                    showIcon
                    selected={selectedDate}
                    onChange={(date: Date) => {
                        setSelectedDate(date);
                    }}
                />
            </div>
            <button className={styles.formButton} onClick={handleSend}>Send</button>
        </div>
    );
};