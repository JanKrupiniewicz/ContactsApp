import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAuth } from "../providers/auth-provider";
import { CreateContact } from "../types/contacts";
import { createContact } from "../api/contacts";
import { CATEGORIES, BUSINESS_SUBCATEGORIES } from "../constants/categories";
import { validatePassword } from "../utils/validation";

const ContactForm = () => {
  const navigate = useNavigate();
  const { isAuthenticated, user } = useAuth();

  if (!isAuthenticated()) {
    navigate("/login");
    return null;
  }

  const [formData, setFormData] = useState<CreateContact>({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    phoneNumber: "",
    dateOfBirth: "",
    category: "private",
    subcategory: "",
    userId: user?.userId || 0,
  });

  const [error, setError] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    const { name, value } = e.target;

    if (name === "password") {
      if (!validatePassword(value)) {
        setPasswordError(
          "Password must be at least 8 characters long and include uppercase, lowercase, and numbers"
        );
      } else {
        setPasswordError("");
      }
    }

    setFormData((prev) => ({ ...prev, [name]: value }));

    // Reset subcategory when category changes
    if (name === "category") {
      setFormData((prev) => ({ ...prev, subcategory: "" }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    if (!validatePassword(formData.password)) {
      setPasswordError(
        "Password must be at least 8 characters long and include uppercase, lowercase, and numbers"
      );
      return;
    }

    try {
      const contactData = {
        ...formData,
        userId: 0,
        dateOfBirth: formData.dateOfBirth
          ? new Date(formData.dateOfBirth).toISOString()
          : undefined,
      };

      await createContact(contactData);
      navigate("/contacts");
    } catch (err) {
      setError("Failed to create contact");
    }
  };

  return (
    <div>
      <h1>Contact Form</h1>
      <p>
        Create a new contact. All fields marked with * are required. Or {}
        <Link to="/contacts">Go Back to Contacts</Link>
      </p>

      {error && (
        <div style={{ color: "red", marginBottom: "15px" }}>{error}</div>
      )}
      <form onSubmit={handleSubmit}>
        <label htmlFor="firstName">First Name *</label>
        <input
          type="text"
          id="firstName"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
          required
        />

        <label htmlFor="lastName">Last Name *</label>
        <input
          type="text"
          id="lastName"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
          required
        />

        <label htmlFor="email">Email *</label>
        <input
          type="email"
          id="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          required
        />

        <label htmlFor="password">Password *</label>
        <input
          type="password"
          id="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
          required
        />

        {passwordError && (
          <div style={{ color: "red", fontSize: "14px", marginTop: "5px" }}>
            {passwordError}
          </div>
        )}

        <label htmlFor="phoneNumber">Phone Number *</label>
        <input
          type="tel"
          id="phoneNumber"
          name="phoneNumber"
          value={formData.phoneNumber}
          onChange={handleChange}
          required
        />

        <label htmlFor="dateOfBirth">Date of Birth</label>
        <input
          type="date"
          id="dateOfBirth"
          name="dateOfBirth"
          value={formData.dateOfBirth}
          onChange={handleChange}
        />

        <label htmlFor="category">Category *</label>
        <select
          id="category"
          name="category"
          value={formData.category}
          onChange={handleChange}
          required
        >
          {CATEGORIES.map((category) => (
            <option key={category} value={category}>
              {category.charAt(0).toUpperCase() + category.slice(1)}
            </option>
          ))}
        </select>

        {formData.category === "business" && (
          <>
            <label htmlFor="subcategory">Subcategory *</label>
            <select
              id="subcategory"
              name="subcategory"
              value={formData.subcategory}
              onChange={handleChange}
              required
            >
              <option value="">Select a subcategory</option>
              {BUSINESS_SUBCATEGORIES.map((subcategory) => (
                <option key={subcategory} value={subcategory}>
                  {subcategory.charAt(0).toUpperCase() + subcategory.slice(1)}
                </option>
              ))}
            </select>
          </>
        )}

        {formData.category === "other" && (
          <>
            <label htmlFor="subcategory">Subcategory *</label>
            <input
              type="text"
              id="subcategory"
              name="subcategory"
              value={formData.subcategory}
              onChange={handleChange}
              required
            />
          </>
        )}
        <Link to="/contacts">Cancel</Link>
        <button type="submit">Save Contact</button>
      </form>
    </div>
  );
};

export default ContactForm;
