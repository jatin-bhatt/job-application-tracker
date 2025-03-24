import React, { useState, useEffect, ChangeEvent } from "react";
import { useParams } from 'react-router-dom';
import ApplicationDataService from "../services/ApplicationService";
import IApplicationData from "../types/Application";
import DropDownList from "./DropDownList";
import { Status } from "../types/Status";

const Application: React.FC = () => {
  const { id } = useParams();
  const initialApplicationState = {
    id: null,
    companyName: "",
    position: "",
    status: "",
    applicationDate: new Date(),
  };
  const [currentApplication, setCurrentApplication] = useState<IApplicationData>(initialApplicationState);
  const [message, setMessage] = useState<string>("");
  const [errors, setErrors] = useState<{ companyName?: string; position?: string; applicationDate?: string }>({});

  const getApplication = (id: string) => {
    ApplicationDataService.get(id)
      .then((response: any) => {
        setCurrentApplication(response.data);
        console.log(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
      });
  };

  useEffect(() => {
    if (id) getApplication(id);
  }, [id]);

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setCurrentApplication({ ...currentApplication, [name]: name === "applicationDate" ? new Date(value) : value });
  };

  const handleDropdownChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = event.target;
    setCurrentApplication({ ...currentApplication, [name]: value });
  };

  const isValidDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.getFullYear() > 1900 && !isNaN(date.getTime());
  };

  const validate = () => {
    let validationErrors: { companyName?: string; position?: string; applicationDate?: string } = {};
    
    if (!currentApplication.companyName.trim()) {
      validationErrors.companyName = "Company name is required.";
    }
    if (!currentApplication.position.trim()) {
      validationErrors.position = "Position is required.";
    }
    if (!isValidDate(currentApplication.applicationDate.toString())) {
      validationErrors.applicationDate = "Invalid application date.";
    }

    setErrors(validationErrors);
    return Object.keys(validationErrors).length === 0;
  };

  const updateApplication = () => {
    if (!validate()) return;

    ApplicationDataService.update(currentApplication.id, currentApplication)
      .then((response: any) => {
        console.log(response.data);
        setMessage("The application was updated successfully!");
      })
      .catch((e: Error) => {
        console.log(e);
      });
  };

  return (
    <div>
      {currentApplication ? (
        <div className="edit-form">
          <h4>Application</h4>
          <form>
            <div className="form-group">
              <label htmlFor="companyName">Company Name</label>
              <input
                type="text"
                className="form-control"
                id="companyName"
                name="companyName"
                value={currentApplication.companyName}
                onChange={handleInputChange}
              />
              {errors.companyName && <div className="text-danger">{errors.companyName}</div>}
            </div>

            <div className="form-group">
              <label htmlFor="position">Position</label>
              <input
                type="text"
                className="form-control"
                id="position"
                name="position"
                value={currentApplication.position}
                onChange={handleInputChange}
              />
              {errors.position && <div className="text-danger">{errors.position}</div>}
            </div>

            <div className="form-group">
              <label htmlFor="status">Status</label>
              <DropDownList
                id="status"
                name="status"
                className="form-control"
                data={Object.entries(Status).map(([value, key]) => ({ key: key as string, value }))}
                value={currentApplication.status}
                onChange={handleDropdownChange}
              />
            </div>

            <div className="form-group">
              <label htmlFor="applicationDate">Application Date</label>
              <input
                type="date"
                className="form-control"
                id="applicationDate"
                name="applicationDate"
                value={new Date(currentApplication.applicationDate).toLocaleDateString("en-CA")} // ISO format for date
                onChange={handleInputChange}
              />
              {errors.applicationDate && <div className="text-danger">{errors.applicationDate}</div>}
            </div>

          </form>

          <button
            type="submit"
            className="badge badge-success"
            onClick={updateApplication}
          >
            Update
          </button>
          <p>{message}</p>
        </div>
      ) : (
        <div>
          <br />
          <p>Please click on an application...</p>
        </div>
      )}
    </div>
  );
};

export default Application;
