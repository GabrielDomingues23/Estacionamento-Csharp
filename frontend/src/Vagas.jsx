import { useEffect, useState } from 'react';

const API_BASE = 'http://localhost:5132/api/estacionamento';

function getStatusText(status) {
  return status === 1 || status === 'Ocupada' ? 'Ocupada' : 'Livre';
}

function Vagas({ token, onLogout, status, setStatus }) {
  const [vagas, setVagas] = useState([]);
  const [loading, setLoading] = useState(false);
  const [newVaga, setNewVaga] = useState('');
  const [carroInputs, setCarroInputs] = useState({});
  const [modalInfo, setModalInfo] = useState(null);

  const apiRequest = async (path, method, body) => {
    const response = await fetch(`${API_BASE}${path}`, {
      method,
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: body ? JSON.stringify(body) : undefined,
    });

    const text = await response.text();
    const contentType = response.headers.get('content-type') || '';
    const isJson = contentType.includes('application/json');
    const data = isJson ? JSON.parse(text || '{}') : text;

    if (!response.ok) {
      const errorMessage = isJson ? data?.mensagem || data?.message || text : text || response.statusText;
      throw new Error(errorMessage || 'Erro na requisição');
    }

    if (response.status === 204) return null;
    return isJson ? data : text;
  };

  const fetchVagas = async () => {
    setLoading(true);
    try {
      const data = await apiRequest('/vagas', 'GET');
      setVagas(data);
      setStatus('Dados de vagas atualizados.');
    } catch (err) {
      console.error(err);
      setStatus(err instanceof Error ? err.message : 'Falha ao buscar vagas.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchVagas();
  }, []);

  const createVaga = async (event) => {
    event.preventDefault();
    if (!newVaga.trim()) {
      setStatus('Número da vaga é obrigatório.');
      return;
    }

    try {
      await apiRequest('/vagas', 'POST', { numeroVaga: newVaga, status: 0 });
      setNewVaga('');
      fetchVagas();
    } catch (err) {
      console.error(err);
      setStatus(err instanceof Error ? err.message : 'Falha ao criar vaga.');
    }
  };

  const handleEntrada = async (vagaId) => {
    const carro = carroInputs[vagaId] || {};
    if (!carro.Placa || !carro.Marca || !carro.Modelo) {
      setStatus('Preencha placa, marca e modelo do carro.');
      return;
    }

    try {
      await apiRequest(`/entrada/${vagaId}`, 'POST', carro);
      setCarroInputs((prev) => ({ ...prev, [vagaId]: {} }));
      fetchVagas();
    } catch (err) {
      console.error(err);
      setStatus(err instanceof Error ? err.message : 'Falha ao registrar entrada.');
    }
  };

  const handleSaida = async (vagaId) => {
    try {
      const result = await apiRequest(`/saida/${vagaId}`, 'PUT');
      const valorTotal = result?.ValorTotal ?? result?.valorTotal;
      const horasEstacionado = result?.HorasEstacionado ?? result?.horasEstacionado;

      if (valorTotal) {
        setStatus(`Saída processada: ${horasEstacionado ?? '?'} hora(s)`);
        setModalInfo({
          title: 'Saída registrada',
          message: `Valor a pagar: ${valorTotal}`,
          details: horasEstacionado ? `Tempo estacionado: ${horasEstacionado} hora(s)` : undefined,
        });
      } else {
        setStatus('Saída processada.');
        setModalInfo({ title: 'Saída registrada', message: 'Saída processada com sucesso.' });
      }
      fetchVagas();
    } catch (err) {
      console.error(err);
      setStatus(err instanceof Error ? err.message : 'Falha ao registrar saída.');
    }
  };

  const closeModal = () => {
    setModalInfo(null);
  };

  const handleDelete = async (id) => {
    try {
      await apiRequest(`/vagas/${id}`, 'DELETE');
      fetchVagas();
    } catch (err) {
      console.error(err);
      setStatus(err instanceof Error ? err.message : 'Falha ao deletar vaga.');
    }
  };

  return (
    <main className="panel">
      <div className="toolbar">
        <h2>Gestão de Vagas</h2>
        <button className="button-secondary" onClick={onLogout}>
          Sair
        </button>
      </div>

      <section className="form-card">
        <h3>Criar vaga</h3>
        <form onSubmit={createVaga}>
          <input
            value={newVaga}
            onChange={(event) => setNewVaga(event.target.value)}
            placeholder="Número da vaga"
          />
          <button type="submit">Adicionar vaga</button>
        </form>
      </section>

      <section className="form-card">
        <p className="status">{status}</p>
        {loading ? (
          <p>Carregando vagas...</p>
        ) : (
          <div className="table-container">
            <table>
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Vaga</th>
                  <th>Status</th>
                  <th>Placa</th>
                  <th>Marca</th>
                  <th>Modelo</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                {vagas.map((vaga) => {
                  const statusText = getStatusText(vaga.status);
                  const carro = vaga.carro || {};

                  return (
                    <tr key={vaga.id}>
                      <td>{vaga.id}</td>
                      <td>{vaga.numeroVaga}</td>
                      <td>{statusText}</td>
                      <td>{carro.placa || '-'}</td>
                      <td>{carro.marca || '-'}</td>
                      <td>{carro.modelo || '-'}</td>
                      <td className="actions-cell">
                        {statusText === 'Livre' ? (
                          <div className="action-row">
                            <input
                              placeholder="Placa"
                              value={carroInputs[vaga.id]?.Placa || ''}
                              onChange={(event) =>
                                setCarroInputs((prev) => ({
                                  ...prev,
                                  [vaga.id]: { ...prev[vaga.id], Placa: event.target.value },
                                }))
                              }
                            />
                            <input
                              placeholder="Marca"
                              value={carroInputs[vaga.id]?.Marca || ''}
                              onChange={(event) =>
                                setCarroInputs((prev) => ({
                                  ...prev,
                                  [vaga.id]: { ...prev[vaga.id], Marca: event.target.value },
                                }))
                              }
                            />
                            <input
                              placeholder="Modelo"
                              value={carroInputs[vaga.id]?.Modelo || ''}
                              onChange={(event) =>
                                setCarroInputs((prev) => ({
                                  ...prev,
                                  [vaga.id]: { ...prev[vaga.id], Modelo: event.target.value },
                                }))
                              }
                            />
                            <button type="button" onClick={() => handleEntrada(vaga.id)}>
                              Entrada
                            </button>
                            <button type="button" onClick={() => handleDelete(vaga.id)}>
                              Deletar
                            </button>
                          </div>
                        ) : (
                          <button type="button" onClick={() => handleSaida(vaga.id)}>
                            Saída
                          </button>
                        )}
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        )}
      </section>
      {modalInfo ? (
        <div className="modal-overlay" onClick={closeModal}>
          <div className="modal-card" onClick={(event) => event.stopPropagation()}>
            <h3>{modalInfo.title}</h3>
            <p>{modalInfo.message}</p>
            {modalInfo.details && <p className="modal-details">{modalInfo.details}</p>}
            <button onClick={closeModal}>Fechar</button>
          </div>
        </div>
      ) : null}
    </main>
  );
}

export default Vagas;
