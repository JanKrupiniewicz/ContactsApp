import { apiClient } from "./api-client";
import {
  ContactsDetailed,
  ContactList,
  CreateContact,
} from "../types/contacts";

export const getAllContacts = async (): Promise<ContactList[]> => {
  const response = await apiClient.get("/Contacts");
  return response.data;
};

export const getContactById = async (id: number): Promise<ContactsDetailed> => {
  const response = await apiClient.get(`/Contacts/${id}`);
  return response.data;
};

export const createContact = async (
  contact: CreateContact
): Promise<ContactsDetailed> => {
  const response = await apiClient.post("/Contacts", contact);
  return response.data;
};

export const updateContact = async (
  contact: ContactsDetailed
): Promise<ContactsDetailed> => {
  console.log(contact);

  const response = await apiClient.put(`/Contacts/${contact.id}`, contact);

  console.log(response);

  return response.data;
};

export const deleteContact = async (id: number): Promise<void> => {
  await apiClient.delete(`/Contacts/${id}`);
};
