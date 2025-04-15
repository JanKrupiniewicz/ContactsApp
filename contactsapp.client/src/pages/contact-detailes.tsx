import { useState, useEffect } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import { getContactById, deleteContact } from "../api/contacts";
import { useAuth } from "../providers/auth-provider";
import { ContactsDetailed } from "../types/contacts";

const ContactDetailsPage = () => {
  const { id } = useParams<{ id: string }>();
  const [contact, setContact] = useState<ContactsDetailed | null>(null);
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

        console.log("Fetched contact:", data);

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
    <div>
      <h1>Contact Details</h1>
      <Link to="/contacts">Back to Contacts</Link>
      <button onClick={handleDelete}>Delete</button>
      <Link to={`/contacts/edit/${id}`}>Edit</Link>

      <div>
        <div>
          <strong>Name:</strong> {contact.firstName} {contact.lastName}
        </div>
        <div>
          <strong>Email:</strong> {contact.email}
        </div>
        <div>
          <strong>Phone:</strong> {contact.phoneNumber}
        </div>
        {contact.dateOfBirth && (
          <div>
            <strong>Date of Birth:</strong>{" "}
            {new Date(contact.dateOfBirth).toLocaleDateString()}
          </div>
        )}
        <div>
          <strong>Category:</strong> {contact.category}
        </div>
        {contact.subcategory && (
          <div>
            <strong>Subcategory:</strong> {contact.subcategory}
          </div>
        )}
      </div>
    </div>
  );
};

export default ContactDetailsPage;
