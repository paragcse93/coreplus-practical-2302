import React, { useState, useEffect } from "react";

import { getSupervisorPractitioners } from "../services/PractionerService";
import { Practitioner } from "../model/Practitioner";

interface SupervisorListProps {
  onSupervisorChange: (id: number, name: string) => void;
  practitionerId: number;
  practitionerName: string;
}

const SupervisorList = ({
  onSupervisorChange,
  practitionerId,
  practitionerName,
}: SupervisorListProps) => {
  const [supervisors, setSupervisors] = useState<Practitioner[]>([]);
  const [filteredSupervisors, setFilteredSupervisors] = useState<
    Practitioner[]
  >([]);

  useEffect(() => {
    (async () => {
      const response = await getSupervisorPractitioners();
      if (response.data) {
        setSupervisors(response.data);
        setFilteredSupervisors(response.data);
      }
    })();
  }, []);

  const onFilterChange = (searchTerm: string) => {
    const filteredList = supervisors.filter((item) =>
      item.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredSupervisors(filteredList);
  };

  if (supervisors.length === 0) return null;

  return (
    <div className="supervisors border rounded-lg p-4">
      <div className="text-lg font-semibold mb-2 text-gray-800 dark:text-gray-200">
        Supervisors
      </div>
      <input
        type="text"
        placeholder="Search supervisors..."
        className="block w-full p-2 mt-2 rounded-lg border border-gray-300 focus:ring-0 focus:border-primary dark:border-gray-700 dark:bg-gray-800 dark:text-gray-200"
        onChange={(event) => onFilterChange(event.target.value)}
      />
      <div className="scrollable-list max-h-40 overflow-y-auto mt-2">
        {filteredSupervisors.map((item: Practitioner) => (
          <button
            key={item.id}
            onClick={() => onSupervisorChange(item.id, item.name)}
            className={`selected-block w-full cursor-pointer rounded-lg p-2 text-left transition duration-300 hover:bg-gray-100 hover:text-gray-600 focus:bg-gray-100 focus:text-gray-600 focus:ring-2 dark:hover:bg-gray-600 dark:hover:text-gray-200 dark:focus:bg-gray-600 dark:focus:text-gray-200 ${
              practitionerId === item.id ? "selected" : ""
            }`}
            type="button"
          >
            {item.name}
          </button>
        ))}
      </div>
    </div>
  );
};
export default SupervisorList;
