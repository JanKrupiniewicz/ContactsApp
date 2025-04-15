import { useState, useEffect } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import { getContactById, deleteContact } from "../api/contacts";
import { useAuth } from "../providers/auth-provider";
import { Contact } from "../types/contacts";

const ContactDetailsPage = () => {
  const { id } = useParams<{ id: string }>();
  const [contact, setContact] = useState<Contact | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated) {
      navigate("/login");
      return;
    }

    const fetchContact = async () => {
      if (!id) return;

      try {
        const data = await getContactById(Number.parseInt(id));
        setContact(data);
        setLoading(false);
      } catch (err) {
        setError("Failed to load contact details");
        setLoading(false);
      }
    };

    fetchContact();
  }, [id, isAuthenticated, navigate]);

  const handleDelete = async () => {
    if (!id || !window.confirm("Are you sure you want to delete this contact?"))
      return;

    try {
      await deleteContact(Number.parseInt(id));
      navigate("/contacts");
    } catch (err) {
      setError("Failed to delete contact");
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div style={{ color: "red" }}>{error}</div>;
  }

  if (!contact) {
    return <div>Contact not found</div>;
  }

  return (
    <div style={{ maxWidth: "800px", margin: "0 auto", padding: "20px" }}>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          marginBottom: "20px",
        }}
      >
        <h1>Contact Details</h1>
        <div>
          <Link
            to="/contacts"
            style={{
              backgroundColor: "#607D8B",
              color: "white",
              padding: "8px 15px",
              textDecoration: "none",
              marginRight: "10px",
            }}
          >
            Back to Contacts
          </Link>
          <Link
            to={`/contacts/edit/${id}`}
            style={{
              backgroundColor: "#FF9800",
              color: "white",
              padding: "8px 15px",
              textDecoration: "none",
              marginRight: "10px",
            }}
          >
            Edit
          </Link>
          <button
            onClick={handleDelete}
            style={{
              backgroundColor: "#f44336",
              color: "white",
              padding: "8px 15px",
              border: "none",
              cursor: "pointer",
            }}
          >
            Delete
          </button>
        </div>
      </div>

      <div
        style={{
          border: "1px solid #ddd",
          borderRadius: "5px",
          padding: "20px",
        }}
      >
        <div style={{ marginBottom: "15px" }}>
          <strong>Name:</strong> {contact.firstName} {contact.lastName}
        </div>
        <div style={{ marginBottom: "15px" }}>
          <strong>Email:</strong> {contact.email}
        </div>
        <div style={{ marginBottom: "15px" }}>
          <strong>Phone:</strong> {contact.phoneNumber}
        </div>
        <div style={{ marginBottom: "15px" }}>
          <strong>Address:</strong> {contact.address}
        </div>
        <div style={{ marginBottom: "15px" }}>
          <strong>Date of Birth:</strong>{" "}
          {new Date(contact.dateOfBirth).toLocaleDateString()}
        </div>
        <div style={{ marginBottom: "15px" }}>
          <strong>Category:</strong> {contact.category}
        </div>
        {contact.subcategory && (
          <div style={{ marginBottom: "15px" }}>
            <strong>Subcategory:</strong> {contact.subcategory}
          </div>
        )}
        {contact.notes && (
          <div style={{ marginBottom: "15px" }}>
            <strong>Notes:</strong> {contact.notes}
          </div>
        )}
      </div>
    </div>
  );
};

export default ContactDetailsPage;
