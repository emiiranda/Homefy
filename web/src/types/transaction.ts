export const TransactionType = {
  Expense: 1,
  Income:  2,
} as const

export type TransactionType = typeof TransactionType[keyof typeof TransactionType]

export interface Transaction {
  id: string
  description: string
  amount: number
  type: TransactionType
  categoryId: string
  categoryDescription: string
  personId: string
  personName: string
}

export interface CreateTransactionRequest {
  description: string
  amount: number
  type: TransactionType
  categoryId: string
  personId: string
}
