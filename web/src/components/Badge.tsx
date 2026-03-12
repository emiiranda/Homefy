import { CategoryPurpose } from "../types/category";
import { TransactionType } from "../types/transaction";

type BadgeVariant = "categoryPurpose" | "transactionType";

interface BadgeProps {
  variant: BadgeVariant;
  value: number;
}

const config: Record<
  BadgeVariant,
  Record<number, { label: string; className: string }>
> = {
  categoryPurpose: {
    [CategoryPurpose.Expense]: {
      label: "Despesa",
      className: "bg-red-100 text-red-700",
    },
    [CategoryPurpose.Income]: {
      label: "Receita",
      className: "bg-green-100 text-green-700",
    },
    [CategoryPurpose.Both]: {
      label: "Ambos",
      className: "bg-blue-100 text-blue-700",
    },
  },
  transactionType: {
    [TransactionType.Expense]: {
      label: "Despesa",
      className: "bg-red-100 text-red-700",
    },
    [TransactionType.Income]: {
      label: "Receita",
      className: "bg-green-100 text-green-700",
    },
  },
};

export default function Badge({ variant, value }: BadgeProps) {
  const item = config[variant][value];
  if (!item) return null;
  return (
    <span
      className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${item.className}`}
    >
      {item.label}
    </span>
  );
}
