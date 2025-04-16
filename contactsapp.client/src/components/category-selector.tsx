import { useState, useEffect } from "react";
import { Category } from "../types/categories";

interface CategorySelectorProps {
  categories: Category[] | undefined;
  selectedCategory: string;
  selectedSubcategory: string | undefined;
  onCategoryChange: (e: React.ChangeEvent<HTMLSelectElement>) => void;
  onSubcategoryChange: (
    e: React.ChangeEvent<HTMLSelectElement | HTMLInputElement>
  ) => void;
}

const CategorySelector = ({
  categories,
  selectedCategory,
  selectedSubcategory,
  onCategoryChange,
  onSubcategoryChange,
}: CategorySelectorProps) => {
  // Find the current selected category object
  const currentCategory = categories?.find(
    (category) => category.name === selectedCategory
  );

  // Check if the current category has subcategories
  const hasSubcategories =
    currentCategory?.subcategories &&
    currentCategory.subcategories.length > 0 &&
    currentCategory.name !== "Other";

  return (
    <>
      {categories && (
        <>
          <label htmlFor="category">Category *</label>
          <select
            id="category"
            name="category"
            value={selectedCategory}
            onChange={onCategoryChange}
            required
          >
            <option value="">Select a category</option>
            {categories.map((category) => (
              <option key={category.id} value={category.name}>
                {category.name.charAt(0).toUpperCase() + category.name.slice(1)}
              </option>
            ))}
          </select>
        </>
      )}

      {/* Show subcategory select if the selected category has subcategories */}
      {selectedCategory && hasSubcategories && (
        <>
          <label htmlFor="subcategory">Subcategory *</label>
          <select
            id="subcategory"
            name="subcategory"
            value={selectedSubcategory}
            onChange={onSubcategoryChange}
            required
          >
            <option value="">Select a subcategory</option>
            {currentCategory?.subcategories?.map((subcategory) => (
              <option key={subcategory.id} value={subcategory.name}>
                {subcategory.name.charAt(0).toUpperCase() +
                  subcategory.name.slice(1)}
              </option>
            ))}
          </select>
        </>
      )}

      {/* Show subcategory input if the selected category is "other" */}
      {selectedCategory === "Other" && (
        <>
          <label htmlFor="subcategory">Subcategory *</label>
          <input
            type="text"
            id="subcategory"
            name="subcategory"
            value={selectedSubcategory}
            onChange={onSubcategoryChange}
            required
          />
        </>
      )}
    </>
  );
};

export default CategorySelector;
