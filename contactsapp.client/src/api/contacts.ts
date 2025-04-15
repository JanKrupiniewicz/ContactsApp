import { apiClient } from "./api-client";
import { Contact, ContactListItem } from "../types/contacts";

export const getAllContacts = async (): Promise<ContactListItem[]> => {
  const response = await apiClient.get("/Contacts");
  return response.data;
};

export const getContactById = async (id: number): Promise<Contact> => {
  const response = await apiClient.get(`/Contacts/${id}`);
  return response.data;
};

export const createContact = async (contact: Contact): Promise<Contact> => {
  const response = await apiClient.post("/Contacts", contact);
  return response.data;
};

export const updateContact = async (contact: Contact): Promise<Contact> => {
  const response = await apiClient.put(`/Contacts/${contact.id}`, contact);
  return response.data;
};

export const deleteContact = async (id: number): Promise<void> => {
  await apiClient.delete(`/Contacts/${id}`);
};
