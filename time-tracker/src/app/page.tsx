"use client";
import Image from "next/image";
import styles from "./page.module.css";
import React, {useEffect} from "react";
import {store, RootState} from "@/Slice/store";
import {Provider, useDispatch, useSelector} from 'react-redux';
import {fetchClients} from "@/Slice/clientsSlice";
import {WeekList} from "@/Home/weekList";

//import {todosSlice} from "@/Home/counterSlice";


export default function Home() {

    const dispatch = useDispatch();
    const count = useSelector((state: RootState) => state.clients);

    useEffect(() => {
        //dispatch(fetchProjects());
        fetchClients(dispatch);

    }, []);

    useEffect(() => {

        console.log(count);
    }, [count]);

    // useEffect(() => {
    //     dispatch(todoAdded()); // Despacha la acción para cargar los todos cuando el componente se monte
    //     console.log(count);
    // }, [dispatch]); // Asegúrate de incluir dispatch como dependencia para evitar advertencias de lint


    return (

    <div>
       <WeekList/>
       {/* <ProjectInput forDate={new Date()}/>*/}
    </div>
  );
}
