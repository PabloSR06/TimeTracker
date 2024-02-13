import {BaseSyntheticEvent, useEffect, useState} from "react";
import {apiLogInUser, ApiLogInUserData, checkTokenValidity} from "../types/config.ts";
import axios from "axios";
import {useNavigate} from "react-router-dom";
import {jwtDecode} from "jwt-decode";

export const Login = () => {
    const navigate = useNavigate();

    const [selectedEmail, setSelectedEmail] = useState("");
    const [selectedPassword, setSelectedPassword] = useState("");

    const [isLoggedIn, setIsLoggedIn] = useState(false);

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
            return response.data.token;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                console.log(error);
            }
        }
    };

    const handleSend = () => {
        sendData().then(token => {
            if (token) {
                console.log(token);
                localStorage.setItem('token', token);
                setSelectedEmail("");
                setSelectedPassword("");
            }
            navigate('/', {replace: true});
        });


    }


    useEffect(() => {
        console.log('Login useEffect');
        setIsLoggedIn(checkTokenValidity());
        if (isLoggedIn) {
            navigate('/', {replace: true});
        }

        console.log(isLoggedIn);

    }, []);



    return (
        <div>
            <form>
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