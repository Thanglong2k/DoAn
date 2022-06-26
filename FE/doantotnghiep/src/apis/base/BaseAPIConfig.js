import APIConfig from "../config/ApiConfig";
import axios from "axios";
var token=localStorage.getItem('access_token');
var BaseAPIConfig = axios.create({
  baseURL: APIConfig,
  headers: { "Content-type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Authorization":`Bearer ${token}`
          },
});

export default BaseAPIConfig;
