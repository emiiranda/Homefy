import { useState } from "react";
import { CategoryPurpose } from "../../types/category.ts";
import type { CreateCategoryRequest } from "../../types/category.ts";

interface CategoryFormProps {
  onSubmit: (data: CreateCategoryRequest) => Promise<void>;
  onCancel: () => void;
}

export default function CategoryForm({
  onSubmit,
  onCancel,
}: CategoryFormProps) {
  const [description, setDescription] = useState("");
  const [purpose, setPurpose] = useState<CategoryPurpose>(
    CategoryPurpose.Expense,
  );
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!description.trim()) {
      setError("Descrição é obrigatória.");
      return;
    }
    setLoading(true);
    try {
      await onSubmit({ description: description.trim(), purpose });
    } catch {
      setError("Erro ao salvar. Tente novamente.");
    } finally {
      setLoading(false);
    }
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
          placeholder="Ex: Alimentação"
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700"
        />
      </div>

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Finalidade</label>
        <select
          value={purpose}
          onChange={(e) =>
            setPurpose(Number(e.target.value) as CategoryPurpose)
          }
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700 bg-white"
        >
          <option value={CategoryPurpose.Expense}>Despesa</option>
          <option value={CategoryPurpose.Income}>Receita</option>
          <option value={CategoryPurpose.Both}>Ambos</option>
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
          {loading ? "Salvando..." : "Criar categoria"}
        </button>
      </div>
    </form>
  );
}
