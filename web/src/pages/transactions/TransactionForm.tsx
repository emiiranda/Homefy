import { useState, useEffect } from "react";
import { TransactionType } from "../../types/transaction.ts";
import type { CreateTransactionRequest } from "../../types/transaction.ts";
import type { Person } from "../../types/person.ts";
import type { Category } from "../../types/category.ts";
import { getPersons } from "../../api/persons.ts";
import { getCategories } from "../../api/categories.ts";

interface TransactionFormProps {
  onSubmit: (data: CreateTransactionRequest) => Promise<void>;
  onCancel: () => void;
}

export default function TransactionForm({
  onSubmit,
  onCancel,
}: TransactionFormProps) {
  const [description, setDescription] = useState("");
  const [amount, setAmount] = useState<number>(0);
  const [type, setType] = useState<TransactionType>(TransactionType.Expense);
  const [personId, setPersonId] = useState("");
  const [categoryId, setCategoryId] = useState("");

  const [persons, setPersons] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loadingData, setLoadingData] = useState(true);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadSelects() {
      try {
        const [personsRes, categoriesRes] = await Promise.all([
          getPersons(),
          getCategories(),
        ]);
        setPersons(personsRes.data);
        setCategories(categoriesRes.data);

        if (personsRes.data.length > 0) setPersonId(personsRes.data[0].id);
        if (categoriesRes.data.length > 0)
          setCategoryId(categoriesRes.data[0].id);
      } catch {
        setError("Erro ao carregar pessoas e categorias.");
      } finally {
        setLoadingData(false);
      }
    }
    loadSelects();
  }, []);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!description.trim()) {
      setError("Descrição é obrigatória.");
      return;
    }
    if (amount <= 0) {
      setError("Valor deve ser maior que zero.");
      return;
    }
    if (!personId) {
      setError("Selecione uma pessoa.");
      return;
    }
    if (!categoryId) {
      setError("Selecione uma categoria.");
      return;
    }
    setLoading(true);
    try {
      await onSubmit({
        description: description.trim(),
        amount,
        type,
        personId,
        categoryId,
      });
    } catch {
      setError("Erro ao salvar. Tente novamente.");
    } finally {
      setLoading(false);
    }
  }

  if (loadingData) {
    return (
      <p className="text-sm text-gray-400 py-4 text-center">
        Carregando dados...
      </p>
    );
  }

  return (
    <form onSubmit={handleSubmit} className="flex flex-col gap-4">
      {error && (
        <p className="text-sm text-red-600 bg-red-50 border border-red-200 rounded-md px-3 py-2">
          {error}
        </p>
      )}

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Descrição</label>
        <input
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Ex: Supermercado"
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700"
        />
      </div>

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Valor (R$)</label>
        <input
          type="number"
          value={amount}
          min={0.01}
          step={0.01}
          onChange={(e) => setAmount(Number(e.target.value))}
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700"
        />
      </div>

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Tipo</label>
        <select
          value={type}
          onChange={(e) => setType(Number(e.target.value) as TransactionType)}
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700 bg-white"
        >
          <option value={TransactionType.Expense}>Despesa</option>
          <option value={TransactionType.Income}>Receita</option>
        </select>
      </div>

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Pessoa</label>
        <select
          value={personId}
          onChange={(e) => setPersonId(e.target.value)}
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700 bg-white"
        >
          {persons.map((p) => (
            <option key={p.id} value={p.id}>
              {p.name}
            </option>
          ))}
        </select>
      </div>

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Categoria</label>
        <select
          value={categoryId}
          onChange={(e) => setCategoryId(e.target.value)}
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700 bg-white"
        >
          {categories.map((c) => (
            <option key={c.id} value={c.id}>
              {c.description}
            </option>
          ))}
        </select>
      </div>

      <div className="flex justify-end gap-2 pt-2">
        <button
          type="button"
          onClick={onCancel}
          disabled={loading}
          className="px-4 py-2 text-sm rounded-md border border-gray-300 text-gray-600 hover:bg-gray-50 transition-colors"
        >
          Cancelar
        </button>
        <button
          type="submit"
          disabled={loading}
          className="px-4 py-2 text-sm rounded-md bg-green-800 text-white hover:bg-green-700 transition-colors disabled:opacity-50"
        >
          {loading ? "Salvando..." : "Criar transação"}
        </button>
      </div>
    </form>
  );
}
