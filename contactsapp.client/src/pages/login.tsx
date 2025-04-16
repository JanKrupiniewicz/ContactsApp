import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAuth } from "../providers/auth-provider";
import { validatePassword } from "../utils/validation";

const LoginPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const { isAuthenticated, login } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated()) {
      navigate("/contacts");
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validatePassword(password)) {
      setPasswordError(
        "Password must be at least 8 characters long and include uppercase, lowercase, and numbers"
      );
      return;
    }

    try {
      await login({ email, password });
      navigate("/contacts");
    } catch (err) {
      alert("Login failed");
      console.error(err);
    }
  };

  return (
    <div>
      <h1>Login</h1>
      <p>
        Don't have an account? <Link to="/register">Register</Link>
      </p>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        {passwordError && (
          <p style={{ color: "red", fontSize: "14px", marginTop: "5px" }}>
            {passwordError}
          </p>
        )}
        <button type="submit">Login</button>
      </form>
    </div>
  );
};

export default LoginPage;
