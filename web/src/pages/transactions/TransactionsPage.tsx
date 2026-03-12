import { useEffect, useState, useCallback } from "react";
import type {
  Transaction,
  CreateTransactionRequest,
} from "../../types/transaction.ts";
import { getTransactions, createTransaction } from "../../api/transactions.ts";
import Table, { type Column } from "../../components/Table.tsx";
import Modal from "../../components/Modal.tsx";
import Badge from "../../components/Badge.tsx";
import TransactionForm from "./TransactionForm.tsx";

export default function TransactionsPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [modalOpen, setModalOpen] = useState(false);

  const fetchTransactions = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const { data } = await getTransactions();
      setTransactions(data);
    } catch {
      setError("Erro ao carregar transações. Verifique a conexão com a API.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchTransactions();
  }, [fetchTransactions]);

  async function handleCreate(data: CreateTransactionRequest) {
    await createTransaction(data);
    await fetchTransactions();
    setModalOpen(false);
  }

  const columns: Column<Transaction>[] = [
    { header: "Descrição", accessor: "description" },
    {
      header: "Valor",
      className: "w-36",
      accessor: (row) =>
        row.amount.toLocaleString("pt-BR", {
          style: "currency",
          currency: "BRL",
        }),
    },
    {
      header: "Tipo",
      className: "w-28",
      accessor: (row) => <Badge variant="transactionType" value={row.type} />,
    },
    { header: "Pessoa", className: "w-40", accessor: "personName" },
    { header: "Categoria", className: "w-40", accessor: "categoryDescription" },
  ];

  return (
    <div className="flex flex-col gap-6">
      {/* Cabeçalho */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-gray-800">Transações</h1>
          <p className="text-sm text-gray-500 mt-0.5">
            Registre receitas e despesas
          </p>
        </div>
        <button
          onClick={() => setModalOpen(true)}
          className="px-4 py-2 text-sm rounded-md bg-green-800 text-white hover:bg-green-700 transition-colors"
        >
          + Nova transação
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
          data={transactions}
          keyExtractor={(row) => row.id}
          emptyMessage="Nenhuma transação registrada."
        />
      )}

      {/* Modal — Criar */}
      <Modal
        title="Nova transação"
        isOpen={modalOpen}
        onClose={() => setModalOpen(false)}
      >
        <TransactionForm
          onSubmit={handleCreate}
          onCancel={() => setModalOpen(false)}
        />
      </Modal>
    </div>
  );
}
