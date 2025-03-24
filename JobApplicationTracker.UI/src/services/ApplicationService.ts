import http from "../http-common";
import IApplicationData from "../types/Application";

const getAll = () => {
  return http.get<Array<IApplicationData>>("/applications");
};

const get = (id: any) => {
  return http.get<IApplicationData>(`/application/${id}`);
};

const create = (data: IApplicationData) => {
  return http.post<IApplicationData>("/application", data);
};

const update = (id: any, data: IApplicationData) => {
  return http.put<any>(`/application/${id}`, data);
};

const ApplicationService = {
  getAll,
  get,
  create,
  update,
};

export default ApplicationService;
