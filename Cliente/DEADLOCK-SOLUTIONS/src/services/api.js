import axios from 'axios';

const API_BASE_URL = 'https://localhost:7134/api'; 

export const api = {


  // 1. Gestión de Clientes
  registrarCliente: (cliente) => axios.post(`${API_BASE_URL}/GestionClientes`, cliente),
  obtenerClientes: () => axios.get(`${API_BASE_URL}/GestionClientes`),

   // 2. Gestión de Órdenes 
  crearOrden: (orden) => axios.post(`${API_BASE_URL}/Order`, orden),
  obtenerOrden: (id) => axios.get(`${API_BASE_URL}/Order/${id}`),
  actualizarCantidadProducto: (ordenId, productoId, data) => 
    axios.put(`${API_BASE_URL}/Order/${ordenId}/productos/${productoId}`, data),
  eliminarProductoOrden: (ordenId, productoId) => 
    axios.delete(`${API_BASE_URL}/Order/${ordenId}/productos/${productoId}`),

  //3. facturacion
  generarFactura: (ordenId) => axios.post(`${API_BASE_URL}/Factura/${ordenId}`)
};