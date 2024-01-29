"use client";
import Image from "next/image";
import styles from "./page.module.css";
import React from "react";
import {WeekList} from "@/Home/weekList";
import {DayBlock} from "@/Home/dayBlock";
import {ProjectInput} from "@/Home/projectInput";



export default function Home() {

  return (
    <div>
       {/*<WeekList/>*/}
        <ProjectInput forDate={new Date()}/>
    </div>
  );
}
