import { useState } from "react";
import { NavLink } from "react-router-dom";

const links = [
  { to: "/persons", label: "Pessoas" },
  { to: "/categories", label: "Categorias" },
  { to: "/transactions", label: "Transações" },
  { to: "/totals/persons", label: "Totais por Pessoa" },
  { to: "/totals/categories", label: "Totais por Categoria" },
];

export default function Navbar() {
  const [open, setOpen] = useState(false);

  const linkClass = ({ isActive }: { isActive: boolean }) =>
    `px-3 py-2 rounded-md text-sm transition-colors ${
      isActive
        ? "bg-green-50 text-green-800 font-bold"
        : "text-gray-600 font-medium hover:bg-gray-100"
    }`;

  return (
    <nav className="bg-white shadow-sm border-b border-gray-200">
      <div className="container mx-auto px-4">
        {/* Barra principal */}
        <div className="flex items-center justify-between h-14">
          <span className="font-extrabold text-lg tracking-tight select-none">
            <span className="text-red-800">Home</span>
            <span className="text-green-800">fy</span>
          </span>

          {/* Links — visíveis apenas em telas maiores */}
          <div className="hidden md:flex items-center gap-1">
            {links.map((link) => (
              <NavLink key={link.to} to={link.to} className={linkClass}>
                {link.label}
              </NavLink>
            ))}
          </div>

          {/* Botão hambúrguer — visível apenas em mobile */}
          <button
            className="md:hidden p-2 rounded-md text-gray-600 hover:bg-gray-100 transition-colors"
            onClick={() => setOpen((prev) => !prev)}
            aria-label="Menu"
          >
            {open ? "✕" : "☰"}
          </button>
        </div>

        {/* Menu mobile expandido */}
        {open && (
          <div className="md:hidden flex flex-col gap-1 pb-3">
            {links.map((link) => (
              <NavLink
                key={link.to}
                to={link.to}
                className={linkClass}
                onClick={() => setOpen(false)}
              >
                {link.label}
              </NavLink>
            ))}
          </div>
        )}
      </div>
    </nav>
  );
}
