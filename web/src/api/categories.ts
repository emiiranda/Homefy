import api from './client'
import type { Category, CreateCategoryRequest } from '../types/category'

export const getCategories   = ()                           => api.get<Category[]>('/categories')
export const createCategory  = (data: CreateCategoryRequest) => api.post<Category>('/categories', data)
