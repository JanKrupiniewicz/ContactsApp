import { useState } from "react";
import { useAuth } from "../providers/auth-provider";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { Link } from "react-router-dom";
import { validatePassword } from "../utils/validation";

const RegisterPage = () => {
  const { register } = useAuth();
  const [form, setForm] = useState({ username: "", email: "", password: "" });
  const [passwordError, setPasswordError] = useState("");
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated()) {
      navigate("/");
      return;
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validatePassword(form.password)) {
      setPasswordError(
        "Password must be at least 8 characters long and include uppercase, lowercase, and numbers"
      );
      return;
    }

    try {
      await register(form);
      alert("Registered! Now you can login.");
    } catch (err) {
      alert("Register failed");
    }
  };

  return (
    <div>
      <h1>Register</h1>
      <p>
        Already have an account? <Link to="/login">Login</Link>
      </p>
      <form onSubmit={handleSubmit}>
        <input
          placeholder="Username"
          value={form.username}
          onChange={(e) => setForm({ ...form, username: e.target.value })}
          required
        />
        <input
          placeholder="Email"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
          required
        />
        <input
          type="password"
          placeholder="Password"
          value={form.password}
          onChange={(e) => setForm({ ...form, password: e.target.value })}
          required
        />
        {passwordError && (
          <div style={{ color: "red", fontSize: "14px", marginTop: "5px" }}>
            {passwordError}
          </div>
        )}
        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default RegisterPage;
