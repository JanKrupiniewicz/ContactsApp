import { useState, useEffect } from "react";
import { useNavigate, Link, useParams } from "react-router-dom";
import { useAuth } from "../providers/auth-provider";
import { ContactsDetailed } from "../types/contacts";
import { getContactById, updateContact } from "../api/contacts";
import { CATEGORIES, BUSINESS_SUBCATEGORIES } from "../constants/categories";

const ContactsEditForm = () => {
  const { id } = useParams<{ id: string }>();
  const { isAuthenticated, user } = useAuth();
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  // Store the original contact data in state
  const [originalContact, setOriginalContact] =
    useState<ContactsDetailed | null>(null);

  const [formData, setFormData] = useState<ContactsDetailed>({
    id: 0,
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

  useEffect(() => {
    if (!isAuthenticated) {
      navigate("/login");
      return;
    }

    if (!id) {
      navigate("/contacts");
      return;
    }

    const fetchContact = async () => {
      try {
        const contactData = await getContactById(Number.parseInt(id));
        setOriginalContact(contactData);

        // Format date to YYYY-MM-DD for input
        const formattedDate = contactData.dateOfBirth
          ? new Date(contactData.dateOfBirth).toISOString().split("T")[0]
          : "";

        setFormData({
          ...contactData,
          dateOfBirth: formattedDate,
          password: "", // Don't show the password in edit mode
        });
        setLoading(false);
      } catch (err) {
        setError("Failed to load contact");
        setLoading(false);
      }
    };

    fetchContact();
  }, [id, isAuthenticated, navigate, user]);

  const validatePassword = (password: string) => {
    if (!password) return true; // Allow empty password in edit mode

    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
    return regex.test(password);
  };

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    const { name, value } = e.target;

    if (name === "password" && value) {
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

    if (formData.password && !validatePassword(formData.password)) {
      setPasswordError(
        "Password must be at least 8 characters long and include uppercase, lowercase, and numbers"
      );
      return;
    }

    try {
      if (!id || !originalContact) return;

      const contactData: ContactsDetailed = {
        ...formData,
        id: Number.parseInt(id),
        userId: user?.userId || 0,
        dateOfBirth: formData.dateOfBirth
          ? new Date(formData.dateOfBirth).toISOString()
          : originalContact.dateOfBirth,
        password: formData.password || originalContact.password,
      };

      await updateContact(contactData);
      navigate("/contacts");
    } catch (err) {
      setError("Failed to update contact");
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1>Contact Form</h1>
      <p>
        Create a new contact. Or {}
        <Link to="/contacts">Go Back to Contacts</Link>
      </p>

      {error && (
        <div style={{ color: "red", marginBottom: "15px" }}>{error}</div>
      )}
      <form onSubmit={handleSubmit}>
        <label htmlFor="firstName">First Name</label>
        <input
          type="text"
          id="firstName"
          name="firstName"
          value={formData.firstName}
          onChange={handleChange}
          required
        />

        <label htmlFor="lastName">Last Name</label>
        <input
          type="text"
          id="lastName"
          name="lastName"
          value={formData.lastName}
          onChange={handleChange}
          required
        />

        <label htmlFor="email">Email</label>
        <input
          type="email"
          id="email"
          name="email"
          value={formData.email}
          onChange={handleChange}
          required
        />

        <label htmlFor="password">Password</label>
        <input
          type="password"
          id="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
        />

        {passwordError && (
          <div style={{ color: "red", fontSize: "14px", marginTop: "5px" }}>
            {passwordError}
          </div>
        )}

        <label htmlFor="phoneNumber">Phone Number</label>
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

        <label htmlFor="category">Category</label>
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
            <label htmlFor="subcategory">Subcategory</label>
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
            <label htmlFor="subcategory">Subcategory</label>
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

export default ContactsEditForm;
