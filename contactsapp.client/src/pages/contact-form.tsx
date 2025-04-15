import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAuth } from "../providers/auth-provider";
import { Contact } from "../types/contacts";
import { createContact } from "../api/contacts";

const CATEGORIES = ["business", "private", "other"];
const BUSINESS_SUBCATEGORIES = ["boss", "client", "colleague"];

const ContactForm = () => {
  const navigate = useNavigate();
  const { isAuthenticated, user } = useAuth();

  if (!isAuthenticated()) {
    navigate("/login");
    return null;
  }

  const [formData, setFormData] = useState<Contact>({
    id: 0,
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    address: "",
    dateOfBirth: "",
    notes: "",
    userId: "",
    category: "",
    subcategory: "",
  });

  const [error, setError] = useState("");

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));

    // Reset subcategory when category changes
    if (name === "category") {
      setFormData((prev) => ({ ...prev, subcategory: "" }));
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    try {
      const contactData = {
        ...formData,
        userId: "", // user?.userId
        dateOfBirth: new Date(formData.dateOfBirth).toISOString(),
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
        <label htmlFor="phoneNumber">Phone Number *</label>
        <input
          type="tel"
          id="phoneNumber"
          name="phoneNumber"
          value={formData.phoneNumber}
          onChange={handleChange}
          required
        />

        <label htmlFor="address">Address</label>
        <input
          type="text"
          id="address"
          name="address"
          value={formData.address}
          onChange={handleChange}
        />

        <label htmlFor="dateOfBirth">Date of Birth</label>
        <input
          type="date"
          id="dateOfBirth"
          name="dateOfBirth"
          value={formData.dateOfBirth}
          onChange={handleChange}
        />

        <label htmlFor="notes">Notes</label>
        <textarea
          id="notes"
          name="notes"
          value={formData.notes}
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
