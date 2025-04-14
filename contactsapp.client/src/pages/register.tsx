import { useState } from "react";
import { useAuth } from "../providers/authProvider";
import { redirect } from "react-router-dom";

const RegisterPage = () => {
  const { register } = useAuth();
  const [form, setForm] = useState({ username: "", email: "", password: "" });

  if (useAuth().isAuthenticated()) {
    redirect("/login");
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await register(form);
      alert("Registered! Now you can login.");
    } catch (err) {
      alert("Register failed");
    }
  };

  return (
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
      <button type="submit">Register</button>
    </form>
  );
};

export default RegisterPage;
