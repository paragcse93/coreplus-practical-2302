import React from "react";
import { useState, useEffect } from "react";
import { getOtherPractitioners } from "../services/PractionerService";
import { Practitioner } from "../model/Practitioner";

interface OtherListProps {
  onOtherChange: (id: number, name: string) => void;
  practitionerId: number;
  practitionerName: string;
}

const OtherPractitionerList = ({
  onOtherChange,
  practitionerId,
  practitionerName,
}: OtherListProps) => {
  const [practitioners, setPractitioners] = useState<Practitioner[]>([]);
  const [filteredPractitioners, setFilteredPractitioners] = useState<
    Practitioner[]
  >([]);
  useEffect(() => {
    (async () => {
      const response = await getOtherPractitioners();
      if (response.data) {
        setPractitioners(response.data);
        setFilteredPractitioners(response.data);
      }
    })();
  }, []);

  const onFilterChange = (searchTerm: string) => {
    const filteredList = practitioners.filter((item) =>
      item.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredPractitioners(filteredList);
  };

  if (practitioners.length === 0) return null;

  return (
    <div className="praclist border border-gray-300 p-2">
      <div className="text-lg font-semibold mb-2 text-gray-800 dark:text-gray-200">
        Remaining Practitioners
      </div>

      <input
        type="text"
        placeholder="Search supervisors..."
        className="block w-full p-2 mt-2 rounded-lg border border-gray-300 focus:ring-0 focus:border-primary dark:border-gray-700 dark:bg-gray-800 dark:text-gray-200"
        onChange={(event) => onFilterChange(event.target.value)}
      />

      <div className="scrollable-list max-h-40 overflow-y-auto mt-2">
        {filteredPractitioners.map((item) => (
          <button
            key={item.id}
            onClick={(event: any) => {
              onOtherChange(item.id, item.name);
            }}
            type="button"
            className={`${
              practitionerId == item.id ? "selected-row" : ""
            } block w-full cursor-pointer rounded-lg p-2 text-left transition duration-500 hover:bg-neutral-100 hover:text-neutral-500 focus:bg-neutral-100 focus:text-neutral-500 focus:ring-0 dark:hover:bg-neutral-600 dark:hover:text-neutral-200 dark:focus:bg-neutral-600 dark:focus:text-neutral-200`}
          >
            {item.name}
          </button>
        ))}
      </div>
    </div>
  );
};

export default OtherPractitionerList;
