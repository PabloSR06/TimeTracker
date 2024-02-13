
import {useDispatch} from "react-redux";
import {useEffect} from "react";
import {fetchProjects} from "./componets/slice/projectsSlice.tsx";
import {fetchClients} from "./componets/slice/clientsSlice.tsx";
import {fetchHours} from "./componets/slice/hoursSlice.tsx";
import {WeekList} from "./componets/week/weekList.tsx";

function Loader() {
    const dispatch = useDispatch();

    useEffect(() => {
        Promise.all([fetchHours(dispatch), fetchProjects(dispatch),fetchClients(dispatch)]).then(() => {
            console.log('All data fetched');
        });

    }, []);

  return (
    <>
        <WeekList />
    </>
  )
}

export default Loader
