import api from './client'
import type { PersonTotalsResponse } from '../types/person'
import type { CategoryTotalsResponse } from '../types/category'

export const getPersonTotals   = () => api.get<PersonTotalsResponse>('/persons/totals')
export const getCategoryTotals = () => api.get<CategoryTotalsResponse>('/categories/totals')
