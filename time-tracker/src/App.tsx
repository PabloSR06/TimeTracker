import {Navigate, Route, Routes} from 'react-router-dom'
import {WeekList} from "./componets/week/weekList.tsx";
import {Provider, useDispatch} from "react-redux";
import {store} from "./componets/slice/store.tsx";
import {DayInfo} from "./componets/home/dayInfo.tsx";
import {ProjectInput} from "./componets/home/projectInput.tsx";
import {Login} from "./componets/login/login.tsx";
import {SingIn} from "./componets/login/singIn.tsx";
import {useEffect, useState} from "react";
import {jwtDecode} from "jwt-decode";
import Loader from "./loader.tsx";
import {checkTokenValidity} from "./componets/types/config.ts";

function App() {

    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        console.log('App useEffect');

        setIsLoggedIn(checkTokenValidity());

        setTimeout(() => {
            setIsLoading(false);
        }, 1000);
    }, []);



    if (isLoading) {
        return <div>Cargando...</div>;
    }




    return (
        <>
            <Provider store={store}>
                {/*<Loader />*/}
                <div>

                </div>
                <Routes>
                        {/*<Route path="/" element={<WeekList/>}/>*/}
                        {/*<Route path={"/day"} element={<DayInfo />}/>*/}
                        {/*<Route path={"/day/input"} element={<ProjectInput />}/>*/}
                        {/*<Route path={"/logIn"} element={<Login />}/>*/}
                        {/*<Route path={"/SingIn"} element={<SingIn />}/>*/}
                    <Route path="/" element={isLoggedIn ? <Loader /> : <Navigate to="/login" />} />
                    <Route path="/day" element={isLoggedIn ? <DayInfo /> : <Navigate to="/login" />} />
                    <Route path="/day/input" element={isLoggedIn ? <ProjectInput /> : <Navigate to="/login" />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/SingIn" element={<SingIn />} />
                    <Route path="*" element={<Navigate to="/" />} />



                </Routes>

            </Provider>
        </>
    )
}

export default App
