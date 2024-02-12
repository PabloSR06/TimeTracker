import {useLocation, } from "react-router-dom";
import {ProjectHourBlock} from "../blockData/projectHourBlock.tsx";
import {DayHourBlock} from "../blockData/dayHourBlock.tsx";
import {useEffect, useState} from "react";
import {NewProjectBlock} from "../blockData/newProjectBlock.tsx";

export const DayInfo = () => {

    const location = useLocation();

    const data: CustomDay = location.state.day ? location.state.day : undefined;

    const [startDay, setStartDay] = useState<DayHours>();
    const [endDay, setEndDay] = useState<DayHours>();

    useEffect(() => {
        const startDay = data.data.filter(item => item.type);
        const endDay = data.data.filter(item => !item.type);
        if(startDay.length > 0) {
            setStartDay(startDay[0] as DayHours);
        }
        if(endDay.length > 0) {
            setEndDay(endDay[0] as DayHours);
        }
    }, [data]);

    return (
        <div>
            {startDay === undefined ? null : <DayHourBlock isStart={true} date={startDay.date}/>}
            <div>
                {data.projects.map((project, index) => (
                    <div key={index}>
                        <ProjectHourBlock minutes={project.minutes} projectName={project.projectName} clientName={project.clientName}/>
                    </div>
                ))}
                <NewProjectBlock date={data.date}/>
            </div>
            {endDay === undefined ? null : <DayHourBlock isStart={false} date={endDay.date}/>}
        </div>
    );
};
