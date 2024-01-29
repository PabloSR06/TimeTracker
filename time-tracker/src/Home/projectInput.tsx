import React, {useEffect, useState} from "react";
import {eachDayOfInterval, endOfWeek, minutesToHours, startOfDay, startOfWeek} from "date-fns";
import {DayBlock} from "@/Home/dayBlock";
import {apiUrl} from "@/Types/config";
import axios from "axios";

interface ProjectInputProps {
    forDate: Date;
}

export const ProjectInput: React.FC<ProjectInputProps> = ({forDate}) => {


    const [inputData, setInputData] = useState<ProjectTimeInputModel>();


    const sendData = async () => {
        // TODO: MAYBE CHANGE CONFIG FILE
        const config = {
            method: 'post',
            url: apiUrl + '/Time/InsertProjectHours',
            data: {
                "userId": 1,
                "projectId": 1,
                "minutes": 10,
                "date": "2021-09-01"
            }
        };
        try {
            const response = await axios.request(config);

        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };


    return (
        <div>
            <div>
                <input type="text" value={inputData?.ProjectId}/>
                <p>{inputData?.ProjectId}</p>
            </div>
            <div>
                <textarea/>
            </div>
            <div>
                <input type="text" value={forDate.toString()}/>
            </div>
            <div>
                <button>Save</button>
            </div>


        </div>
    );
};
