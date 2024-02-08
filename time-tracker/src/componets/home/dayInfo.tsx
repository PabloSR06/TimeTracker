
export const DayInfo = (props: { day: CustomDay; }) => {

    const {day} = props;



    return (
        <div>
            {day.projects.map((project, index) => (
                <div key={index}>
                    <div>{project.name}</div>
                    <div>{project.description}</div>
                </div>

            ))}


        </div>
    );
};
