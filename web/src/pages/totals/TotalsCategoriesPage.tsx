import { useEffect, useState, useCallback } from "react";
import type {
  CategoryTotalsItem,
  CategoryTotalsResponse,
} from "../../types/category.ts";
import { getCategoryTotals } from "../../api/totals.ts";
import Table, { type Column } from "../../components/Table.tsx";
import Badge from "../../components/Badge.tsx";

function formatBRL(value: number) {
  return value.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

function balanceClass(value: number) {
  if (value > 0) return "text-green-700 font-medium";
  if (value < 0) return "text-red-600 font-medium";
  return "text-gray-500";
}

export default function TotalsCategoriesPage() {
  const [data, setData] = useState<CategoryTotalsResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const fetchTotals = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const { data: response } = await getCategoryTotals();
      setData(response);
    } catch {
      setError("Erro ao carregar totais. Verifique a conexão com a API.");
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchTotals();
  }, [fetchTotals]);

  const columns: Column<CategoryTotalsItem>[] = [
    { header: "Categoria", accessor: "description" },
    {
      header: "Finalidade",
      className: "w-32",
      accessor: (row) => (
        <Badge variant="categoryPurpose" value={row.purpose} />
      ),
    },
    {
      header: "Receitas",
      className: "w-36 text-right",
      accessor: (row) => (
        <span className="text-green-700">{formatBRL(row.totalIncome)}</span>
      ),
    },
    {
      header: "Despesas",
      className: "w-36 text-right",
      accessor: (row) => (
        <span className="text-red-600">{formatBRL(row.totalExpense)}</span>
      ),
    },
    {
      header: "Saldo",
      className: "w-36 text-right",
      accessor: (row) => (
        <span className={`${balanceClass(row.balance)}`}>
          {formatBRL(row.balance)}
        </span>
      ),
    },
  ];

  return (
    <div className="flex flex-col gap-6">
      {/* Cabeçalho */}
      <div>
        <h1 className="text-2xl font-bold text-gray-800">
          Totais por Categoria
        </h1>
        <p className="text-sm text-gray-500 mt-0.5">
          Resumo financeiro consolidado por categoria
        </p>
      </div>

      {/* Erro */}
      {error && (
        <p className="text-sm text-red-600 bg-red-50 border border-red-200 rounded-md px-4 py-3">
          {error}
        </p>
      )}

      {/* Tabela */}
      {loading ? (
        <p className="text-sm text-gray-400">Carregando...</p>
      ) : (
        <>
          <Table
            columns={columns}
            data={data?.categories ?? []}
            keyExtractor={(row) => row.id}
            emptyMessage="Nenhum dado encontrado."
          />

          {/* Rodapé de totais gerais */}
          {data && (
            <div className="flex justify-end">
              <div className="bg-white border border-gray-200 rounded-lg px-6 py-4 flex gap-8 text-sm">
                <div className="flex flex-col items-end gap-1">
                  <span className="text-gray-500 text-xs uppercase tracking-wide">
                    Total Receitas
                  </span>
                  <span className="text-green-700 font-semibold">
                    {formatBRL(data.totalIncome)}
                  </span>
                </div>
                <div className="flex flex-col items-end gap-1">
                  <span className="text-gray-500 text-xs uppercase tracking-wide">
                    Total Despesas
                  </span>
                  <span className="text-red-600 font-semibold">
                    {formatBRL(data.totalExpense)}
                  </span>
                </div>
                <div className="flex flex-col items-end gap-1">
                  <span className="text-gray-500 text-xs uppercase tracking-wide">
                    Saldo Geral
                  </span>
                  <span
                    className={`font-semibold ${balanceClass(data.balance)}`}
                  >
                    {formatBRL(data.balance)}
                  </span>
                </div>
              </div>
            </div>
          )}
        </>
      )}
    </div>
  );
}
