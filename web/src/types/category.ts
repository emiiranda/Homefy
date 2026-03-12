export const CategoryPurpose = {
  Expense: 1,
  Income:  2,
  Both:    3,
} as const

export type CategoryPurpose = typeof CategoryPurpose[keyof typeof CategoryPurpose]

export interface Category {
  id: string
  description: string
  purpose: CategoryPurpose
}

export interface CreateCategoryRequest {
  description: string
  purpose: CategoryPurpose
}

export interface CategoryTotalsItem {
  id: string
  description: string
  purpose: CategoryPurpose
  totalIncome: number
  totalExpense: number
  balance: number
}

export interface CategoryTotalsResponse {
  categories: CategoryTotalsItem[]
  totalIncome: number
  totalExpense: number
  balance: number
}
