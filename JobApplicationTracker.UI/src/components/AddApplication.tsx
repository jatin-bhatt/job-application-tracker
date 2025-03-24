import React, { useState, ChangeEvent } from "react";
import ApplicationDataService from "../services/ApplicationService";
import IApplicationData from '../types/Application';
import DropDownList from "./DropDownList";
import { Status } from "../types/Status";

const AddApplication: React.FC = () => {
  const initialApplicationState = {
    id: null,
    companyName: "",
    position: "",
    status: Status.Submitted,
    applicationDate: new Date(),
  };
  const [application, setApplication] = useState<IApplicationData>(initialApplicationState);
  const [submitted, setSubmitted] = useState<boolean>(false);
  const [errors, setErrors] = useState<{ companyName?: string; position?: string; applicationDate?:string; saveError?: string }>({});

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setApplication({ ...application, [name]: value });
  };

  const handleDropdownChange = (event: ChangeEvent<HTMLSelectElement>) => {
    const { name, value } = event.target;
    setApplication({ ...application, [name]: value });
  };


  const isValidDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.getFullYear() > 1900 && !isNaN(date.getTime());
  }

  const validate = () => {
    let validationErrors: { companyName?: string; position?: string; applicationDate? : string } = {};
    if (!application.companyName.trim()) {
      validationErrors.companyName = "Company name is required.";
    }
    if (!application.position.trim()) {
      validationErrors.position = "Position is required.";
    }
    if (!isValidDate(application.applicationDate.toString())){
      validationErrors.applicationDate = "Invalid Date";
    }

    setErrors(validationErrors);
    return Object.keys(validationErrors).length === 0;
  };

  const saveApplication = () => {
    if (!validate()) return;

    var data = {
      companyName: application.companyName,
      position: application.position,
      status: application.status,
      applicationDate: new Date(application.applicationDate),
    };

    ApplicationDataService.create(data)
      .then((response: any) => {
        setApplication({
          id: response.data.id,
          companyName: response.data.companyName,
          position: response.data.position,
          status: response.data.status,
          applicationDate: response.data.applicationDate,
        });
        setSubmitted(true);
        setErrors({});
        console.log(response.data);
      })
      .catch((e: Error) => {
        console.log(e);
        setErrors(prevErrors => ({ ...prevErrors, saveError: "Failed to save application. Please try again." }));
      });
  };

  const newApplication = () => {
    setApplication(initialApplicationState);
    setSubmitted(false);
    setErrors({});
  };

  return (
    <div className="submit-form">
      {submitted ? (
        <div>
          <h4>Submitted successfully!</h4>
          <button className="btn btn-success" onClick={newApplication}>
            Add
          </button>
        </div>
      ) : (
        <div>
          <div className="form-group">
            <label htmlFor="companyName">Company Name</label>
            <input
              type="text"
              className="form-control"
              id="companyName"
              name="companyName"
              value={application.companyName}
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
              value={application.position}
              onChange={handleInputChange}
            />
            {errors.position && <div className="text-danger">{errors.position}</div>}
          </div>

          <div className="form-group">
            <label htmlFor="status">Status</label>
            <DropDownList
              id="status"
              className="form-control"
              name="status"
              data={
                Object.entries(Status)
                  .filter(x => isNaN(parseInt(x[0])))
                  .map(([value, key]) => ({ key: key as string, value }))
              }
              value={application.status} 
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
              value={new Date(application.applicationDate).toLocaleDateString("en-CA")}
              onChange={handleInputChange}
            />
            {errors.applicationDate && <div className="text-danger">{errors.applicationDate}</div>}
          </div>
          {errors.saveError && <div className="text-danger">{errors.saveError}</div>}
          <button onClick={saveApplication} className="btn btn-success">
            Submit
          </button>
        </div>
      )}
    </div>
  );
};

export default AddApplication;