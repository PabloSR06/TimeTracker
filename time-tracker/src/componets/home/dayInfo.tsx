import {useLocation, } from "react-router-dom";
import {ProjectHourBlock} from "../blockData/projectHourBlock.tsx";
import {DayHourBlock} from "../blockData/dayHourBlock.tsx";
import {useEffect, useState} from "react";
import {NewProjectBlock} from "../blockData/newProjectBlock.tsx";
import {WeekBlock} from "../week/weekBlock.tsx";
import {NewDayHourBlock} from "../blockData/newDayHourBlock.tsx";
import {useSelector} from "react-redux";
import {RootState} from "../slice/store.tsx";
import {ButtonsBlock} from "../controls/buttonsBlock.tsx";


export const DayInfo = () => {

    const [startDay, setStartDay] = useState<DayHours>();
    const [endDay, setEndDay] = useState<DayHours>();

    const location = useLocation();
    const allData = useSelector((state: RootState) => state.hours);

    const index: number = location.state.id ? location.state.id : 0;
    const [data, setData] = useState<CustomDay>();



    useEffect(() => {
        setData(allData.find(obj => obj.id === index) as  CustomDay);
    }, [allData, index]);


    useEffect(() => {
        setStartDay(undefined);
        setEndDay(undefined);

        if (data && data.data) {
            const startDay = data.data.filter(item => item.type);
            const endDay = data.data.filter(item => !item.type);
            if (startDay.length > 0) {
                setStartDay(startDay[0] as DayHours);
            }
            if (endDay.length > 0) {
                setEndDay(endDay[0] as DayHours);
            }
        }
    }, [data]);

    if (!data) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <ButtonsBlock date={data.date}/>
            <WeekBlock date={data.date}/>
            <div>
                {startDay === undefined ? <NewDayHourBlock isStart={true} date={data.date}/> : <DayHourBlock isStart={true} date={startDay.date}/>}
                <div>
                    {data.projects.map((project, index) => (
                        <div key={index}>
                            <ProjectHourBlock minutes={project.minutes} projectName={project.projectName}
                                              clientName={project.clientName}/>
                        </div>
                    ))}
                    <NewProjectBlock date={data.date}/>
                </div>
                {startDay !== undefined && endDay === undefined ? <NewDayHourBlock isStart={false} date={data.date}/> : null}
                {startDay !== undefined && endDay !== undefined ? <DayHourBlock isStart={false} date={endDay.date}/> : null}
            </div>
        </div>
    );
};
