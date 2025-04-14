import { useState } from "react";
import { useAuth } from "../providers/authProvider";
import { apiClient } from "../lib/apiClient";

const LoginPage = () => {
  const { login } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await apiClient.post("/Auth/login", {
        email,
        password,
      });

      console.log("Login response:", response.data); // Log the response data

      const { token, refreshToken } = response.data;

      // Store the tokens in localStorage or secure cookie for later use
      localStorage.setItem("token", token);
      localStorage.setItem("refreshToken", refreshToken);

      alert("Logged in!");
    } catch (err) {
      alert("Login failed");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        required
      />
      <input
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        required
      />
      <button type="submit">Login</button>
    </form>
  );
};

export default LoginPage;
