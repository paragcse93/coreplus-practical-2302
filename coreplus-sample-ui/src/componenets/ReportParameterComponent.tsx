import React from "react";
import { useState, useEffect } from "react";

interface ReportParameterComponentProps {
  onGenerateReportClick: (reportParameter: ReportParameters) => void;
}

interface ReportParameters {
  startDate: string;
  endDate: string;
  practitionerId: number;
}

const ReportParameterComponent = ({
  onGenerateReportClick,
}: ReportParameterComponentProps) => {
  const [reportParameter, setReportParameter] = useState<ReportParameters>({
    startDate: "",
    practitionerId: 0,
    endDate: "",
  });

  const onChangeStartDate = (event: any) => {
    //  console.log(event.target.value);
    setReportParameter((prevState: ReportParameters) => {
      return { ...prevState, startDate: event.target.value };
    });
  };

  const onChangeEndDate = (event: any) => {
    //  console.log(event.target.value);
    setReportParameter((prevState: ReportParameters) => {
      return { ...prevState, endDate: event.target.value };
    });
  };

  const onGenerateClick = () => {
    if (reportParameter.startDate && reportParameter.endDate) {
      onGenerateReportClick(reportParameter);
    } else {
      alert("Invalid Date");
    }
  };

  return (
    <div className="w-1/2">
      <div className="w-full max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700">
        <div className="flex justify-end px-4 pt-4"></div>
        <div className="flex flex-col p-5">
          <span className="text-sm text-gray-500 dark:text-gray-400">
            <h6 className="text-sm font-semibold mb-4">
              Cost and Revenue Summary Reports
            </h6>
          </span>

          <div className="mb-4">
            <label htmlFor="startDate" className="block font-medium mb-1">
              Start Date
            </label>
            <input
              style={{ width: "100%" }}
              type="date"
              onChange={onChangeStartDate}
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full pl-2 py-1.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              id="startDate"
            />
          </div>
          <div className="mb-4">
            <label htmlFor="endDate" className="block font-medium mb-1">
              End Date
            </label>
            <input
              style={{ width: "100%" }}
              type="date"
              onChange={onChangeEndDate}
              className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full pl-2 py-1.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              id="endDate"
            />
          </div>
          <button
            className="bg-blue-500 hover:bg-blue-600 text-white text-sm font-medium py-2 px-4 rounded-lg"
            onClick={(event: any) => {
              onGenerateClick();
            }}
          >
            Generate Report
          </button>
        </div>
      </div>
    </div>
  );
};

export default ReportParameterComponent;
