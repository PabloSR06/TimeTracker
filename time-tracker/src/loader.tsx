
import {useDispatch, useSelector} from "react-redux";
import {useEffect} from "react";
import {RootState} from "./componets/slice/store.tsx";
import {fetchHours} from "./componets/slice/hoursSlice.tsx";
import {fetchProjects} from "./componets/slice/projectsSlice.tsx";
import {fetchClients} from "./componets/slice/clientsSlice.tsx";
import {checkTokenValidity} from "./componets/types/config.ts";

interface LoaderProps {
    handleToken: (token: string) => void;
}
export const Loader: React.FC<LoaderProps> = ({handleToken}) => {
    const dispatch = useDispatch();

    const token = useSelector((state: RootState) => state.user.userToken);

    useEffect(() => {
        if(checkTokenValidity()){
            Promise.all([fetchHours(dispatch), fetchProjects(dispatch),fetchClients(dispatch)]).then(() => {
                console.log('All data fetched');
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
