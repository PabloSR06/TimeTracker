"use client";
import Image from "next/image";
import styles from "./page.module.css";
import React, {useEffect} from "react";
import {store, RootState} from "@/Slice/store";
import {Provider, useDispatch, useSelector} from 'react-redux';
import {fetchClients} from "@/Slice/clientsSlice";
import {WeekList} from "@/Home/weekList";
import {ProjectInput} from "@/Home/projectInput";
import {fetchProjects} from "@/Slice/projectsSlice";

//import {todosSlice} from "@/Home/counterSlice";


export default function Home() {

    const dispatch = useDispatch();
    const count = useSelector((state: RootState) => state.clients);

    useEffect(() => {
        //dispatch(fetchProjects());
        fetchProjects(dispatch);
        fetchClients(dispatch);

    }, []);

    useEffect(() => {

    }, [count]);




    return (

    <div>
       {/*<WeekList/>*/}
        <ProjectInput forDate={new Date()}/>
    </div>
  );
}
