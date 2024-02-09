
import {Route, Router, Routes} from 'react-router-dom'
import NavBar from './componets/NavBar'
import {WeekList} from "./componets/home/weekList.tsx";
import {Provider} from "react-redux";
import {store} from "./componets/slice/store.tsx";
import {ProjectInput} from "./componets/home/projectInput.tsx";
import Loader from "./loader.tsx";
import {DayInfo} from "./componets/home/dayInfo.tsx";

function App() {


  return (
    <>
        <Provider store={store}>
            <Loader />
            <div>
                <p>hola</p>
            </div>
            <Routes>

                <Route path="/" element={<WeekList/>}/>
                <Route path={"/day"} element={<DayInfo />}/>
            </Routes>


        </Provider>
    </>
  )
}

export default App
