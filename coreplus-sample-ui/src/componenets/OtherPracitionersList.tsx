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
    <div className="flex flex-col items-center justify-end h-full">
      <div className="flex flex-col w-full max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 p-2">
        <div className="flex text-sm flex-col p-1 text-center">
          Remaining Practitioners
        </div>

        <input
          type="text"
          placeholder="Search supervisors..."
          className="block w-full p-2 mt-2 rounded-lg border border-gray-300 focus:ring-0 focus:border-primary dark:border-gray-700 dark:bg-gray-800 dark:text-gray-200"
          onChange={(event) => onFilterChange(event.target.value)}
        />

        <div className="scrollable-list max-h-64 overflow-y-auto mt-2">
          {filteredPractitioners.map((item) => (
            <button
              key={item.id}
              onClick={(event: any) => {
                onOtherChange(item.id, item.name);
              }}
              type="button"
              className={`${
                practitionerId == item.id ? "selected" : ""
              } selected-block w-full cursor-pointer rounded-lg p-2 text-left  text-sm transition duration-300 hover:bg-gray-100 hover:text-gray-600 focus:bg-gray-100 focus:text-gray-600 focus:ring-2 dark:hover:bg-gray-600 dark:hover:text-gray-200 dark:focus:bg-gray-600 dark:focus:text-gray-200`}
            >
              {item.name}
            </button>
          ))}
        </div>
      </div>
    </div>
  );
};

export default OtherPractitionerList;
