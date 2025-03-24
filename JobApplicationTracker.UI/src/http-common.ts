import axios from "axios";

export default axios.create({
  baseURL: "http://localhost:53320/api",
  headers: {
    "Content-type": "application/json"
  }
});