import React, { useState } from 'react';
import { api } from '../services/api';

export default function Clientes() {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [mensaje, setMensaje] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Enviamos el objeto con las propiedades exactas que espera tu .NET (name y email)
      await api.registrarCliente({ name, email });
      setMensaje('¡Cliente registrado con éxito!');
      setName('');
      setEmail('');
    } catch (error) {
      console.error(error);
      setMensaje('Error al registrar el cliente. Verifica la consola o CORS.');
    }
  };

  return (
    <div className="card p-4 shadow-sm">
      <h2 className="mb-4">1. Registro de Clientes</h2>
      {mensaje && <div className="alert alert-info">{mensaje}</div>}
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Nombre Completo</label>
          <input 
            type="text" 
            className="form-control" 
            value={name} 
            onChange={(e) => setName(e.target.value)} 
            required 
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Correo Electrónico</label>
          <input 
            type="email" 
            className="form-control" 
            value={email} 
            onChange={(e) => setEmail(e.target.value)} 
            required 
          />
        </div>
        <button type="submit" className="btn btn-primary w-100">Registrar Cliente</button>
      </form>
    </div>
  );
}