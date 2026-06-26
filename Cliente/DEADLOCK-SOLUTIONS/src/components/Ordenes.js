import React, { useState } from 'react';
import { api } from '../services/api';

export default function Ordenes() {
  // Estado para la creación de una nueva orden
  const [clienteId, setClienteId] = useState('2'); 
  
  // Requerimiento 2: Carga inicial obligatoria con mínimo dos productos de tu DB
  const [itemsNuevaOrden, setItemsNuevaOrden] = useState([
    { productoId: 1, cantidad: 1 }, // Laptop Pro 15"
    { productoId: 2, cantidad: 2 }  // Mouse Inalámbrico
  ]);

  // Estados para buscar, modificar y facturar órdenes existentes
  const [ordenIdBuscada, setOrdenIdBuscada] = useState('');
  const [ordenActiva, setOrdenActiva] = useState(null);
  const [factura, setFactura] = useState(null);

  // 1. Enviar la orden estructurada al backend
  const handleCrearOrden = async () => {
    try {
      const ordenDto = {
        clienteId: parseInt(clienteId),
        items: itemsNuevaOrden
      };
      
      const res = await api.crearOrden(ordenDto);
      alert(`¡Órden creada exitosamente! ID de la orden: ${res.data.id || 'Verificar'}`);
    } catch (error) {
      console.error(error);
      alert('Error al crear la orden. Verifica la consola del navegador.');
    }
  };

  // 2. Cargar orden existente para las modificaciones de ítems o stock
  const buscarOrden = async () => {
    if (!ordenIdBuscada) return;
    try {
      const res = await api.obtenerOrden(ordenIdBuscada);
      setOrdenActiva(res.data);
      setFactura(null); 
    } catch (error) {
      console.error(error);
      alert('Orden no encontrada en el sistema.');
    }
  };

  // 3. Actualizar la cantidad de un ítem existente
  const handleActualizarCantidad = async (productoId, nuevaCantidad) => {
    try {
      await api.actualizarCantidadProducto(ordenActiva.id, productoId, { cantidad: nuevaCantidad });
      buscarOrden(); // Recargar datos frescos
    } catch (error) {
      console.error(error);
      alert('Error al actualizar la cantidad del ítem.');
    }
  };

  // 4. Eliminar ítem (Dispara la lógica de restauración de stock en tu backend)
  const handleEliminarProducto = async (productoId) => {
    try {
      await api.eliminarProductoOrden(ordenActiva.id, productoId);
      buscarOrden(); // Recargar datos frescos
    } catch (error) {
      console.error(error);
      alert('Error al remover el ítem de la orden.');
    }
  };

  // 5. Generar factura (Requerimiento 3: Consumiendo POST /api/Factura/{id})
  const handleGenerarFactura = async () => {
    try {
      const res = await api.generarFactura(ordenActiva.id);
      setFactura(res.data);
    } catch (error) {
      console.error(error);
      alert('Error al procesar la facturación de esta orden.');
    }
  };

  return (
    <div className="row g-4">
      {/* FORMULARIO: CREAR ÓRDEN */}
      <div className="col-md-6">
        <div className="card p-4 shadow-sm h-100">
          <h3 className="mb-3 text-primary">2. Gestión de Órdenes</h3>
          <p className="text-muted small">Carga inicial configurada con productos existentes.</p>
          
          <div className="mb-3">
            <label className="form-label fw-bold">ID Cliente Asociado</label>
            <input 
              type="number" 
              className="form-control" 
              value={clienteId} 
              onChange={(e) => setClienteId(e.target.value)} 
            />
          </div>

          <h5 className="mt-4">Productos asignados en el JSON:</h5>
          <ul className="list-group mb-4">
            <li className="list-group-item d-flex justify-content-between align-items-center bg-light">
              <span>💻 Laptop Pro 15" (Id: 1)</span>
              <span className="badge bg-primary rounded-pill">Cantidad: 1</span>
            </li>
            <li className="list-group-item d-flex justify-content-between align-items-center bg-light">
              <span>🖱️ Mouse Inalámbrico (Id: 2)</span>
              <span className="badge bg-primary rounded-pill">Cantidad: 2</span>
            </li>
          </ul>
          
          <button className="btn btn-success w-100 mt-auto fw-bold py-2" onClick={handleCrearOrden}>
            Crear Orden de Compra
          </button>
        </div>
      </div>

      {/* PANEL: GESTIONAR ÓRDEN ACTIVA (PUT / DELETE) */}
      <div className="col-md-6">
        <div className="card p-4 shadow-sm h-100">
          <h3 className="mb-3 text-dark">Modificar / Facturar</h3>
          <div className="input-group mb-4">
            <input 
              type="number" 
              className="form-control" 
              placeholder="Ingrese ID de Orden generada" 
              value={ordenIdBuscada}
              onChange={(e) => setOrdenIdBuscada(e.target.value)}
            />
            <button className="btn btn-dark" onClick={buscarOrden}>Cargar Orden</button>
          </div>

          {ordenActiva && (
            <div>
              <div className="alert alert-secondary py-2 small">
                <strong>Orden ID:</strong> #{ordenActiva.id} | <strong>Cliente ID:</strong> {ordenActiva.clienteId}
              </div>
              
              <table className="table table-sm align-middle mt-3">
                <thead>
                  <tr>
                    <th>Prod ID</th>
                    <th>Cantidad</th>
                    <th className="text-end">Acción</th>
                  </tr>
                </thead>
                <tbody>
                  {(ordenActiva.items || ordenActiva.detalles || []).map((item) => (
                    <tr key={item.productoId}>
                      <td><strong>{item.productoId}</strong></td>
                      <td>
                        <input 
                          type="number" 
                          className="form-control form-control-sm" 
                          style={{ width: '75px' }}
                          defaultValue={item.cantidad}
                          onBlur={(e) => handleActualizarCantidad(item.productoId, parseInt(e.target.value))}
                        />
                      </td>
                      <td className="text-end">
                        <button 
                          className="btn btn-outline-danger btn-sm" 
                          onClick={() => handleEliminarProducto(item.productoId)}
                        >
                          Eliminar
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>

              <button className="btn btn-warning w-100 mt-4 fw-bold py-2 text-uppercase" onClick={handleGenerarFactura}>
                3. Generar Factura (Preview)
              </button>
            </div>
          )}
        </div>
      </div>

      {/* COMPONENTE VISUAL: PREVIEW DE LA FACTURA ADAPTADO A TU JSON */}
      {factura && (
        <div className="col-12 mt-4">
          <div className="card p-4 border-success bg-white shadow">
            <div className="d-flex justify-content-between align-items-center mb-2">
              <h4 className="m-0 text-success fw-bold">🧾 FACTURA ELECTRÓNICA</h4>
              <span className="badge bg-success font-monospace p-2">EFECTUADO - 200 OK</span>
            </div>
            <hr className="my-2" />
            <div className="row small mb-4 bg-light p-3 rounded">
              <div className="col-md-6 mb-2 mb-md-0">
                <strong>Factura Folio:</strong> #{factura.id}<br />
                <strong>Orden Origen:</strong> #{ordenActiva.id}
              </div>
              <div className="col-md-6 text-md-end">
                <strong>Cliente Propietario:</strong> ID {factura.clienteId}<br />
                <strong>Fecha Registro:</strong> {new Date(factura.fecha).toLocaleString()}
              </div>
            </div>

            <table className="table table-bordered table-striped table-sm m-0">
              <thead className="table-success text-dark">
                <tr>
                  <th>Concepto</th>
                  <th className="text-center">Cantidad</th>
                  <th className="text-end">Precio Unit.</th>
                  <th className="text-end">Subtotal</th>
                </tr>
              </thead>
              <tbody>
                {(factura.items || []).map((det, index) => (
                  <tr key={index}>
                    <td>Producto ID #{det.productoId}</td>
                    <td className="text-center fw-bold">{det.cantidad}</td>
                    <td className="text-end">${det.precioUnitario.toFixed(2)}</td>
                    <td className="text-end text-primary fw-bold">${det.subtotal.toFixed(2)}</td>
                  </tr>
                ))}
                <tr className="table-light">
                  <td colSpan="3" className="text-end fw-bold fs-5">TOTAL NETO:</td>
                  <td className="text-end text-success fw-bold fs-5">
                    ${factura.total.toFixed(2)}
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
}