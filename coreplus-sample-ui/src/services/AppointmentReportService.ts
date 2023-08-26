import { APIEndPoints } from "../utils/APIEndPoints";
import { fetchDataFromAPI } from "../utils/ApiDataClient";

export const getCostRevenueSummaryReport = async (reportParameter: any) => {
  return fetchDataFromAPI(
    APIEndPoints.GetMonthWiseCostRevenue,
    reportParameter
  );
};

export const getAppointmentListbyDate = async (reportParameter: any) => {
  return fetchDataFromAPI(APIEndPoints.GetAppointmentList, reportParameter);
};

export const getAppointmentDetails = async (appointmentId: number) => {
  return fetchDataFromAPI(APIEndPoints.GetAppointmentDetails, {
    appointmentId: appointmentId,
  });
};
