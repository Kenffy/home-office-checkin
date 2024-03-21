import axios from "axios";
import { LoginProps } from "../pages/login/Login";
import { HomeOfficeTime } from "../types/types";

const BASE_URL = import.meta.env.VITE_SERVER_URL;
const request = axios.create({ baseURL: BASE_URL });

export const LoginAsync = (creds: LoginProps) =>
  request.post(`/employees/login`, creds);

export const GetHomeOfficeTimeAsync = (userId?: string) =>
  request.get(`/checkin/${userId}`);

export const GetHomeOfficeTimesByDayAsync = (userId?: string, date?: string) =>
  request.get(`/checkin/${userId}/${date}`);

export const StartHomeOfficeAsync = (time: HomeOfficeTime) =>
  request.post(`/checkin`, time);

export const StopHomeOfficeAsync = (time: HomeOfficeTime) =>
  request.put(`/checkin/${time.id}`, time);
