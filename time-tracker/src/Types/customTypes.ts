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
    projects: ProjectHoursName[]
};
type ProjectHours = {
    "id": number,
    "userId": number,
    "projectId": number,
    "minutes": number,
    "date": Date,
    "createOnDate": Date,
    "lastModifiedOnDate": Date
};
type ProjectModel = {
    "id": number,
    "name": string,
    "description": string,
    "clientId": number,
};
type ProjectHoursName = {
    "id": number,
    "userId": number,
    "projectId": number,
    "minutes": number,
    "date": Date,
    "name": string,
    "description": string,
};
type ProjectTimeInputModel = {
    "userId": number,
    "Minutes": number,
    "Date": Date,
    "ProjectId": number
};
type ClientModel = {
    id: number,
    name: string,
    createOnDate: Date,
    lastModifiedOnDate: Date
};