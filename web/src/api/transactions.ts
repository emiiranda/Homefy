import api from './client'
import type { Transaction, CreateTransactionRequest } from '../types/transaction'

export const getTransactions   = ()                              => api.get<Transaction[]>('/transactions')
export const createTransaction = (data: CreateTransactionRequest) => api.post<Transaction>('/transactions', data)
