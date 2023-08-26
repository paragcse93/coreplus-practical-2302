const base_url = "https://localhost:7091";

export var APIEndPoints = {
  GetSupervisors: base_url + "/practitioners/supervisorsPractionars",
  GetOthers: base_url + "/practitioners/otherpractionars",
  GetAll: base_url + "/practitioners",
  GetMonthWiseCostRevenue: base_url + "/appointments/summarybydate?",
  GetAppointmentList: base_url + "/appointments/practitionerappointmentlist?",
  GetAppointmentDetails: base_url + "/appointments/apointmentdetails?",
};
