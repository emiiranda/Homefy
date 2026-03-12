import { useEffect, useState, useCallback } from "react";
import type { Person } from "../../types/person.ts";
import {
  getPersons,
  createPerson,
  updatePerson,
  deletePerson,
} from "../../api/persons.ts";
import Table, { type Column } from "../../components/Table.tsx";
import Modal from "../../components/Modal.tsx";
import PersonForm from "./PersonForm.tsx";

type ModalMode = "create" | "edit" | "delete" | null;

export default function PersonsPage() {
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [modalMode, setModalMode] = useState<ModalMode>(null);
  const [selected, setSelected] = useState<Person | null>(null);
  const [deleteLoading, setDeleteLoading] = useState(false);

  const fetchPersons = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const { data } = await getPersons();
      setPersons(data);
    } catch {
      setError("Erro ao carregar pessoas. Verifique a conexão com a API.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchPersons();
  }, [fetchPersons]);

  function openCreate() {
    setSelected(null);
    setModalMode("create");
  }
  function openEdit(person: Person) {
    setSelected(person);
    setModalMode("edit");
  }
  function openDelete(person: Person) {
    setSelected(person);
    setModalMode("delete");
  }
  function closeModal() {
    setModalMode(null);
    setSelected(null);
  }

  async function handleCreate(data: { name: string; age: number }) {
    await createPerson(data);
    await fetchPersons();
    closeModal();
  }

  async function handleEdit(data: { name: string; age: number }) {
    if (!selected) return;
    await updatePerson(selected.id, data);
    await fetchPersons();
    closeModal();
  }

  async function handleDelete() {
    if (!selected) return;
    setDeleteLoading(true);
    try {
      await deletePerson(selected.id);
      await fetchPersons();
      closeModal();
    } catch {
      setDeleteLoading(false);
    }
  }

  const columns: Column<Person>[] = [
    { header: "Nome", accessor: "name" },
    {
      header: "Idade",
      accessor: (row) => `${row.age} anos`,
      className: "w-28",
    },
    {
      header: "Ações",
      className: "w-36 text-right",
      accessor: (row) => (
        <div className="flex justify-end gap-2">
          <button
            onClick={() => openEdit(row)}
            className="px-3 py-1 text-xs rounded-md border border-gray-300 text-gray-600 hover:bg-gray-50 transition-colors"
          >
            Editar
          </button>
          <button
            onClick={() => openDelete(row)}
            className="px-3 py-1 text-xs rounded-md border border-red-200 text-red-600 hover:bg-red-50 transition-colors"
          >
            Excluir
          </button>
        </div>
      ),
    },
  ];

  return (
    <div className="flex flex-col gap-6">
      {/* Cabeçalho */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-800">Pessoas</h1>
          <p className="text-sm text-gray-500 mt-0.5">
            Gerencie aqui as pessoas da residência, como familiares ou colegas
            de quarto.
          </p>
        </div>
        <button
          onClick={openCreate}
          className="px-4 py-2 text-sm rounded-md bg-green-800 text-white hover:bg-green-700 transition-colors"
        >
          + Nova pessoa
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
          data={persons}
          keyExtractor={(row) => row.id}
          emptyMessage="Nenhuma pessoa cadastrada."
        />
      )}

      {/* Modal — Criar */}
      <Modal
        title="Nova pessoa"
        isOpen={modalMode === "create"}
        onClose={closeModal}
      >
        <PersonForm onSubmit={handleCreate} onCancel={closeModal} />
      </Modal>

      {/* Modal — Editar */}
      <Modal
        title="Editar pessoa"
        isOpen={modalMode === "edit"}
        onClose={closeModal}
      >
        <PersonForm
          initial={selected ?? undefined}
          onSubmit={handleEdit}
          onCancel={closeModal}
        />
      </Modal>

      {/* Modal — Confirmar exclusão */}
      <Modal
        title="Excluir pessoa"
        isOpen={modalMode === "delete"}
        onClose={closeModal}
      >
        <div className="flex flex-col gap-4">
          <p className="text-sm text-gray-600">
            Tem certeza que deseja excluir{" "}
            <span className="font-semibold text-gray-800">
              {selected?.name}
            </span>
            ? Esta ação não pode ser desfeita.
          </p>
          <div className="flex justify-end gap-2">
            <button
              onClick={closeModal}
              disabled={deleteLoading}
              className="px-4 py-2 text-sm rounded-md border border-gray-300 text-gray-600 hover:bg-gray-50 transition-colors"
            >
              Cancelar
            </button>
            <button
              onClick={handleDelete}
              disabled={deleteLoading}
              className="px-4 py-2 text-sm rounded-md bg-red-600 text-white hover:bg-red-700 transition-colors disabled:opacity-50"
            >
              {deleteLoading ? "Excluindo..." : "Confirmar exclusão"}
            </button>
          </div>
        </div>
      </Modal>
    </div>
  );
}
