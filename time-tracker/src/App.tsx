import {Route, Routes} from 'react-router-dom'
import {WeekList} from "./componets/week/weekList.tsx";
import {Provider} from "react-redux";
import {store} from "./componets/slice/store.tsx";
import Loader from "./loader.tsx";
import {DayInfo} from "./componets/home/dayInfo.tsx";
import {ProjectInput} from "./componets/home/projectInput.tsx";
import {Login} from "./componets/login/login.tsx";
import {SingIn} from "./componets/login/singIn.tsx";

function App() {


  return (
    <>
        <Provider store={store}>
            {/*<Loader />*/}
            <div>

            </div>
            <Routes>
                <Route path="/" element={<WeekList/>}/>
                <Route path={"/day"} element={<DayInfo />}/>
                <Route path={"/day/input"} element={<ProjectInput />}/>
                <Route path={"/logIn"} element={<Login />}/>
                <Route path={"/SingIn"} element={<SingIn />}/>
            </Routes>


        </Provider>
    </>
  )
}

export default App
