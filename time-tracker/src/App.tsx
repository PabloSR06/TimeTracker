import {Navigate, Route, Routes, useLocation} from 'react-router-dom'
import {Provider} from "react-redux";
import {store} from "./componets/slice/store.tsx";
import {DayInfo} from "./componets/home/dayInfo.tsx";
import {ProjectInput} from "./componets/home/projectInput.tsx";
import {Login} from "./componets/login/login.tsx";
import {SingIn} from "./componets/login/singIn.tsx";
import {useEffect, useState} from "react";
import Loader from "./loader.tsx";
import {checkTokenValidity} from "./componets/types/config.ts";
import {WeekList} from "./componets/week/weekList.tsx";
import {ForgotPassword} from "./componets/login/forgotPassword.tsx";
import {ChangePassword} from "./componets/login/changePassword.tsx";
import {Toaster} from "react-hot-toast";
function App() {

    const location = useLocation();

    const [token, setToken] = useState('');

    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(location.state?.isLoggedIn ?? false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        setIsLoggedIn(checkTokenValidity());

        setTimeout(() => {
            setIsLoading(false);
        }, 1000);
    }, [token]);



    if (isLoading) {
        return <div>Cargando...</div>;
    }

    const handleToken = (token: string) => {
        setToken(token);
    }

    return (
        <>
            <Provider store={store}>
                <Toaster position="top-left"/>
                <div>
                    <Loader handleToken={handleToken}/>
                </div>
                <Routes>
                    <Route path="/" element={isLoggedIn ? <WeekList/> : <Navigate to="/login"/>}/>
                    <Route path="/day" element={isLoggedIn ? <DayInfo/> : <Navigate to="/login"/>}/>
                    <Route path="/day/input" element={isLoggedIn ? <ProjectInput/> : <Navigate to="/login"/>}/>
                    <Route path="/login" element={<Login/>}/>
                    <Route path="/SingIn" element={<SingIn/>}/>
                    <Route path="*" element={<Navigate to="/"/>}/>
                    <Route path="/password/forgot" element={<ForgotPassword />}/>
                    <Route path="/password/change/:token" element={<ChangePassword />}/>
                </Routes>
            </Provider>
        </>
    )
}

export default App
