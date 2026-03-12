import { useState, useEffect } from "react";
import type { Person, CreatePersonRequest } from "../../types/person.ts";

interface PersonFormProps {
  initial?: Person;
  onSubmit: (data: CreatePersonRequest) => Promise<void>;
  onCancel: () => void;
}

export default function PersonForm({
  initial,
  onSubmit,
  onCancel,
}: PersonFormProps) {
  const [name, setName] = useState(initial?.name ?? "");
  const [age, setAge] = useState(initial?.age ?? 0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setName(initial?.name ?? "");
    setAge(initial?.age ?? 0);
    setError(null);
  }, [initial]);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    if (!name.trim()) {
      setError("Nome é obrigatório.");
      return;
    }
    if (age < 1 || age > 120) {
      setError("Idade deve estar entre 1 e 120.");
      return;
    }
    setLoading(true);
    try {
      await onSubmit({ name: name.trim(), age });
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
        <label className="text-sm font-medium text-gray-700">Nome</label>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Ex: Eduardo Miranda"
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700"
        />
      </div>

      <div className="flex flex-col gap-1">
        <label className="text-sm font-medium text-gray-700">Idade</label>
        <input
          type="number"
          value={age}
          min={1}
          max={120}
          onChange={(e) => setAge(Number(e.target.value))}
          className="border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-green-700"
        />
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
          {loading
            ? "Salvando..."
            : initial
              ? "Salvar alterações"
              : "Criar pessoa"}
        </button>
      </div>
    </form>
  );
}
