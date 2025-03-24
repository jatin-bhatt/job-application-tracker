import React, { useState, useEffect } from "react";
import ApplicationService from "../services/ApplicationService";
import { Link } from "react-router-dom";
import IApplicationData from '../types/Application';

const ApplicationList: React.FC = () => {
  const [applications, setApplications] = useState<Array<IApplicationData>>([]);
  const [isError, setError] = useState<boolean>(false);
  const [isLoading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    retrieveApplications();
  }, []);

  const retrieveApplications = () => {
    ApplicationService.getAll()
      .then((response: any) => {
        setApplications(response.data);
        console.log(response.data);
        setLoading(false);
      })
      .catch((e: Error) => {
        console.log(e);
        setError(true);
        setLoading(false);
      });
  };

  return (
    <React.Fragment>
      {
        isLoading ? (
          <>
          Fetching Job Application...
          </>
        ) :
      
      isError ? (
                <>
                Error Occurred while fetching Job Applications
                </>
            ) :
            <>
                <div className="list row">
                  <div className="col-md-12">
                    <h4>Job Applications List</h4>
                    <table className="table table-striped">
                      <thead>
                        <tr>
                          <th>ID</th>
                          <th>Company Name</th>
                          <th>Position</th>
                          <th>Status</th>
                          <th>Application Date</th>
                          <th></th>
                        </tr>
                      </thead>
                      <tbody>
                        {applications &&
                          applications.map((application, index) => (
                            <tr key={index}>
                              <td>{application.id}</td>
                              <td>{application.companyName}</td>
                              <td>{application.position}</td>
                              <td>{application.status}</td>
                              <td>{new Date(application.applicationDate).toLocaleDateString("en-CA")}</td>
                              <td><Link to={"/applications/" + application.id} className="badge badge-warning" >
                                    Edit
                                  </Link>
                              </td>
                            </tr>
                          ))}
                      </tbody>
                    </table>
                  </div>
              </div>

            </> 
            
          } 
      
    </React.Fragment>
    
  );
};

export default ApplicationList;
