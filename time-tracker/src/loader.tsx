
import {useDispatch, useSelector} from "react-redux";
import {useEffect} from "react";
import {RootState} from "./componets/slice/store.tsx";
import {fetchHours} from "./componets/slice/hoursSlice.tsx";
import {fetchProjects} from "./componets/slice/projectsSlice.tsx";
import {fetchClients} from "./componets/slice/clientsSlice.tsx";
import {checkTokenValidity} from "./componets/types/config.ts";
import toast from "react-hot-toast";
import {useTranslation} from "react-i18next";

interface LoaderProps {
    handleToken: (token: string) => void;
}
export const Loader: React.FC<LoaderProps> = ({handleToken}) => {
    const {t} = useTranslation();

    const dispatch = useDispatch();

    const token = useSelector((state: RootState) => state.user.userToken);

    const loadAllData = async () => {
        await Promise.all([fetchHours(dispatch), fetchProjects(dispatch),fetchClients(dispatch)]).then(() => {
            console.log('All data fetched');
        });
    }

    useEffect(() => {
        if(checkTokenValidity()){

            toast.promise(loadAllData(), {
                loading: t("loading"),
                success: t("dataLoaded"),
                error: t("error"),
            });
        }
        handleToken(token);
    }, [token]);



  return (
    <>

    </>
  )
}

export default Loader
