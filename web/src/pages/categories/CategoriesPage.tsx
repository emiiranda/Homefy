import { useEffect, useState, useCallback } from "react";
import type { Category, CreateCategoryRequest } from "../../types/category.ts";
import { getCategories, createCategory } from "../../api/categories.ts";
import Table, { type Column } from "../../components/Table.tsx";
import Modal from "../../components/Modal.tsx";
import Badge from "../../components/Badge.tsx";
import CategoryForm from "./CategoryForm.tsx";

export default function CategoriesPage() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [modalOpen, setModalOpen] = useState(false);

  const fetchCategories = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const { data } = await getCategories();
      setCategories(data);
    } catch {
      setError("Erro ao carregar categorias. Verifique a conexão com a API.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  async function handleCreate(data: CreateCategoryRequest) {
    await createCategory(data);
    await fetchCategories();
    setModalOpen(false);
  }

  const columns: Column<Category>[] = [
    { header: "Descrição", accessor: "description" },
    {
      header: "Finalidade",
      className: "w-36",
      accessor: (row) => (
        <Badge variant="categoryPurpose" value={row.purpose} />
      ),
    },
  ];

  return (
    <div className="flex flex-col gap-6">
      {/* Cabeçalho */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-800">Categorias</h1>
          <p className="text-sm text-gray-500 mt-0.5">
            Gerencie aqui as categorias das transações 
          </p>
        </div>
        <button
          onClick={() => setModalOpen(true)}
          className="px-4 py-2 text-sm rounded-md bg-green-800 text-white hover:bg-green-700 transition-colors"
        >
          + Nova categoria
        </button>
      </div>

      {/* Erro de carregamento */}
      {error && (
        <p className="text-sm text-red-600 bg-red-50 border border-red-200 rounded-md px-4 py-3">
          {error}
        </p>
      )}

      {/* Tabela */}
      {loading ? (
        <p className="text-sm text-gray-400">Carregando...</p>
      ) : (
        <Table
          columns={columns}
          data={categories}
          keyExtractor={(row) => row.id}
          emptyMessage="Nenhuma categoria cadastrada."
        />
      )}

      {/* Modal — Criar */}
      <Modal
        title="Nova categoria"
        isOpen={modalOpen}
        onClose={() => setModalOpen(false)}
      >
        <CategoryForm
          onSubmit={handleCreate}
          onCancel={() => setModalOpen(false)}
        />
      </Modal>
    </div>
  );
}
