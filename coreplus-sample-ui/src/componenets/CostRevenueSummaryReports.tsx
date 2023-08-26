import React from "react";
import { useState, useEffect } from "react";
import ReportParameterComponent from "./ReportParameterComponent";
import {
  getCostRevenueSummaryReport,
  getAppointmentListbyDate,
} from "../services/AppointmentReportService";
import { CostRevenueSummary } from "../model/CostRevenueSummary";
import { Appointment } from "../model/Appointment";

export interface ReportParameters {
  startDate: string;
  endDate: string;
  practitionerId: number;
}

interface CostRevenueSummaryReportsProps {
  practitionerId: number;
  practitionerName: string;
}

const CostRevenueSummaryReports = ({
  practitionerId,
  practitionerName,
}: CostRevenueSummaryReportsProps) => {
  const [costRevenueSummaryList, setCostRevenueSummaryList] = useState<
    CostRevenueSummary[]
  >([]);

  const [appointmentList, setAppointmentList] = useState<Appointment[]>([]);
  const [practitionerNameinUi, setPractitionerNameinUi] = useState("");

  const [loading, setLoading] = useState<boolean>(false);
  const [reportParameter, setReportParameter] = useState<ReportParameters>({
    startDate: "",
    practitionerId: 0,
    endDate: "",
  });

  const toggleExpand = (appointmentId: Number) => {
    const updatedAppointmentList = appointmentList.map((appointment) => {
      if (appointment.id === appointmentId) {
        return { ...appointment, isExpanded: !appointment.isExpanded };
      }
      return appointment;
    });

    setAppointmentList(updatedAppointmentList);
  };

  const onClickAppointmentRow = async () => {
    // console.log(reportParameter);

    setLoading(true);
    const response = await getAppointmentListbyDate(reportParameter);

    if (response.data) {
      if (response.data.length > 0) {
        setAppointmentList(response.data);
      } else {
        alert("No Appointment Data Found.");
      }
    } else {
      alert("No Appointment Data Found.");
    }
    setLoading(false);
  };

  const generateReport = async (reportParameter: ReportParameters) => {
    console.log(practitionerName);

    if (practitionerId == 0) {
      alert("Please select a Practisioner");
    } else {
      setAppointmentList([]);
      setCostRevenueSummaryList([]);
      reportParameter = { ...reportParameter, practitionerId: practitionerId };
      setReportParameter(reportParameter);
      setLoading(true);
      const response = await getCostRevenueSummaryReport(reportParameter);

      if (response.data) {
        //  console.log(response.data);
        if (response.data.length > 0) {
          setPractitionerNameinUi(practitionerName);
          setCostRevenueSummaryList(response.data);
        } else {
          alert("No Summary Found");
        }
      }
      setLoading(false);
    }
    //console.log(reportParameter);
  };
  return (
    <div>
      <ReportParameterComponent
        onGenerateReportClick={(reportParameter: ReportParameters) =>
          generateReport(reportParameter)
        }
      />
      <div className="my-4 border-t border-gray-300"></div>
      {costRevenueSummaryList.length > 0 && (
        <div className="flex space-x-4">
          <div className="w-1/2 overflow-hidden">
            <div className="flex justify-center bg-blue-100 text-blue-800 font-bold text-s p-1 rounded-lg mb-4">
              Cost and Revenue Summary Reports
            </div>
            <div className="h-80 overflow-y-auto">
              <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                  <tr className="bg-blue-200 text-black">
                    <th className="border px-4 py-2">Practioniner Name</th>
                    <th className="border px-4 py-2">Month Year</th>
                    <th className="border px-4 py-2">Revenue</th>
                    <th className="border px-4 py-2">Cost</th>
                  </tr>
                </thead>
                <tbody className="cursor-pointer">
                  {costRevenueSummaryList.map((summary, index) => (
                    <tr
                      className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
                      key={index}
                      onClick={() => onClickAppointmentRow()}
                    >
                      {index === 0 ? (
                        <td className="border px-4 py-2">
                          <b>{practitionerNameinUi}</b>
                        </td>
                      ) : (
                        <td className=""></td>
                      )}
                      <td className="flex justify-center border px-4 py-2">
                        {summary.month}
                      </td>
                      <td className="border px-4 py-2">
                        {summary.totalRevenue}
                      </td>
                      <td className="border px-4 py-2">{summary.totalCost}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
          {appointmentList.length > 0 && (
            <div className="w-1/2 overflow-hidden">
              <div className="flex justify-center bg-green-100 text-green-800 font-bold text-s p-1 rounded-lg mb-2">
                Appointment List Breakdown
              </div>

              <div className="h-80 overflow-y-auto">
                <table className="w-full text-sm text-left text-gray-500 dark:text-gray-400">
                  <thead className="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                    <tr className="bg-green-200 text-black">
                      <th className="border px-4 py-2">Date</th>
                      <th className="border px-4 py-2">Revenue</th>
                      <th className="border px-4 py-2">Cost</th>
                    </tr>
                  </thead>
                  <tbody className="cursor-pointer">
                    {appointmentList.map((appointment) => (
                      <>
                        <tr
                          className="bg-white border-b dark:bg-gray-800 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-600"
                          key={appointment.id}
                          onClick={() => {
                            //  console.log("Row Click");
                            toggleExpand(appointment.id);
                          }}
                        >
                          <td className="border px-4 py-2">
                            {new Date(appointment.date).toLocaleDateString(
                              "en-US",
                              {
                                year: "numeric",
                                month: "2-digit",
                                day: "2-digit",
                              }
                            )}
                          </td>
                          <td className="border px-4 py-2">
                            {appointment.revenue}
                          </td>
                          <td className="border px-4 py-2">
                            {appointment.cost}
                          </td>
                        </tr>
                        {appointment.isExpanded && (
                          <>
                            <tr>
                              <td colSpan={1}>
                                <div className="w-full max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 my-5">
                                  <div className="flex justify-end px-4 pt-4"></div>
                                  <div className="flex flex-col p-2">
                                    <span className="text-sm text-gray-500 dark:text-gray-400"></span>

                                    <dl className="max-w-md text-xs text-gray-500 divide-y divide-gray-300 dark:text-white dark:divide-gray-700">
                                      <div className="mb-2">
                                        <h6 className="text-lg font-semibold mb-1 text-center text-gray-700 dark:text-white">
                                          Appointment Details
                                        </h6>
                                        <div className="my-4 border-t border-gray-300"></div>
                                        <dt className="mb-0.5 text-gray-400 md:text-sm dark:text-gray-400">
                                          Client
                                        </dt>
                                        <dd className="text-sm font-semibold">
                                          {appointment.client_name}
                                        </dd>
                                      </div>
                                      <div className="flex flex-col py-1">
                                        <dt className="mb-0.5 text-gray-400 md:text-sm dark:text-gray-400">
                                          Type:
                                        </dt>
                                        <dd className="text-sm font-semibold">
                                          {appointment.appointment_type}
                                        </dd>
                                      </div>
                                      <div className="flex flex-col pt-1">
                                        <dt className="mb-0.5 text-gray-400 md:text-sm dark:text-gray-400">
                                          Duration
                                        </dt>
                                        <dd className="text-sm font-semibold">
                                          {appointment.duration}
                                        </dd>
                                      </div>
                                    </dl>
                                  </div>
                                </div>
                              </td>
                            </tr>
                          </>
                        )}
                      </>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      )}

      {loading && (
        <div className="flex items-center justify-center">
          <div
            className="inline-block h-8 w-8 animate-spin rounded-full border-4 border-solid border-current border-r-transparent align-[-0.125em] motion-reduce:animate-[spin_1.5s_linear_infinite]"
            role="status"
          >
            <span className="!absolute !-m-px !h-px !w-px !overflow-hidden !whitespace-nowrap !border-0 !p-0 ![clip:rect(0,0,0,0)]">
              Loading...
            </span>
          </div>
        </div>
      )}
    </div>
  );
};

export default CostRevenueSummaryReports;
