import "./app.css";
import SupervisorList from "./componenets/SupervisorList";
import React, { useState } from "react";
import OtherPractitionerList from "./componenets/OtherPracitionersList";
import CostRevenueSummaryReports from "./componenets/CostRevenueSummaryReports";

function App() {
  const [practitionerId, setPractitionerId] = useState<number>(0);
  const [practitionerName, setpractitionerName] = useState<string>("");

  const togglePractitioner = (
    practitioner_id: number,
    practitioner_name: string
  ) => {
    if (practitionerId === practitioner_id) {
      setPractitionerId(0);
      setpractitionerName("");
    } else {
      setPractitionerId(practitioner_id);
      setpractitionerName(practitioner_name);
    }
  };

  return (
    <div className="h-screen w-full appshell">
      <div className="header flex flex-row items-center p-2 bg-primary shadow-sm">
        <p className="font-bold text-lg">coreplus</p>
      </div>
      <div className="supervisors">
        <SupervisorList
          practitionerId={practitionerId}
          practitionerName={practitionerName}
          onSupervisorChange={(
            practitionerId: number,
            practitionerName: string
          ) => togglePractitioner(practitionerId, practitionerName)}
        />
      </div>

      <div className="praclist grid grid-rows-1 p-2 row-start-3 row-end-4">
        <OtherPractitionerList
          practitionerId={practitionerId}
          practitionerName={practitionerName}
          onOtherChange={(practitionerId, practitionerName) =>
            togglePractitioner(practitionerId, practitionerName)
          }
        />
      </div>
      <div className="pracinfo">
        <CostRevenueSummaryReports
          practitionerName={practitionerName}
          practitionerId={practitionerId}
        />
      </div>
    </div>
  );
}

export default App;
