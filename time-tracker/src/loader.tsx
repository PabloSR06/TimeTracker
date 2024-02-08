
import { Route, Routes } from 'react-router-dom'
import NavBar from './componets/NavBar'
import {WeekList} from "./componets/home/weekList.tsx";
import {Provider, useDispatch} from "react-redux";
import {store} from "./componets/slice/store.tsx";
import {ProjectInput} from "./componets/home/projectInput.tsx";
import {useEffect} from "react";
import {fetchProjects} from "./componets/slice/projectsSlice.tsx";
import {fetchClients} from "./componets/slice/clientsSlice.tsx";

function Loader() {
    const dispatch = useDispatch();


    useEffect(() => {
        //dispatch(fetchProjects());
        fetchProjects(dispatch);
        fetchClients(dispatch);

    }, []);

  return (
    <>

    </>
  )
}

export default Loader
