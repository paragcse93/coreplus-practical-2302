import { APIEndPoints } from "../utils/APIEndPoints";
import { fetchDataFromAPI } from "../utils/ApiDataClient";

export const getSupervisorPractitioners = async () => {
  return fetchDataFromAPI(APIEndPoints.GetSupervisors, null);
};

export const getOtherPractitioners = async () => {
  return fetchDataFromAPI(APIEndPoints.GetOthers, null);
};

export const getAllPractitioners = async () => {
  return fetchDataFromAPI(APIEndPoints.GetAll, null);
};
