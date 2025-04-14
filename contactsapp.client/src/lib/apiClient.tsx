import axios from "axios";

const BACKEND_URL =
  import.meta.env.BACKEND_URL || "https://localhost:7091/api/";

export const apiClient = axios.create({
  baseURL: BACKEND_URL,
  timeout: !import.meta.env.REQUEST_TIMEOUT
    ? 10000
    : parseInt(import.meta.env.REQUEST_TIMEOUT),
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
});

apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("accessToken");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`; // Add token to headers
    }
    return config;
  },
  (error) => Promise.reject(error)
);

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      const refreshToken = localStorage.getItem("refreshToken");

      if (refreshToken) {
        try {
          const response = await axios.post(`${BACKEND_URL}/refreshToken`, {
            refreshToken,
          });
          // don't use axious instance that already configured for refresh token api call
          const newAccessToken = response.data.accessToken;
          localStorage.setItem("accessToken", newAccessToken); //set new access token
          originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
          return axios(originalRequest); //recall Api with new token
        } catch (error) {
          // Handle token refresh failure
          // mostly logout the user and re-authenticate by login again
          console.error("Token refresh failed:", error);
          localStorage.removeItem("accessToken");
          localStorage.removeItem("refreshToken");
          window.location.href = "/login"; // Redirect to login page
        }
      }
    }
    return Promise.reject(error);
  }
);
