export interface Contact {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  address: string;
  dateOfBirth: string;
  notes: string;
  userId: string;
  category: string;
  subcategory: string;
}

export interface ContactListItem {
  id: number;
  firstName: string;
  lastName: string;
}
