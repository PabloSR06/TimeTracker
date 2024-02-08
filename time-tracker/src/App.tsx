
import { Route, Routes } from 'react-router-dom'
import NavBar from './componets/NavBar'
import {WeekList} from "./componets/home/weekList.tsx";
import {Provider} from "react-redux";
import {store} from "./componets/slice/store.tsx";
import {ProjectInput} from "./componets/home/projectInput.tsx";
import Loader from "./loader.tsx";


function App() {


  return (
    <>
        <Provider store={store}>
            <Loader />
        <NavBar />
       <Routes>
          <Route path="/" element={<WeekList />} />
          <Route path="/products" element={<ProjectInput />} />
       </Routes>
        </Provider>
    </>
  )
}

export default App
