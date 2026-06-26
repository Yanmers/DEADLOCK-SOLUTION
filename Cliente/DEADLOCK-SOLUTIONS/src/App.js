import React from 'react';
import Clientes from './components/Clientes';
import Ordenes from './components/Ordenes';

function App() {
  return (
    <div>
      {/* Navbar de presentación */}
      <nav className="navbar navbar-dark bg-dark mb-4 shadow">
        <div className="container">
          <span className="navbar-brand mb-0 h1">📦 Sistema de Ventas & Facturación .NET / React</span>
          <span className="badge bg-info text-dark font-monospace">Evaluación Candidato</span>
        </div>
      </nav>

      {/* Contenedor Principal */}
      <div className="container mb-5">
        <div className="row g-4">
          <div className="col-lg-4">
            <Clientes />
          </div>
          <div className="col-lg-8">
            <Ordenes />
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
