"use client";

import { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../providers/auth-provider";
import { getAllContacts } from "../api/contacts";
import { ContactListItem } from "../types/contacts";

const ContactsPage = () => {
  const [contacts, setContacts] = useState<ContactListItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated() === false) {
      navigate("/login");
      return;
    }

    const fetchContacts = async () => {
      try {
        const data = await getAllContacts();
        setContacts(data);
        setLoading(false);
      } catch (err) {
        setError("Failed to load contacts");
        setLoading(false);
      }
    };

    fetchContacts();
  }, [isAuthenticated, navigate]);

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <div>
        <h1>Contacts</h1>
        <div>
          <Link to="/contacts/add">Add Contact</Link>
          <button onClick={logout}>Logout</button>
        </div>
      </div>

      {error && <div>{error}</div>}

      {contacts.length === 0 ? (
        <p>No contacts found. Add a new contact to get started.</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {contacts.map((contact) => (
              <tr key={contact.id}>
                <td>{contact.firstName}</td>
                <td>{contact.lastName}</td>
                <td>
                  <Link to={`/contacts/${contact.id}`}>View</Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default ContactsPage;
