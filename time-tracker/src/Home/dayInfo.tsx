import React, {useEffect, useState} from "react";
import {minutesToHours} from "date-fns";
import {DayBlock} from "@/Home/dayBlock";
import {apiUrl} from "@/Types/config";
import axios from "axios";

export const DayInfo = (props: { day: CustomDay; }) => {

    const {day} = props;



    return (
        <div>
            {day.projects.map((project, index) => (
                <div key={index}>
                    <div>{project.name}</div>
                    <div>{project.description}</div>
                </div>

            ))}


        </div>
    );
};
