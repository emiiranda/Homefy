export interface Person {
  id: string
  name: string
  age: number
}

export interface CreatePersonRequest {
  name: string
  age: number
}

export interface UpdatePersonRequest {
  name: string
  age: number
}

export interface PersonTotalsItem {
  id: string
  name: string
  age: number
  totalIncome: number
  totalExpense: number
  balance: number
}

export interface PersonTotalsResponse {
  persons: PersonTotalsItem[]
  totalIncome: number
  totalExpense: number
  balance: number
}
