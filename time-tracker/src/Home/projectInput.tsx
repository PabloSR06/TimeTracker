import React, {useEffect, useState} from "react";
import {apiInsertProjectHours, ApiInsertProjectHoursData} from "@/Types/config";
import "react-datepicker/dist/react-datepicker.css";
import {FormProvider, useForm} from "react-hook-form";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@/Slice/store";
import {number} from "prop-types";

interface ProjectInputProps {
    forDate: Date;
}

export const ProjectInput: React.FC<ProjectInputProps> = ({forDate}) => {
    const [inputData, setInputData] = useState<ProjectTimeInputModel>();
    const [selectedDate, setSelectedDate] = useState<Date>(forDate);

    const dispatch = useDispatch();

    const clients = useSelector((state: RootState) => state.clients);
    const projects = useSelector((state: RootState) => state.projects);


    const sendData = async () => {
        const data: ApiInsertProjectHoursData = {
            userId: 1,
            projectId: 1,
            minutes: 10,
            date: selectedDate
        };
        console.log(apiInsertProjectHours(data));
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