import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Link, Route, Routes } from "react-router-dom";
import ApplicationList from "./components/ApplicationList";
import AddApplication from "./components/AddApplication";
import Application from "./components/Application";

const App: React.FC = () => {
  return (
    <div>
      <nav className="navbar navbar-expand navbar-dark bg-dark">
        <a href="/applications" className="navbar-brand">
          Job Application Tracker
        </a>
        <div className="navbar-nav mr-auto">
          <li className="nav-item">
            <Link to={"/applications"} className="nav-link">
              Applications
            </Link>
          </li>
          <li className="nav-item">
            <Link to={"/add"} className="nav-link">
              Add
            </Link>
          </li>
        </div>
      </nav>

      <div className="container mt-3">
        <Routes>
          <Route path="/" element={<ApplicationList/>} />
          <Route path="/applications" element={<ApplicationList/>} />
          <Route path="/add" element={<AddApplication/>} />
          <Route path="/applications/:id" element={<Application/>} />
        </Routes>
      </div>
    </div>
  );
}

export default App;
