export interface ContactsDetailed {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  DateOfBirth: string;
  userId: number;
  category: string;
  subcategory?: string;
}

export interface CreateContact {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  DateOfBirth: string;
  userId: number;
  category: string;
  subcategory?: string;
}

export interface ContactList {
  id: number;
  firstName: string;
  lastName: string;
}
