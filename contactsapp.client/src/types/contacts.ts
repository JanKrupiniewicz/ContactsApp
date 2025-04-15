export interface ContactsDetailed {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
  dateOfBirth?: string;
  category: string;
  subcategory?: string;
  userId: number;
}

export interface CreateContact {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  phoneNumber: string;
  dateOfBirth?: string;
  category: string;
  subcategory?: string;
  userId: number;
}

export interface ContactList {
  id: number;
  firstName: string;
  lastName: string;
}
