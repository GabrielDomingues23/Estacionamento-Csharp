import { useState } from "react";

function App() {
  const [vagas, setVagas] = useState([]);
  const [mensagem, setMensagem] = useState("");

  async function buscarVagas() {
    try {
      setMensagem("Buscando vagas...");

      const response = await fetch(
        "http://localhost:5132/api/Estacionamento/vagas"
      );

      if (!response.ok) {
        throw new Error("Erro ao acessar backend");
      }

      const data = await response.json();

      setVagas(data);
      setMensagem("Backend conectado com sucesso!");
    } catch (error) {
      console.error(error);
      setMensagem("Erro ao conectar com o backend");
    }
  }

  return (
    <div style={{ padding: "20px" }}>
      <h1>Teste Frontend + Backend</h1>

      <button onClick={buscarVagas}>
        Buscar vagas
      </button>

      <p>{mensagem}</p>

      <ul>
        {vagas.map((vaga) => (
          <li key={vaga.id}>
            {vaga.numeroVaga} - {vaga.status}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;