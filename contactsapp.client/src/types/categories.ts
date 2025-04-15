export interface Subcategory {
  id: number;
  name: string;
  categoryId: number;
}

export interface Category {
  id: number;
  name: string;
  subcategories?: Subcategory[];
}
