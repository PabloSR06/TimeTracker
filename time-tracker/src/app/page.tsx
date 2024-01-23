"use client";
import Image from "next/image";
import styles from "./page.module.css";
import React from "react";
import {WeekList} from "@/Home/weekList";
import {DayBlock} from "@/Home/dayBlock";



export default function Home() {
    const today = new Date();

  return (
    <div>
        <WeekList/>
    </div>
  );
}
