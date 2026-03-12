import { NavLink } from 'react-router-dom'

const links = [
  { to: '/persons',           label: 'Pessoas'              },
  { to: '/categories',        label: 'Categorias'           },
  { to: '/transactions',      label: 'Transações'           },
  { to: '/totals/persons',    label: 'Totais por Pessoa'    },
  { to: '/totals/categories', label: 'Totais por Categoria' },
]

export default function Navbar() {
  return (
    <nav className="bg-white shadow-sm border-b border-gray-200">
      <div className="container mx-auto px-4">
        <div className="flex items-center gap-1 h-14">

          {/* Logo Homefy — "Home" em vermelho, "fy" em verde */}
          <span className="font-extrabold text-lg tracking-tight mr-6 select-none">
            <span className="text-red-800">Home</span>
            <span className="text-green-800">fy</span>
          </span>

          {/* Links de navegação */}
          {links.map(link => (
            <NavLink
              key={link.to}
              to={link.to}
              className={({ isActive }) =>
                `px-3 py-2 rounded-md text-sm transition-colors ${
                  isActive
                    ? 'bg-green-50 text-green-800 font-bold'
                    : 'text-gray-600 font-medium hover:bg-gray-100'
                }`
              }
            >
              {link.label}
            </NavLink>
          ))}

        </div>
      </div>
    </nav>
  )
}
