import { useState, useEffect, use } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useAuth } from "../providers/auth-provider";
import { CreateContact } from "../types/contacts";
import { createContact } from "../api/contacts";
import { validatePassword } from "../utils/validation";
import { Category } from "../types/categories";
import CategorySelector from "../components/category-selector";
import { getAllCategories } from "../api/categories";

const ContactForm = () => {
  const navigate = useNavigate();
  const { isAuthenticated, user } = useAuth();
  const [error, setError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [categories, setCategories] = useState<Category[]>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!isAuthenticated) {
      navigate("/login");
      return;
    }

    const fetchCategories = async () => {
      try {
        const data = await getAllCategories();
        console.log("Fetched categories:", data);

        setCategories(data);
        setLoading(false);
      } catch (error) {
        console.error("Failed to fetch categories", error);
        setLoading(false);
      }
    };

    fetchCategories();
  }, [isAuthenticated, navigate]);

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
        userId: user?.userId || 0,
        dateOfBirth: formData.dateOfBirth
          ? new Date(formData.dateOfBirth).toISOString()
          : undefined,
      };

      console.log("User: ", user);
      console.log("Contact data to be created:", contactData);

      await createContact(contactData);
      navigate("/contacts");
    } catch (err) {
      setError("Failed to create contact");
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

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

        <CategorySelector
          categories={categories}
          selectedCategory={formData.category}
          selectedSubcategory={formData.subcategory}
          onCategoryChange={handleChange}
          onSubcategoryChange={handleChange}
        />
        <Link to="/contacts">Cancel</Link>
        <button type="submit">Save Contact</button>
      </form>
    </div>
  );
};

export default ContactForm;
