import axios from "axios";
import { APIResponse } from "../helper/APIResponse";

export const fetchDataFromAPI = async (
  endPoint: string,
  queryParams: any | null
) => {
  let apiUrl = endPoint;

  if (queryParams) {
    apiUrl = endPoint + new URLSearchParams(queryParams).toString();
  }

  try {
    const response = await axios.get<APIResponse>(apiUrl);
    const { statusCode, message, data } = response.data;

    if (statusCode === 200) {
      // console.log(data);
      return { data: data };
    }

    return { statusCode, message };
  } catch (error) {
    return { statusCode: 500, message: "An error occurred" };
  }
};
