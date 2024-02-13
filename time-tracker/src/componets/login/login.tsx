import {BaseSyntheticEvent, useState} from "react";
import {apiLogInUser, ApiLogInUserData} from "../types/config.ts";
import axios from "axios";

export const Login = () => {

    const [token, setToken] = useState<string>("");


    const [selectedEmail, setSelectedEmail] = useState("");
    const [selectedPassword, setSelectedPassword] = useState("");
    const handleEmail = (e: BaseSyntheticEvent) => {
        setSelectedEmail(e.target.value);
    };
    const handlePassword = (e: BaseSyntheticEvent) => {
        setSelectedPassword(e.target.value);
    };

    const sendData = async () => {
        const data: ApiLogInUserData = {
            email: selectedEmail,
            password: selectedPassword
        };
        try {
            const response = await axios.request(apiLogInUser(data));
            setToken(response.data.token);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };

    const handleSend = () => {

        sendData().then(() => {
            setSelectedEmail("");
            setSelectedPassword("");
            localStorage.setItem('token', token);

            console.log(token);
        });

    }

    return (
        <div>
            <form >
                <label>
                    <p>Email</p>
                    <input onChange={handleEmail} type="text"/>
                </label>
                <label>
                    <p>Password</p>
                    <input onChange={handlePassword} type="password"/>
                </label>
                <div>
                    <a onClick={handleSend}>Submit</a>
                </div>
            </form>
        </div>


    );
};