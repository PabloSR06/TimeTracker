type DayHours = {
    "id": number,
    "userId": number,
    "type": boolean,
    "date": Date,
    "createOnDate": Date,
    "lastModifiedOnDate": Date
};
type CustomDay = {
    id: number,
    date: Date,
    data: DayHours[],
    projects: ProjectHoursName[]
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
    "projectName": string,
    "description": string,
    "clientName": string
};
type ClientModel = {
    id: number,
    name: string,
    createOnDate: Date,
    lastModifiedOnDate: Date
};