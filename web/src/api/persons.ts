import api from './client'
import type { Person, CreatePersonRequest, UpdatePersonRequest } from '../types/person'

export const getPersons    = ()                              => api.get<Person[]>('/persons')
export const getPersonById = (id: string)                   => api.get<Person>(`/persons/${id}`)
export const createPerson  = (data: CreatePersonRequest)    => api.post<Person>('/persons', data)
export const updatePerson  = (id: string, data: UpdatePersonRequest) => api.put<Person>(`/persons/${id}`, data)
export const deletePerson  = (id: string)                   => api.delete(`/persons/${id}`)
