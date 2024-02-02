import React, {BaseSyntheticEvent, useEffect, useState} from "react";
import {apiInsertProjectHours, ApiInsertProjectHoursData} from "@/Types/config";
import "react-datepicker/dist/react-datepicker.css";
import {useSelector} from "react-redux";
import {RootState} from "@/Slice/store";
import ReactDatePicker from "react-datepicker";
import axios from "axios";

interface ProjectInputProps {
    forDate: Date;
}
export const ProjectInput: React.FC<ProjectInputProps> = ({forDate}) => {

    const clients = useSelector((state: RootState) => state.clients);
    const projects = useSelector((state: RootState) => state.projects);

    const [selectedDate, setSelectedDate] = useState<Date>(forDate);
    const [selectedClient, setSelectedClient] = useState(-1);
    const [selectedProject, setSelectedProject] = useState(-1);
    const [formMinutes, setFormMinutes] = useState(0);
    const [formDescription, setFormDescription] = useState<string>();

    const [filteredProjects, setFilteredProjects] = useState<ProjectModel[]>([]);


    useEffect(() => {
        if (selectedClient !== -1) {
            const filteredProjects = projects.filter(project => project.clientId === selectedClient);
            setFilteredProjects(filteredProjects);
        } else {
            setFilteredProjects([]);
        }
    }, [selectedClient, projects]);

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
    const handleDateChange = (date: Date) => {
        setSelectedDate(date);
    };

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


    useEffect(() => {
        console.log(clients);
    }, [clients]);




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
        <div className="app">
            <div className="formInput">
                <select className="col-6" value={selectedClient} onChange={handleClientChange}>
                    <option value="-1">Select a Client</option>
                    {clients.map(client => <option key={client.id} value={client.id}>{client.name}</option>)}
                </select>
            </div>
            <div className="formInput">
                <select className="col-6" value={selectedProject} onChange={handleProjectChange}>
                    <option value="-1">Select a Project</option>
                    {filteredProjects.map(project => <option key={project.id}
                                                             value={project.id}>{project.name}</option>)}
                </select>
            </div>
            <div className="formInput">
                <input type="number" value={formMinutes} onChange={handleMinutesChange}/>
            </div>
            <div className="formInput">
                <input type="text" value={formDescription} onChange={handleDescriptionChange}/>
            </div>
            <div className="formInput">
                <ReactDatePicker
                    showIcon
                    selected={selectedDate}
                    onChange={handleDateChange}
                />
            </div>

            <button onClick={handleSend}>Send</button>
        </div>
    );

};