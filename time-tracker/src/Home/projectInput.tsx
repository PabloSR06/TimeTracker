import React, {useEffect, useState} from "react";
import {eachDayOfInterval, endOfWeek, minutesToHours, startOfDay, startOfWeek} from "date-fns";
import {DayBlock} from "@/Home/dayBlock";
import {apiUrl} from "@/Types/config";
import axios from "axios";
import DatePicker from "react-datepicker";
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

import "react-datepicker/dist/react-datepicker.css";
import {FormProvider, useForm} from "react-hook-form";
import {Input} from "postcss";
import {ProjectTimeSchema} from "@/Types/customTypes";

interface ProjectInputProps {
    forDate: Date;
}

export const ProjectInput: React.FC<ProjectInputProps> = ({forDate}) => {
    const [inputData, setInputData] = useState<ProjectTimeInputModel>();
    const [selectedDate, setSelectedDate] = useState<Date>(forDate);

    const sendData = async () => {
        // TODO: MAYBE CHANGE CONFIG FILE
        const config = {
            method: 'post',
            url: apiUrl + '/Time/InsertProjectHours',
            data: {
                "userId": 1,
                "projectId": 1,
                "minutes": 10,
                "date": selectedDate
            }
        };
        console.log(config);
        // try {
        //     const response = await axios.request(config);
        // } catch (error) {
        //     if (axios.isAxiosError(error)) {
        //         console.log(error);
        //     }
        // }
    };

    // const handleDateChange = (date: Date) => {
    //     setSelectedDate(date);
    // };
    // <DatePicker
    //     showIcon
    //     selected={selectedDate}
    //     onChange={handleDateChange}
    // />/


    const handleDateChange = (date: Date) => {
        setSelectedDate(date);
    };

    const { handleSubmit, register, formState: { errors } } = useForm();
    const onSubmit = values => alert(values.email + " " + values.password);

    return (
        <div className="app">
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className="formInput">
                    <label>Email</label>
                    <input
                        type="email"
                        {...register("email", {
                            required: "Required",
                            pattern: {
                                value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                                message: "invalid email address"
                            }
                        })}
                    />
                    {errors.email && errors.email.message}
                </div>
                <div className="formInput">
                    <label>Password</label>
                    <input
                        type="password"
                        {...register("password", {
                            required: "Required",
                            pattern: {
                                value: /^(?=.*[0-9])(?=.*[a-zA-Z])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,20}$/,
                                message: "Password requirements: 8-20 characters, 1 number, 1 letter, 1 symbol."
                            }
                        })}
                    />
                    {errors.password && errors.password.message}
                </div>
                <button type="submit">Submit</button>
            </form>
        </div>
    );

};