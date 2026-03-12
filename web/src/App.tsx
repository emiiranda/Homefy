import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Navbar from "./components/Navbar.tsx";
import PersonsPage from "./pages/persons/PersonsPage.tsx";
import CategoriesPage from "./pages/categories/CategoriesPage.tsx";
import TransactionsPage from "./pages/transactions/TransactionsPage.tsx";
import TotalsPersonsPage from "./pages/totals/TotalsPersonsPage.tsx";
import TotalsCategoriesPage from "./pages/totals/TotalsCategoriesPage.tsx";

export default function App() {
  return (
    <BrowserRouter>
      <div className="min-h-screen bg-gray-50">
        <Navbar />
        <main className="container mx-auto px-4 py-8">
          <Routes>
            <Route path="/" element={<Navigate to="/persons" replace />} />
            <Route path="/persons" element={<PersonsPage />} />
            <Route path="/categories" element={<CategoriesPage />} />
            <Route path="/transactions" element={<TransactionsPage />} />
            <Route path="/totals/persons" element={<TotalsPersonsPage />} />
            <Route
              path="/totals/categories"
              element={<TotalsCategoriesPage />}
            />
          </Routes>
        </main>
      </div>
    </BrowserRouter>
  );
}
