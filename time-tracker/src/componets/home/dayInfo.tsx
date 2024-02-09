import {useLocation, } from "react-router-dom";


export const DayInfo = () => {

    const location = useLocation();

    const data: CustomDay = location.state.day ? location.state.day : undefined;

    return (
        <div>
            <div>{data.date.toString()}</div>
            <div>
                {data.projects.map((project, index) => (
                    <div key={index}>
                        <div>{project.name}</div>
                        <div>{project.description}</div>
                    </div>

                ))}
            </div>


        </div>
    );
};
