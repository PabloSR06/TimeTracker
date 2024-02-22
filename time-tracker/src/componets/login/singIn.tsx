import {BaseSyntheticEvent, useState} from "react";
import axios from "axios";
import {apiCreateUser, ApiCreateUserData} from "../types/api/users.ts";

export const SingIn = () => {

    const [selectedEmail, setSelectedEmail] = useState("");
    const [selectedPassword, setSelectedPassword] = useState("");
    const handleEmail = (e: BaseSyntheticEvent) => {
        setSelectedEmail(e.target.value);
    };
    const handlePassword = (e: BaseSyntheticEvent) => {
        setSelectedPassword(e.target.value);
    };

    const sendData = async () => {
        const data: ApiCreateUserData = {
            email: selectedEmail,
            password: selectedPassword
        };
        try {
            await axios.request(apiCreateUser(data));
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
                    <button type="submit" onClick={handleSend}>Submit</button>
                </div>
            </form>
        </div>


    );
};