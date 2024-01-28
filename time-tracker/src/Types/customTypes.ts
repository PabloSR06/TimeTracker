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
type Project = {
    "id": number,
    "name": string,
    "description": string
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
