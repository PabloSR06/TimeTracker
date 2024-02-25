import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import "./react-datepicker.css";
import {BrowserRouter} from "react-router-dom";
import i18n from "./i18n.ts";
import {I18nextProvider,} from "react-i18next";


ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
        <I18nextProvider i18n={i18n}>
            <BrowserRouter>

                <div className="main">
                    <div className="appMain">
                        <App />
                    </div>
                    <div className="nameMain">
                        <p>TimeTracker</p>
                        <p>By Pablo Suárez</p>
                    </div>
                </div>
            </BrowserRouter>
        </I18nextProvider>
    </React.StrictMode>
);
