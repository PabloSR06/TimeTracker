import "./index.css";
import {useDispatch, useSelector} from "react-redux";
import {useEffect, useState} from "react";
import {RootState} from "./componets/slice/store.tsx";
import {fetchHours} from "./componets/slice/hoursSlice.tsx";
import {fetchProjects} from "./componets/slice/projectsSlice.tsx";
import {fetchClients} from "./componets/slice/clientsSlice.tsx";
import {checkTokenValidity} from "./componets/types/config.ts";
import toast from "react-hot-toast";
import {useTranslation} from "react-i18next";
import Loader from "./componets/loader/loader.tsx";

interface LoaderProps {
    handleToken: (token: string) => void;
}

export const DataLoader: React.FC<LoaderProps> = ({handleToken}) => {
    const {t} = useTranslation();

    const dispatch = useDispatch();

    const token = useSelector((state: RootState) => state.user.userToken);

    const [isLoading, setIsLoading] = useState(false);

    const loadAllData = async () => {
        setIsLoading(true);

        try {
            await Promise.all([fetchHours(dispatch), fetchProjects(dispatch), fetchClients(dispatch)]).then(() => {
                console.log('All data fetched');
                toast.success(t("dataLoaded"));
            });
        } catch (error) {
            console.error('Error fetching data:', error);
            toast.error(t("error"));
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        if (checkTokenValidity()) {

            loadAllData();
        }
        handleToken(token);
    }, [token]);


    return (
        <>
            <Loader Loading={isLoading}/>
        </>
    )
}

export default DataLoader
