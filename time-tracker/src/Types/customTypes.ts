type DayHours = {
    "id": number,
    "userId": number,
    "type": boolean,
    "date": Date,
    "createOnDate": Date,
    "lastModifiedOnDate": Date
};
type CustomDay = {
    date: Date,
    data: DayHours[],
    projects: ProjectHours[]
};
type ProjectHours = {
    "id": number,
    "userId": number,
    "projectId": number,
    "minutes": number,
    "date": Date,
    "createOnDate": Date,
    "lastModifiedOnDate": Date
}