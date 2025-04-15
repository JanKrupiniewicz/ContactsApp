import { apiClient } from "./api-client";
import { Category } from "../types/categories";

export const getAllCategories = async (): Promise<Category[]> => {
  const response = await apiClient.get("/Categories");
  return response.data;
};
